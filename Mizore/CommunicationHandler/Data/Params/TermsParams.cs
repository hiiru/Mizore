/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.TermsParams
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

using System;

namespace Mizore.CommunicationHandler.Data.Params
{
    public static class TermsParams
    {
        /// <summary>
        /// The component name.  Set to true to turn on the TermsComponent

        /// </summary>
        public const string TERMS = "terms";

        /// <summary>
        /// Used for building up the other terms

        /// </summary>
        public const string TERMS_PREFIX = TERMS + ".";

        /// <summary>
        /// Required.  Specify the field to look up terms in.

        /// </summary>
        public const string TERMS_FIELD = TERMS_PREFIX + "fl";

        /// <summary>
        /// Optional.  The lower bound term to start at.  The TermEnum will start at the next term after this term in the dictionary.
        /// If not specified, the empty string is used

        /// </summary>
        public const string TERMS_LOWER = TERMS_PREFIX + "lower";

        /// <summary>
        /// Optional.  The term to stop at.
        /// @see #TERMS_UPPER_INCLUSIVE

        /// </summary>
        public const string TERMS_UPPER = TERMS_PREFIX + "upper";

        /// <summary>
        /// Optional.  If true, include the upper bound term in the results.  False by default.

        /// </summary>
        public const string TERMS_UPPER_INCLUSIVE = TERMS_PREFIX + "upper.incl";

        /// <summary>
        /// Optional.  If true, include the lower bound term in the results, otherwise skip to the next one.  True by default.

        /// </summary>
        public const string TERMS_LOWER_INCLUSIVE = TERMS_PREFIX + "lower.incl";

        /// <summary>
        /// Optional.  The number of results to return.  If not specified, looks for {@link org.apache.solr.common.params.CommonParams#ROWS}.  If that's not specified, uses 10.

        /// </summary>
        public const string TERMS_LIMIT = TERMS_PREFIX + "limit";

        public const string TERMS_PREFIX_STR = TERMS_PREFIX + "prefix";

        public const string TERMS_REGEXP_STR = TERMS_PREFIX + "regex";

        public const string TERMS_REGEXP_FLAG = TERMS_REGEXP_STR + ".flag";

        [Flags]
        public enum TermsRegexpFlag
        {
            UNIX_LINES = 0x01,
            CASE_INSENSITIVE = 0x02,
            COMMENTS = 0x04,
            MULTILINE = 0x08,
            LITERAL = 0x10,
            DOTALL = 0x20,
            UNICODE_CASE = 0x40,
            CANON_EQ = 0x80
        }

        /// <summary>
        /// Optional.  The minimum value of docFreq to be returned.  1 by default

        /// </summary>
        public const string TERMS_MINCOUNT = TERMS_PREFIX + "mincount";

        /// <summary>
        /// Optional.  The maximum value of docFreq to be returned.  -1 by default means no boundary

        /// </summary>
        public const string TERMS_MAXCOUNT = TERMS_PREFIX + "maxcount";

        /// <summary>
        /// Optional.  If true, return the raw characters of the indexed term, regardless of if it is readable.
        /// For instance, the index form of numeric numbers is not human readable.  The default is false.

        /// </summary>
        public const string TERMS_RAW = TERMS_PREFIX + "raw";

        /// <summary>
        /// Optional.  If sorting by frequency is enabled.  Defaults to sorting by count.

        /// </summary>
        public const string TERMS_SORT = TERMS_PREFIX + "sort";

        public const string TERMS_SORT_COUNT = "count";
        public const string TERMS_SORT_INDEX = "index";
    }
}