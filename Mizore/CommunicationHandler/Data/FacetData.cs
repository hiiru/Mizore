using System.Collections;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler.Data
{
    public class FacetData
    {
        public FacetData(INamedList responseHeader)
        {
            if (responseHeader.IsNullOrEmpty()) return;
            Queries = responseHeader.GetOrDefault<INamedList>("facet_queries");

            var fieldsList = responseHeader.GetOrDefault<INamedList>("facet_fields");
            if (fieldsList != null)
            {
                Fields=new NamedList();
                for (int i = 0; i < fieldsList.Count; i++)
                {
                    var innerList = fieldsList.GetOrDefault<IList>(i);
                    if (innerList == null) continue;
                    var list = new NamedList();
                    for (int j = 0; j < innerList.Count; j = j + 2)
                    {
                        var valKey = innerList[j] as string;
                        if (valKey != null)
                            list.Add(valKey, innerList[j + 1]);
                    }

                    Fields.Add(fieldsList.GetKey(i), list);
                }
            }

            //Fields = responseHeader.GetOrDefault<INamedList>("facet_fields");
            Dates = responseHeader.GetOrDefault<INamedList>("facet_dates");
            Ranges = responseHeader.GetOrDefault<INamedList>("facet_ranges");
        }

        public INamedList Queries;
        public INamedList Fields;
        public INamedList Dates;
        public INamedList Ranges;
    }
}
