/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.MoreLikeThisParams
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
    public static class MoreLikeThisParams
    {
        // enable more like this -- this only applies to 'StandardRequestHandler' maybe DismaxRequestHandler
        public const string MLT = "mlt";

        public const string PREFIX = "mlt.";

        public const string SIMILARITY_FIELDS = PREFIX + "fl";
        public const string MIN_TERM_FREQ = PREFIX + "mintf";
        public const string MAX_DOC_FREQ = PREFIX + "maxdf";
        public const string MIN_DOC_FREQ = PREFIX + "mindf";
        public const string MIN_WORD_LEN = PREFIX + "minwl";
        public const string MAX_WORD_LEN = PREFIX + "maxwl";
        public const string MAX_QUERY_TERMS = PREFIX + "maxqt";
        public const string MAX_NUM_TOKENS_PARSED = PREFIX + "maxntp";
        public const string BOOST = PREFIX + "boost"; // boost or not?
        public const string QF = PREFIX + "qf"; //boosting applied to mlt fields

        // the /mlt request handler uses 'rows'
        public const string DOC_COUNT = PREFIX + "count";

        // Do you want to include the original document in the results or not
        public const string MATCH_INCLUDE = PREFIX + "match.include";

        // If multiple docs are matched in the query, what offset do you want?
        public const string MATCH_OFFSET = PREFIX + "match.offset";

        // Do you want to include the original document in the results or not
        public const string INTERESTING_TERMS = PREFIX + "interestingTerms";  // false,details,(list or true)

        public enum TermStyle
        {
            NONE,
            LIST,
            DETAILS
        }
    }
}