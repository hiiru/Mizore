using System;
using Mizore.ContentSerializer.Data.Solr;

namespace Mizore.DataMappingHandler
{
    public interface IDataMappingHandler
    {
        IDataMappingHandler GetMappingHandler(Type type);

        IDataMappingHandler<T> GetMappingHandler<T>() where T : class, new();

        bool CanHandle(Type type);

        Type GetGenericType();

        object GetObject(SolrDocument doc);

        SolrInputDocument GetDocument(object obj);
    }

    public interface IDataMappingHandler<T> : IDataMappingHandler where T : class, new()
    {
        new T GetObject(SolrDocument doc);
    }
}