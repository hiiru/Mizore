using System;
using System.Collections.Generic;
using System.Text;
//TODO: in which namespace should this class be?
using Mizore.CommunicationHandler.Data.Params;

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
            Query = new Dictionary<string, string>();
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

        public SolrUriBuilder GetBuilder(string core, string handler)
        {
            return new SolrUriBuilder(this, core, handler);
        }

        public bool IsBaseUrl { get; protected set; }

        /// <summary>
        /// Solr Server Address (without core/handler/querystring)
        /// </summary>
        public string ServerAddress { get; protected set; }

        protected string _core;

        public string Core
        {
            get
            {
                return _core;
            }
            protected set
            {
                if (IsBaseUrl) throw new InvalidOperationException("Core can't be set on BaseUrl");
                if (_core != null) throw new InvalidOperationException("Core can only be set once!");
                if (value == null) value = "";
                _core = value;
            }
        }

        protected string _handler;

        public string Handler
        {
            get
            {
                return _handler;
            }
            protected set
            {
                if (IsBaseUrl) throw new InvalidOperationException("Handler can't be set on BaseUrl");
                if (_handler != null) throw new InvalidOperationException("Handler can only be set once!");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("value");
                _handler = value;
            }
        }

        public Dictionary<string, string> Query { get; set; }

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
            foreach (var item in Query)
            {
                if (sbQuery.Length > 0) sbQuery.Append('&');
                sbQuery.AppendFormat("{0}={1}", item.Key, item.Value);
            }
            return sbQuery.ToString();
        }

        public void SetQuery(IQueryBuilder queryBuilder)
        {
            if (queryBuilder == null)
                throw new ArgumentNullException("queryBuilder");
            if (queryBuilder.QueryParameters.IsNullOrEmpty())
                throw new ArgumentException("QueryParameters are empty", "queryBuilder");
            foreach (var parameter in queryBuilder.QueryParameters)
            {
                if (parameter.Key.Equals(CommonParams.WT, StringComparison.InvariantCultureIgnoreCase)) throw new InvalidOperationException("wt parameter is not allowed in the QueryParameters");
                Query[parameter.Key] = parameter.Value;
            }
        }
    }
}