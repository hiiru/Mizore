using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mizore.ContentSerializer.JsonNet;
using Mizore.util;

namespace Mizore.ContentSerializer.JavaBin
{
    public class JavaBinSerializer : IContentSerializer
    {
        public string wt { get { return "javabin"; } }

        public string ContentType { get { return "application/javabin"; } }

        public Version SupportedSince { get; private set; }

        public void Marshal<T>(T obj, Stream stream) where T : INamedList
        {
            var converter = new SolrJavaBinConverter();
            converter.WriteJavaBin(obj, stream);
        }

        public INamedList Unmarshal(Stream stream)
        {
            var converter = new SolrJavaBinConverter();
            return converter.ReadJavaBin(stream);
        }
    }
}
