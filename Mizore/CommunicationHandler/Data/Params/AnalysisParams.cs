/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.AnalysisParams
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
    /// Defines the request parameters used by all analysis request handlers.
    /// </summary>
    public static class AnalysisParams
    {
        /// <summary>
        /// The prefix for all parameters.
        /// </summary>
        public const string PREFIX = "analysis";

        /// <summary>
        /// Holds the query to be analyzed.
        /// </summary>
        public const string QUERY = PREFIX + ".query";

        /// <summary>
        /// Set to {@code true} to indicate that the index tokens that match query tokens should be marked as "mateched".
        /// </summary>
        public const string SHOW_MATCH = PREFIX + ".showmatch";

        //===================================== FieldAnalysisRequestHandler Params =========================================

        /// <summary>
        /// Holds the value of the field which should be analyzed.
        /// </summary>
        public const string FIELD_NAME = PREFIX + ".fieldname";

        /// <summary>
        /// Holds a comma-separated list of field types that the analysis should be peformed for.
        /// </summary>
        public const string FIELD_TYPE = PREFIX + ".fieldtype";

        /// <summary>
        /// Hodls a comma-separated list of field named that the analysis should be performed for.
        /// </summary>
        public const string FIELD_VALUE = PREFIX + ".fieldvalue";
    }
}