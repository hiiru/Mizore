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
            var docs=namedList.GetOrDefault<List<object>>("docs");
            if (docs.IsNullOrEmpty())
                Docs = new List<INamedList>(0);
            else
            {
                Docs = new List<INamedList>(docs.Count);
                foreach (var doc in docs)
                {
                    Docs.Add(doc as INamedList);
                }
            }
        }

        public int NumFound;
        public int Start;
        public List<INamedList> Docs;
    }
}
