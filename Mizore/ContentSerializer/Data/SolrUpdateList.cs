using System;
using System.Collections.Generic;

namespace Mizore.ContentSerializer.Data
{
    /// <summary>
    /// This is a object containg all required Update Request date.
    /// Bacause the structure of a XML and JSON is quite different, this object can be used for detection.
    /// Note: this isn't a normal Named List, the index is "faked" and can change after each modification.
    /// </summary>
    public class SolrUpdateList : INamedList
    {
        private readonly INamedList ListDelete;
        private readonly INamedList ListAdd;
        private readonly INamedList ListCommit;

        public SolrUpdateList(INamedList add = null, INamedList delete = null, INamedList commit = null)
        {
            ListAdd = add;
            ListDelete = delete;
            ListCommit = commit;
        }

        public object Get(int i)
        {
            return Get(GetKey(i));
        }

        public object Get(string key)
        {
            switch (key)
            {
                case "add":
                    return ListAdd;

                case "delete":
                    return ListDelete;

                case "commit":
                    return ListCommit;

                default:
                    return null;
            }
        }

        public IList<object> GetAll(string key)
        {
            switch (key)
            {
                case "add":
                    return ListAdd as IList<object>;

                case "delete":
                    return ListDelete as IList<object>;

                case "commit":
                    return new List<object> { ListCommit };

                default:
                    return null;
            }
        }

        public string GetKey(int i)
        {
            switch (i)
            {
                case 0:
                    return "add";

                case 1:
                    return "delete";

                case 2:
                    return "commit";

                default:
                    return null;
            }
        }

        public void Add(string name, object obj)
        {
            throw new NotSupportedException("Can't add anything to a SolrUpdateList, use the constructor!");
        }

        public int Count { get { return 3; } }
    }
}