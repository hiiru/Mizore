using System.Collections.Generic;

//TODO: in which namespace should this interface be?
using System.Collections.Specialized;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler
{
    //TODO: change as needed while impelmenting a real querybuilder
    public interface IQueryBuilder
    {
        INamedList<string> QueryParameters { get; }

        //string GetRawQuery();
    }
}