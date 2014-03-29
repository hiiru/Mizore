/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.GroupParams
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
    /// Group parameters
    /// </summary>
    public static class GroupParams
    {
        public const string GROUP = "group";

        public const string GROUP_QUERY = GROUP + ".query";
        public const string GROUP_FIELD = GROUP + ".field";
        public const string GROUP_FUNC = GROUP + ".func";
        public const string GROUP_SORT = GROUP + ".sort";

        /// <summary>
        /// the limit for the number of documents in each group
        /// </summary>
        public const string GROUP_LIMIT = GROUP + ".limit";

        /// <summary>
        /// the offset for the doclist of each group
        /// </summary>
        public const string GROUP_OFFSET = GROUP + ".offset";

        /// <summary>
        /// treat the first group result as the main result.  true/false
        /// </summary>
        public const string GROUP_MAIN = GROUP + ".main";

        /// <summary>
        /// treat the first group result as the main result.  true/false
        /// </summary>
        public const string GROUP_FORMAT = GROUP + ".format";

        /// <summary>
        /// Whether to cache the first pass search (doc ids and score) for the second pass search.
        /// Also defines the maximum size of the group cache relative to maxdoc in a percentage.
        /// Values can be a positive integer, from 0 till 100. A value of 0 will disable the group cache.
        /// The default is 0.
        /// </summary>
        public const string GROUP_CACHE_PERCENTAGE = GROUP + ".cache.percent";

        // Note: Since you can supply multiple fields to group on, but only have a facets for the whole result. It only makes
        // sense to me to support these parameters for the first group.
        /// <summary>
        /// Whether the docSet (for example for faceting) should be based on plain documents (a.k.a UNGROUPED) or on the groups (a.k.a GROUPED).
        /// The docSet will only the most relevant documents per group. It is if you query for everything with group.limit=1
        /// </summary>
        public const string GROUP_TRUNCATE = GROUP + ".truncate";

        /// <summary>
        /// Whether the group count should be included in the response.
        /// </summary>
        public const string GROUP_TOTAL_COUNT = GROUP + ".ngroups";

        /// <summary>
        /// Whether to compute grouped facets based on the first specified group.
        /// </summary>
        public const string GROUP_FACET = GROUP + ".facet";

        /// <summary>
        /// Retrieve the top search groups (top group values) from the shards being queried.
        /// </summary>
        public const string GROUP_DISTRIBUTED_FIRST = GROUP + ".distributed.first";

        /// <summary>
        /// Retrieve the top groups from the shards being queries based on the specified search groups in
        /// the {@link #GROUP_DISTRIBUTED_TOPGROUPS_PREFIX} parameters.

        /// </summary>
        public const string GROUP_DISTRIBUTED_SECOND = GROUP + ".distributed.second";

        public const string GROUP_DISTRIBUTED_TOPGROUPS_PREFIX = GROUP + ".topgroups.";
    }
}