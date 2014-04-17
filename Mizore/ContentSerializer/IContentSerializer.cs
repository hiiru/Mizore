using Mizore.ContentSerializer.Data;
using System.Collections.ObjectModel;
using System.IO;

namespace Mizore.ContentSerializer
{
    /// <summary>
    /// The ContentSerializer classes serialize Data for the transfer to the solr server and back to a named list
    /// Stream<->NamedList
    /// </summary>
    public interface IContentSerializer
    {
        //naming for wt= parameter?
        string WT { get; }

        string ContentType { get; }

        ReadOnlyCollection<string> Aliases { get; }

        //Minimum solr version needed - Version or float?
        //Version SupportedSince { get; }

        void Serialize<T>(T obj, Stream stream) where T : INamedList;

        INamedList Deserialize(Stream stream);
    }
}