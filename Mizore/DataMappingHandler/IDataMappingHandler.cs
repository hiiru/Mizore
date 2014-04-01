using Mizore.ContentSerializer.Data.Solr;

namespace Mizore.DataMappingHandler
{
    public interface IDataMappingHandlery<T> where T : class, new()
    {
        T GetObject(SolrDocument nl);

        SolrInputDocument GetDocument(T obj);
    }
}