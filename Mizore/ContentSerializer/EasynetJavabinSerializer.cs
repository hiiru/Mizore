using System;
using System.IO;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.util;

namespace Mizore.ContentSerializer
{
    public class EasynetJavabinSerializer : IContentSerializer
    {
        public string wt { get { return "javabin"; } }

        public string ContentType { get { return "application/javabin"; } }

        public Version SupportedSince { get; private set; }

        public void Marshal<T>(T obj, Stream stream)
        {
            var x = new JavaBinCodec();
            x.Marshal(obj, stream);
        }

        public INamedList Unmarshal(Stream stream)
        {
            var x = new JavaBinCodec();
            return x.Unmarshal(stream) as EasynetNamedList;
        }
    }
}