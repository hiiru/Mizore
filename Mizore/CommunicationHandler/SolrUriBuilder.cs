using System.Collections.Specialized;
using Mizore.CommunicationHandler.Data.Params;
using System;
using System.Collections.Generic;
using System.Text;
using Mizore.ContentSerializer.Data;

namespace Mizore.CommunicationHandler
{
    public class SolrUriBuilder
    {
        #region Constructor

        public SolrUriBuilder(string uriString)
        {
            Uri uri;
            if (!Uri.TryCreate(uriString, UriKind.Absolute, out uri))
                throw new ArgumentException("uri");
            Init(uri);
        }

        public SolrUriBuilder(Uri uri)
        {
            if (uri == null)
                throw new ArgumentException("uri");
            Init(uri);
        }

        protected SolrUriBuilder(SolrUriBuilder server, string core, string handler)
        {
            if (server == null) throw new ArgumentNullException("server");
            if (!server.IsBaseUrl) throw new ArgumentException("server");
            ServerAddress = server.ServerAddress;
            Core = core;
            Handler = handler;
            Query = new NamedList<string>();
        }

        private void Init(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            var lowerScheme = uri.Scheme.ToLowerInvariant();
            switch (lowerScheme)
            {
                case "http":
                case "https":
                    break;

                default:
                    throw new ArgumentException("Unsuppored url scheme: " + uri.Scheme, "uri");
            }
            var sbAddress = new StringBuilder();
            sbAddress.AppendFormat("{0}://", lowerScheme);
            if (!string.IsNullOrWhiteSpace(uri.UserInfo))
                sbAddress.AppendFormat("{0}@", uri.UserInfo);
            sbAddress.AppendFormat("{0}:{1}/{2}", uri.Host, uri.Port, uri.AbsolutePath.Trim('/'));
            ServerAddress = sbAddress.ToString();
            IsBaseUrl = true;
        }

        #endregion Constructor

        public SolrUriBuilder GetBuilder(string core, string handler = null)
        {
            return new SolrUriBuilder(this, core, handler);
        }

        public bool IsBaseUrl { get; protected set; }

        /// <summary>
        /// Solr Server Address (without core/handler/querystring)
        /// </summary>
        public string ServerAddress { get; protected set; }

        public string Core { get; set; }

        public string Handler { get; set; }

        public NamedList<string> Query { get; set; }

        public Uri Uri { get { return new Uri(ToString(), UriKind.Absolute); } }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return Equals(obj as SolrUriBuilder);
        }

        public bool Equals(SolrUriBuilder obj)
        {
            if (obj == null) return false;
            return Uri.Equals(obj.Uri);
        }

        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}{2}{3}?{4}", ServerAddress, Core, string.IsNullOrWhiteSpace(Core) ? "" : "/", Handler, GenerateQueryString());
        }

        public string GenerateQueryString()
        {
            var sbQuery = new StringBuilder();
            for (int i=0;i<Query.Count;i++)
            {
                var key = Query.GetKey(i);
                var value = Query.Get(i);
                if (sbQuery.Length > 0) sbQuery.Append('&');
                sbQuery.AppendFormat("{0}={1}", key, value);
            }
            return sbQuery.ToString();
        }

        public void SetQuery(IQueryBuilder queryBuilder)
        {
            if (queryBuilder == null)
                throw new ArgumentNullException("queryBuilder");
            if (queryBuilder.QueryParameters.Count==0)
                throw new ArgumentException("QueryParameters are empty", "queryBuilder");
            for(int i=0;i<queryBuilder.QueryParameters.Count;i++)
            {
                var key = queryBuilder.QueryParameters.GetKey(i);
                if (key.Equals(CommonParams.WT, StringComparison.InvariantCultureIgnoreCase))
                    throw new InvalidOperationException("wt parameter is not allowed in the QueryParameters");
                var value = queryBuilder.QueryParameters.Get(i);
                Query.Add(key,value);
            }
        }
    }
}