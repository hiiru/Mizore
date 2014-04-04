using System;
using Mizore.ContentSerializer.Data.Solr;

namespace Mizore.DataMappingHandler.Reflection
{
    public class ReflectionDataMapper : IDataMappingHandler
    {
        public IDataMappingHandler GetMappingHandler(Type type)
        {
            return null;
        }

        public IDataMappingHandler<T> GetMappingHandler<T>() where T : class, new()
        {
            return null;
        }

        public bool CanHandle(Type type)
        {
            return true;
        }

        public Type GetGenericType()
        {
            return typeof(ReflectionDataMapper<>);
        }

        public object GetObject(SolrDocument doc)
        {
            throw new NotSupportedException("Can't create object in base class");
        }

        public SolrInputDocument GetDocument(object obj)
        {
            throw new NotSupportedException("Can't create document in base class");
        }
    }
}