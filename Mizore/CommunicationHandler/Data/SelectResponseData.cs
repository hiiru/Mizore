using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mizore.util;

namespace Mizore.CommunicationHandler.Data
{
    public class SelectResponseData
    {
        public SelectResponseData(INamedList namedList)
        {
            if (namedList.IsNullOrEmpty()) return;
            NumFound = namedList.GetOrDefaultStruct<int>("numFound");
            Start = namedList.GetOrDefaultStruct<int>("start");
            var docs=namedList.GetOrDefault<INamedList>("docs");
            if (docs.IsNullOrEmpty())
                Docs = new List<INamedList>(0);
            else
            {
                Docs = new List<INamedList>(docs.Count);
                for (int i=0;i<docs.Count;i++)
                {
                    var item = docs.GetOrDefault<INamedList>(i);
                    Docs.Add(item);
                }
            }
        }

        public int NumFound;
        public int Start;
        public List<INamedList> Docs;
    }
}
