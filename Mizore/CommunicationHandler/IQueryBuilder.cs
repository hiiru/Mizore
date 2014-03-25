using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO: in which namespace should this interface be?
namespace Mizore.CommunicationHandler
{
    //TODO: change as needed while impelmenting a real querybuilder
    public interface IQueryBuilder
    {
        Dictionary<string, string> QueryParameters { get; }

        string GetRawQuery();
    }
}
