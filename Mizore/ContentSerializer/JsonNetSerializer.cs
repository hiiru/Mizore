using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Mizore.util;
using Newtonsoft.Json;
using SimpleTestApp;

namespace Mizore.ContentSerializer
{
    public class JsonNetSerializer : IContentSerializer
    {
        public string wt { get { return "json"; } }

        public string ContentType { get { return "application/json"; } }

        public Version SupportedSince { get; private set; }

        protected JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter> {new SolrJsonConverter()}
        };

        public void Marshal<T>(T obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public INamedList Unmarshal(Stream stream)
        {
            return JsonConvert.DeserializeObject<NamedList>(new StreamReader(stream).ReadToEnd(), SerializerSettings);
        }
    }
}