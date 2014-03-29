using System;
using System.Collections.Generic;
using Mizore.ContentSerializer.JavaBin;
using Mizore.ContentSerializer.JsonNet;

namespace Mizore.ContentSerializer
{
    public class ContentSerializerFactory : IContentSerializerFactory
    {
        /// <summary>
        /// This maps the ContentType string to a Serializer instance
        /// e.g. application/json -> JsonNetSerializer
        /// </summary>
        protected Dictionary<string, IContentSerializer> Serializers;

        public IContentSerializer DefaultSerializer { get; protected set; }

        public ContentSerializerFactory()
        {
            Serializers = new Dictionary<string, IContentSerializer>();
            DefaultSerializer = new JsonNetSerializer();
            RegisterContentSerializer(DefaultSerializer);
            RegisterContentSerializer(new JavaBinSerializer());

            /* solr example
    Map<String,ContentStreamLoader> registry = new HashMap<>();
    registry.put("application/xml", new XMLLoader().init(p) );
    registry.put("application/json", new JsonLoader().init(p) );
    registry.put("application/csv", new CSVLoader().init(p) );
    registry.put("application/javabin", new JavabinLoader().init(p) );
    registry.put("text/csv", registry.get("application/csv") );
    registry.put("text/xml", registry.get("application/xml") );
    registry.put("text/json", registry.get("application/json") );
    return registry;
             *
             *
    HashMap<String, QueryResponseWriter> m= new HashMap<>();
    m.put("xml", new XMLResponseWriter());
    m.put("standard", m.get("xml"));
    m.put("json", new JSONResponseWriter());
    m.put("python", new PythonResponseWriter());
    m.put("php", new PHPResponseWriter());
    m.put("phps", new PHPSerializedResponseWriter());
    m.put("ruby", new RubyResponseWriter());
    m.put("raw", new RawResponseWriter());
    m.put("javabin", new BinaryResponseWriter());
    m.put("csv", new CSVResponseWriter());
    m.put("schema.xml", new SchemaXmlResponseWriter());
    DEFAULT_RESPONSE_WRITERS = Collections.unmodifiableMap(m);
             */
        }

        public IContentSerializer GetContentSerializer(string type)
        {
            if (type == null)
                return DefaultSerializer;
            if (type.IndexOf(';') != -1)
                type = type.Substring(0, type.IndexOf(';'));
            IContentSerializer serializer;
            if (Serializers.TryGetValue(type, out serializer))
                return serializer;

            //TODO: what should this return as default value?
            return null;
        }

        public void RegisterContentSerializer(IContentSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (Serializers.ContainsKey(serializer.ContentType))
                throw new InvalidOperationException("Serializer for Content-Type " + serializer.ContentType + " is already registered");
            Serializers[serializer.ContentType] = serializer;
            if (!serializer.Aliases.IsNullOrEmpty())
            {
                foreach (var alias in serializer.Aliases)
                {
                    if (Serializers.ContainsKey(alias))
                        throw new InvalidOperationException("Serializer for alias Content-Type " + serializer.ContentType + " is already registered");
                    Serializers[alias] = serializer;
                }
            }
        }
    }
}