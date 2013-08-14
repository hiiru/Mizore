using System;
using System.IO;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.util;

namespace Mizore.ContentSerializer
{
    internal class JsonNetSerializer : IContentSerializer
    {
		 public string wt { get { return "json"; } }
		 public string ContentType { get { return "application/json"; } }
	    public Version SupportedSince { get; private set; }

	    public Stream GetStream<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public NamedList<T> GetObject<T>(Stream stream)
        {
            throw new NotImplementedException();
        }

	    public EasynetNamedList GetObject(Stream stream)
	    {
		    throw new NotImplementedException();
	    }
    }
}