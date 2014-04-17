using Mizore.ContentSerializer.JavaBin;
using Mizore.ContentSerializer.JsonNet;
using Mizore.Exceptions;
using System;
using System.Collections.Generic;

namespace Mizore.ContentSerializer
{
    public class ContentSerializerFactory : IContentSerializerFactory
    {
        /// <summary>
        /// This maps the ContentType (or any of it's aliases) to a Serializer instance
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
        }

        public IContentSerializer GetContentSerializer(string type)
        {
            if (type == null)
                return DefaultSerializer;
            //ignore encoding suffixes
            if (type.IndexOf(';') != -1)
                type = type.Substring(0, type.IndexOf(';'));
            IContentSerializer serializer;
            if (Serializers.TryGetValue(type, out serializer))
                return serializer;
            return null;
        }

        public void RegisterContentSerializer(IContentSerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (Serializers.ContainsKey(serializer.ContentType))
                throw new MizoreSerializationException("Serializer for Content-Type " + serializer.ContentType + " is already registered", serializer);
            Serializers[serializer.ContentType] = serializer;
            if (!serializer.Aliases.IsNullOrEmpty())
            {
                foreach (var alias in serializer.Aliases)
                {
                    if (Serializers.ContainsKey(alias))
                        throw new MizoreSerializationException("Serializer for alias Content-Type " + serializer.ContentType + " is already registered", serializer);
                    Serializers[alias] = serializer;
                }
            }
        }
    }
}