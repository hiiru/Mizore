using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Mizore.ContentSerializer.Data;

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

        public void Marshal<T>(T obj, Stream stream) where T : INamedList
        {
            var converter = new SolrJavaBinConverter();
            converter.WriteJavaBin(obj, stream);
        }

        public INamedList Unmarshal(Stream stream)
        {
            var converter = new SolrJavaBinConverter();
            return converter.ReadJavaBin(stream) as INamedList;
        }
    }
}