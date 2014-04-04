using System;
using System.Collections.Generic;
using Mizore.CommunicationHandler.Data.Params;
using Mizore.CommunicationHandler.ResponseHandler;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler;
using Mizore.Exceptions;

namespace Mizore.CommunicationHandler.RequestHandler
{
    /// <summary>
    /// Solr Update Request
    /// This request handes Add, Delete, Commit and Optimize
    ///
    /// Atomic Updates aren't currently implemented, but will come in future
    ///
    /// Note: those deprecated attributes won't be implemented because those are deprecated or got removed, if you NEED them: create a custom UpdateRequest
    /// Add: allowDups, overwritePending and overwriteCommitted  -> use overwrite
    /// Delete: fromPending,fromCommitted  -> both are deprecated
    /// Commit / Optimize: waitFlush -> removed in solr 4.0
    ///
    /// Expert-level options:
    /// Rollback and prepareCommet aren't currently implementet nor planned, if you need them, feel free to to add them and send a pull request
    ///
    /// Alternative Update (GET, stream.body):
    /// Currently those aren't planned, however if you want them, please implement and send a pull request :)
    ///
    /// This Class is based upon http://wiki.apache.org/solr/UpdateXmlMessages and SolrJ
    /// </summary>
    public class UpdateRequest : IRequest
    {
        public UpdateRequest(SolrUriBuilder urlBuilder)
        {
            if (urlBuilder == null) throw new ArgumentNullException("urlBuilder");
            urlBuilder.Handler = "update";
            urlBuilder.Query.Add(CommonParams.WT, "json");
            UrlBuilder = urlBuilder;
        }

        protected struct CommitOptimizeOptions
        {
            public bool Optimize;
            public bool? waitSearcher;
            public bool? softCommit;
            public bool? expungeDeletes;
            public int? maxSegments;
        }

        protected CommitOptimizeOptions? _CommitOptimizeOptions;

        /// <summary>
        /// Document Id's to delete.
        /// </summary>
        protected List<string> _deleteIds;

        /// <summary>
        /// Delete Queries for the Update Request
        /// </summary>
        protected List<string> _deleteQueries;

        /// <summary>
        /// Documents to Add or Update on the solr server
        /// </summary>
        protected List<SolrInputDocument> _documents;

        protected int? _commitWithin;

        public int? CommitWithin
        {
            get
            {
                return _commitWithin;
            }
            set
            {
                _commitWithin = value;
                _changed = true;
            }
        }

        protected bool? _overwrite;

        public bool? Overwrite
        {
            get
            {
                return _overwrite;
            }
            set
            {
                _overwrite = value;
                _changed = true;
            }
        }

        public void Clear()
        {
            _deleteIds = null;
            _deleteQueries = null;
            _documents = null;
        }

        #region Delete

        public UpdateRequest DeleteById(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException("id");

            if (_deleteIds == null)
                _deleteIds = new List<string>();

            _deleteIds.Add(id);
            _changed = true;
            return this;
        }

        public UpdateRequest DeleteByIds(ICollection<string> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            if (_deleteIds == null)
                _deleteIds = new List<string>();
            foreach (var id in ids)
            {
                if (string.IsNullOrWhiteSpace(id))
                    continue;
                _deleteIds.Add(id);
            }
            _changed = true;
            return this;
        }

        public UpdateRequest DeleteByQuery(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException("query");

            if (_deleteQueries == null)
                _deleteQueries = new List<string>();
            _deleteQueries.Add(query);
            _changed = true;
            return this;
        }

        public UpdateRequest DeleteByQueries(ICollection<string> queries)
        {
            if (queries == null)
                throw new ArgumentNullException("queries");

            if (_deleteQueries == null)
                _deleteQueries = new List<string>();
            foreach (var query in queries)
            {
                if (string.IsNullOrWhiteSpace(query))
                    continue;
                _deleteQueries.Add(query);
            }
            _changed = true;
            return this;
        }

        #endregion Delete

        #region Documents (Add or Update)

        public UpdateRequest Add(SolrInputDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException("doc");

            if (_documents == null)
                _documents = new List<SolrInputDocument>();
            _documents.Add(doc);
            _changed = true;
            return this;
        }

        public UpdateRequest Add(IList<SolrInputDocument> docs)
        {
            if (docs == null)
                throw new ArgumentNullException("docs");

            if (_documents == null)
                _documents = new List<SolrInputDocument>();
            foreach (var doc in docs)
            {
                if (doc == null) continue;
                _documents.Add(doc);
            }
            _changed = true;
            return this;
        }

        public UpdateRequest AddObject<T>(T obj, IDataMappingHandler mappingHandler) where T : class, new()
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            if (mappingHandler == null)
                throw new ArgumentNullException("mappingHandler");
            var handler = mappingHandler.GetMappingHandler<T>();
            if (handler == null)
                throw new MizoreException("Can't map the type " + typeof(T).Name);

            if (_documents == null)
                _documents = new List<SolrInputDocument>();
            _documents.Add(handler.GetDocument(obj));
            _changed = true;
            return this;
        }

        public UpdateRequest AddObject<T>(IList<T> objs, IDataMappingHandler mappingHandler) where T : class, new()
        {
            if (objs == null)
                throw new ArgumentNullException("objs");
            if (mappingHandler == null)
                throw new ArgumentNullException("mappingHandler");
            var handler = mappingHandler.GetMappingHandler<T>();
            if (handler == null)
                throw new MizoreException("Can't map the type " + typeof(T).Name);

            if (_documents == null)
                _documents = new List<SolrInputDocument>();

            foreach (var obj in objs)
            {
                if (obj == null) continue;
                _documents.Add(handler.GetDocument(obj));
            }
            _changed = true;
            return this;
        }

        #endregion Documents (Add or Update)

        public UpdateRequest Commit(bool? waitSearcher = null, bool? softCommit = null, bool? expungeDeletes = null)
        {
            _CommitOptimizeOptions = new CommitOptimizeOptions
            {
                Optimize = false,
                waitSearcher = waitSearcher,
                softCommit = softCommit,
                expungeDeletes = expungeDeletes
            };
            _changed = true;
            return this;
        }

        public UpdateRequest Optimize(bool? waitSearcher = null, bool? softCommit = null, int? maxSegments = null)
        {
            _CommitOptimizeOptions = new CommitOptimizeOptions
            {
                Optimize = false,
                waitSearcher = waitSearcher,
                softCommit = softCommit,
                maxSegments = maxSegments
            };
            _changed = true;
            return this;
        }

        public virtual RequestMethod Method { get { return RequestMethod.POST; } }

        public SolrUriBuilder UrlBuilder { get; private set; }

        protected bool _changed;
        protected INamedList _content;

        public virtual INamedList Content
        {
            get
            {
                if (_content == null || _changed)
                    _content = PrepareContent();
                return _content;
            }
        }

        protected SolrUpdateList PrepareContent()
        {
            INamedList deleteList = null;
            INamedList addList = null;
            INamedList commitList = null;

            if (!_deleteIds.IsNullOrEmpty())
            {
                deleteList = new NamedList();
                foreach (var deleteId in _deleteIds)
                {
                    deleteList.Add("id", deleteId);
                }
            }

            if (!_deleteQueries.IsNullOrEmpty())
            {
                if (deleteList == null)
                    deleteList = new NamedList();
                foreach (var deleteQuery in _deleteQueries)
                {
                    deleteList.Add("query", deleteQuery);
                }
            }

            if (!_documents.IsNullOrEmpty())
            {
                addList = new NamedList();
                if (_overwrite.HasValue)
                    addList.Add("overwrite", _overwrite.Value);
                if (_commitWithin.HasValue)
                    addList.Add("commitWithin", _commitWithin.Value);
                addList.Add("doc", new List<SolrInputDocument>(_documents));
            }
            if (_CommitOptimizeOptions.HasValue)
            {
                commitList = new NamedList();
                var innerCommitList = new NamedList();
                var options = _CommitOptimizeOptions.Value;
                if (options.waitSearcher.HasValue)
                    innerCommitList.Add("waitSearcher", options.waitSearcher.Value);
                if (options.softCommit.HasValue)
                    innerCommitList.Add("softCommit", options.softCommit.Value);
                if (_CommitOptimizeOptions.Value.Optimize)
                {
                    if (options.maxSegments.HasValue)
                        innerCommitList.Add("maxSegments", options.maxSegments.Value);
                    commitList.Add("optimize", innerCommitList);
                }
                else
                {
                    if (options.expungeDeletes.HasValue)
                        innerCommitList.Add("expungeDeletes", options.expungeDeletes.Value);
                    commitList.Add("commit", innerCommitList);
                }
            }
            _changed = false;
            return new SolrUpdateList(addList, deleteList, commitList);
        }

        public Dictionary<string, string> Header { get; protected set; }

        public IResponse GetResponse(INamedList nl)
        {
            return new UpdateResponse(this, nl);
        }
    }
}