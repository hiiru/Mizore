using System;
using System.Collections.Generic;
using System.IO;
using Mizore.Data;
using Newtonsoft.Json;

namespace Mizore.ContentSerializer.JsonNet
{
    public class JsonNetSerializer : IContentSerializer
    {
        public string wt { get { return "json"; } }

        public string ContentType { get { return "application/json"; } }

        public Version SupportedSince { get; private set; }

        protected JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> { new SolrJsonConverter() }
        };

        public void Marshal<T>(T obj, Stream stream) where T : INamedList
        {
            var writer = new StreamWriter(stream);
            writer.Write(JsonConvert.SerializeObject(obj, SerializerSettings));
            writer.Flush();
        }

        public INamedList Unmarshal(Stream stream)
        {
            return JsonConvert.DeserializeObject<NamedList>(new StreamReader(stream).ReadToEnd(), SerializerSettings);
        }
    }
}