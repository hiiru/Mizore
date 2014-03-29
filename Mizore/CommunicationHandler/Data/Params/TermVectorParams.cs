/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.TermVectorParams
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
    public static class TermVectorParams
    {
        public const string TV_PREFIX = "tv.";

        /// <summary>
        /// Return Term Frequency info
        ///
        /// </summary>
        public const string TF = TV_PREFIX + "tf";

        /// <summary>
        /// Return Term Vector position information
        ///
        /// </summary>
        public const string POSITIONS = TV_PREFIX + "positions";

        /// <summary>
        /// Return offset information, if available
        ///
        /// </summary>
        public const string OFFSETS = TV_PREFIX + "offsets";

        /// <summary>
        /// Return IDF information.  May be expensive
        ///
        /// </summary>
        public const string DF = TV_PREFIX + "df";

        /// <summary>
        /// Return TF-IDF calculation, i.e. (tf / idf).  May be expensive.

        /// </summary>
        public const string TF_IDF = TV_PREFIX + "tf_idf";

        /// <summary>
        /// Return all the options: TF, positions, offsets, idf

        /// </summary>
        public const string ALL = TV_PREFIX + "all";

        /// <summary>
        /// The fields to get term vectors for

        /// </summary>
        public const string FIELDS = TV_PREFIX + "fl";

        /// <summary>
        /// The Doc Ids (Lucene internal ids) of the docs to get the term vectors for

        /// </summary>
        public const string DOC_IDS = TV_PREFIX + "docIds";
    }
}