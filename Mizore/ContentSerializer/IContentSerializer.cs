using System;
using System.IO;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.util;

namespace Mizore.ContentSerializer
{
    /// <summary>
    /// The ContentSerializer classes serialize Data for the transfer to the solr server and back to a named list
    /// Stream<->NamedList
    /// </summary>
    public interface IContentSerializer
    {
		 //naming for wt= parameter?
	    string wt { get; }

	    string ContentType { get; }

		 //Minimum solr version needed - Version or float?
	    Version SupportedSince { get; }

		 
        Stream GetStream<T>(T obj);

        NamedList<T> GetObject<T>(Stream stream);

		 //tmp
	    EasynetNamedList GetObject(Stream stream);
    }
}