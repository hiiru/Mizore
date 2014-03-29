using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Mizore.ContentSerializer.Data;
using Mizore.Exceptions;
using Newtonsoft.Json;

namespace Mizore.ContentSerializer.JsonNet
{
    public class JsonNetSerializer : IContentSerializer
    {
        private readonly ReadOnlyCollection<string> _aliases;

        public ReadOnlyCollection<string> Aliases { get { return _aliases; } }

        public string WT { get { return "json"; } }

        public string ContentType { get { return "application/json"; } }

        public JsonNetSerializer()
        {
            //text/plan is used due to SOLR-2091 (https://issues.apache.org/jira/browse/SOLR-2091) -> wt=json is served as text/plain
            _aliases = new ReadOnlyCollection<string>(new List<string> { WT, "text/json", "text/plain" });
        }

        protected JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> { new SolrJsonConverter() }
        };

        public void Serialize<T>(T obj, Stream stream) where T : INamedList
        {
            try
            {
                var writer = new StreamWriter(stream);
                writer.Write(JsonConvert.SerializeObject(obj, SerializerSettings));
                writer.Flush();
            }
            catch (Exception e)
            {
                throw new MizoreSerializationException("Exception during Serialization.", this, e);
            }
        }

        public INamedList Deserialize(Stream stream)
        {
            try
            {
                return JsonConvert.DeserializeObject<NamedList>(new StreamReader(stream).ReadToEnd(), SerializerSettings);
            }
            catch (Exception e)
            {
                throw new MizoreSerializationException("Exception during Deserialization.", this, e);
            }
        }
    }
}