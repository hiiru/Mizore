/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.DisMaxParams
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
    /// A collection of params used in DisMaxRequestHandler, both for Plugin initialization and for Requests.
    /// </summary>
    public static class DisMaxParams
    {
        /// <summary>
        /// query and init param for tiebreaker value
        /// </summary>
        public const string TIE = "tie";

        /// <summary>
        /// query and init param for query fields
        /// </summary>
        public const string QF = "qf";

        /// <summary>
        /// query and init param for phrase boost fields
        /// </summary>
        public const string PF = "pf";

        /// <summary>
        /// query and init param for bigram phrase boost fields
        /// </summary>
        public const string PF2 = "pf2";

        /// <summary>
        /// query and init param for trigram phrase boost fields
        /// </summary>
        public const string PF3 = "pf3";

        /// <summary>
        /// query and init param for MinShouldMatch specification
        /// </summary>
        public const string MM = "mm";

        /// <summary>
        /// query and init param for Phrase Slop value in phrase
        /// boost query (in pf fields)
        /// </summary>
        public const string PS = "ps";

        /// <summary>
        /// default phrase slop for bigram phrases (pf2)
        /// </summary>
        public const string PS2 = "ps2";

        /// <summary>
        /// default phrase slop for bigram phrases (pf3)
        /// </summary>
        public const string PS3 = "ps3";

        /// <summary>
        /// query and init param for phrase Slop value in phrases
        /// explicitly included in the user's query string ( in qf fields)
        /// </summary>
        public const string QS = "qs";

        /// <summary>
        /// query and init param for boosting query
        /// </summary>
        public const string BQ = "bq";

        /// <summary>
        /// query and init param for boosting functions
        /// </summary>
        public const string BF = "bf";

        /// <summary>
        /// Alternate query (expressed in Solr QuerySyntax)
        /// to use if main query (q) is empty
        /// </summary>
        public const string ALTQ = "q.alt";

        /// <summary>
        /// query and init param for field list
        /// </summary>
        public const string GEN = "gen";
    }
}