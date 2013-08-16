using System;
using System.IO;
using Mizore.util;

namespace Mizore.ContentSerializer
{
    internal class JsonNetSerializer : IContentSerializer
    {
        public string wt { get { return "json"; } }

        public string ContentType { get { return "application/json"; } }

        public Version SupportedSince { get; private set; }

        public void Marshal<T>(T obj, Stream stream)
        {
            throw new NotImplementedException();
        }

        public INamedList Unmarshal(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}