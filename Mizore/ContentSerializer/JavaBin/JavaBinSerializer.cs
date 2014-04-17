using Mizore.ContentSerializer.Data;
using Mizore.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Mizore.ContentSerializer.JavaBin
{
    public class JavaBinSerializer : IContentSerializer
    {
        private readonly ReadOnlyCollection<string> _aliases;

        public ReadOnlyCollection<string> Aliases { get { return _aliases; } }

        public string ContentType { get { return "application/javabin"; } }

        public string WT { get { return "javabin"; } }

        public JavaBinSerializer()
        {
            _aliases = new ReadOnlyCollection<string>(new List<string> { WT });
        }

        public Version SupportedSince { get; private set; }

        public void Serialize<T>(T obj, Stream stream) where T : INamedList
        {
            try
            {
                var converter = new SolrJavaBinConverter();
                converter.WriteJavaBin(obj, stream);
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
                var converter = new SolrJavaBinConverter();
                return converter.ReadJavaBin(stream) as INamedList;
            }
            catch (Exception e)
            {
                throw new MizoreSerializationException("Exception during Deserialization.", this, e);
            }
        }
    }
}