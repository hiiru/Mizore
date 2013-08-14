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


	    public Stream GetStream<T>(T obj)
	    {
		    throw new NotImplementedException();
	    }

	    public NamedList<T> GetObject<T>(Stream stream)
	    {
			 throw new NotImplementedException();
	    }

		 public void GetStream<T>(T obj, Stream stream)
		 {
			 var x = new JavaBinCodec();
			 x.Marshal(obj, stream);
		 }
	    public EasynetNamedList GetObject(Stream stream)
        {
		    var x = new JavaBinCodec();
	       return x.Unmarshal(stream) as EasynetNamedList;
        }
    }
}