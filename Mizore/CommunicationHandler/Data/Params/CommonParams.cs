/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.CommonParams
 * 
 * Original License text:
 * 
 * Licensed to the Apache Software Foundation (ASF) under one or more
 * contributor license agreements.  See the NOTICE file distributed with
 * this work for additional information regarding copyright ownership.
 * The ASF licenses this file to You under the Apache License, Version 2.0
 * (the "License"); you may not use this file except in compliance with
 * the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace Mizore.CommunicationHandler.Data.Params
{
    /// <summary>
    /// Parameters used across many handlers
    /// </summary>
    public static class CommonParams
    {
        /// <summary>
        /// Override for the concept of "NOW" to be used throughout this request,
        /// expressed as milliseconds since epoch.  This is primarily used in
        /// distributed search to ensure consistent time values are used across
        /// multiple sub-requests.
        /// </summary>
        public const string NOW = "NOW";

        /// <summary>
        /// Specifies the TimeZone used by the client for the purposes of
        /// any DateMath rounding that may take place when executing the request
        /// </summary>
        public const string TZ = "TZ";

        /// <summary>
		/// the Request Handler (formerly known as the Query Type) - which Request Handler should handle the request
		/// </summary>
        public const string QT = "qt";

        /// <summary>
		/// the response writer type - the format of the response
		/// </summary>
        public const string WT = "wt";

        /// <summary>
		/// query string
		/// </summary>
        public const string Q = "q";

        /// <summary>
		/// distrib string
		/// </summary>
        public const string DISTRIB = "distrib";

        /// <summary>
		/// sort order
		/// </summary>
        public const string SORT = "sort";

        /// <summary>
		/// Lucene query string(s) for filtering the results without affecting scoring
		/// </summary>
        public const string FQ = "fq";

        /// <summary>
		/// zero based offset of matching documents to retrieve
		/// </summary>
        public const string START = "start";

        /// <summary>
		/// number of documents to return starting at "start"
		/// </summary>
        public const string ROWS = "rows";

        // SOLR-4228 start
        /// <summary>
		/// handler value for SolrPing
		/// </summary>
        public const string PING_HANDLER = "/admin/ping";

        /// <summary>
		/// "action" parameter for SolrPing
		/// </summary>
        public const string ACTION = "action";

        /// <summary>
		/// "disable" value for SolrPing action
		/// </summary>
        public const string DISABLE = "disable";

        /// <summary>
		/// "enable" value for SolrPing action
		/// </summary>
        public const string ENABLE = "enable";

        /// <summary>
		/// "ping" value for SolrPing action
		/// </summary>
        public const string PING = "ping";
        // SOLR-4228 end

        /// <summary>
		/// stylesheet to apply to XML results
		/// </summary>
        public const string XSL = "xsl";

        /// <summary>
		/// version parameter to check request-response compatibility
		/// </summary>
        public const string VERSION = "version";

        /// <summary>
		/// query and init param for field list
		/// </summary>
        public const string FL = "fl";

        /// <summary>
		/// default query field
		/// </summary>
        public const string DF = "df";

        /// <summary>
		/// Transformer param -- used with XSLT
		/// </summary>
        public const string TR = "tr";

        /// <summary>
        /// whether to include debug data for all components pieces, including doing explains
        /// </summary>
        public const string DEBUG_QUERY = "debugQuery";

        /// <summary>
        /// Whether to provide debug info for specific items.
        /// </summary>
        public const string DEBUG = "debug";

        /// <summary>
        /// {@link #DEBUG} value indicating an interest in debug output related to timing
        /// </summary>
        public const string TIMING = "timing";
        /// <summary>
        /// {@link #DEBUG} value indicating an interest in debug output related to the results (explains)
        /// </summary>
        public const string RESULTS = "results";
        /// <summary>
        /// {@link #DEBUG} value indicating an interest in debug output related to the Query (parsing, etc.)
        /// </summary>
        public const string QUERY = "query";
        /// <summary>
        /// {@link #DEBUG} value indicating an interest in debug output related to the distributed tracking
        /// </summary>
        public const string TRACK = "track";
        /// <summary>
        /// boolean indicating whether score explanations should structured (true),
        /// or plain text (false)
        /// </summary>
        public const string EXPLAIN_STRUCT = "debug.explain.structured";

        /// <summary>
		/// another query to explain against
		/// </summary>
        public const string EXPLAIN_OTHER = "explainOther";

        /// <summary>
		/// If the content stream should come from a URL (using URLConnection)
		/// </summary>
        public const string STREAM_URL = "stream.url";

        /// <summary>
		/// If the content stream should come from a File (using FileReader)
		/// </summary>
        public const string STREAM_FILE = "stream.file";

        /// <summary>
		/// If the content stream should come directly from a field
		/// </summary>
        public const string STREAM_BODY = "stream.body";

        /// <summary>
        /// Explicitly set the content type for the input stream
        /// If multiple streams are specified, the explicit contentType
        /// will be used for all of them.
        /// </summary>
        public const string STREAM_CONTENTTYPE = "stream.contentType";

        /// <summary>
        /// Timeout value in milliseconds.  If not set, or the value is <= 0, there is no timeout.
        /// </summary>
        public const string TIME_ALLOWED = "timeAllowed";

        /// <summary>
		/// 'true' if the header should include the handler name
		/// </summary>
        public const string HEADER_ECHO_HANDLER = "echoHandler";

        /// <summary>
        /// include the parameters in the header
        /// </summary>
        public const string HEADER_ECHO_PARAMS = "echoParams";

        /// <summary>
		/// include header in the response
		/// </summary>
        public const string OMIT_HEADER = "omitHeader";

        /// <summary>
        /// valid values for: <code>echoParams</code>
        /// </summary>
        public enum EchoParamStyle
        {
            NONE,
            EXPLICIT,
            ALL,
        }
        
        /// <summary>
        /// which parameters to log (if not supplied all parameters will be logged)
        /// </summary>
        public const string LOG_PARAMS_LIST = "logParamsList";

        public const string EXCLUDE = "ex";
        public const string TAG = "tag";
        public const string TERMS = "terms";
        public const string OUTPUT_KEY = "key";
        public const string FIELD = "f";
        public const string VALUE = "v";
        public const string THREADS = "threads";
        public const string TRUE = "true";
        public const string FALSE = "false";

        /// <summary>
		/// Used as a local parameter on queries.  cache=false means don't check any query or filter caches.
        /// cache=true is the default.
        /// </summary>
        public const string CACHE = "cache";

        /// <summary>
		/// Used as a local param on filter queries in conjunction with cache=false.  Filters are checked in order, from
        /// smallest cost to largest. If cost>=100 and the query implements PostFilter, then that interface will be used to do post query filtering.
        /// </summary>
        public const string COST = "cost";

        /// <summary>
        /// Request ID parameter added to the request when using debug=track
        /// </summary>
        public const string REQUEST_ID = "rid";

        /// <summary>
        /// Request Purpose parameter added to each internal shard request when using debug=track
        /// </summary>
        public const string REQUEST_PURPOSE = "requestPurpose";
    }
}