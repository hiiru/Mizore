using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO: in which namespace should this class be?
namespace Mizore.CommunicationHandler
{
    /// <summary>
    /// Proof of concept / Test querybuilder
    /// </summary>
    public class SimpleQueryBuilder : IQueryBuilder
    {
        public SimpleQueryBuilder(string query)
        {
            QueryParameters = new Dictionary<string, string> { { "q", query } };
        }

        /// <summary>
        /// For Request completation.  NOTE: wt key is not allowed, wt is set inside the request depending on the ContentSerializer
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> QueryParameters { get; protected set; }

        /// <summary>
        /// For Debugging,  might also be replaced by ToString() for better IDE intergration and less method cluttering
        /// </summary>
        public string GetRawQuery()
        {
            throw new NotImplementedException();
        }
    }
}
