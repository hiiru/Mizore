/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.ShardParams
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
    /// <summary>
    /// Parameters used for distributed search.
    /// </summary>
    public static class ShardParams
    {/// <summary>
        /// the shards to use (distributed configuration)
        /// </summary>
        public const string SHARDS = "shards";

        /// <summary>
        /// per-shard start and rows
        /// </summary>
        public const string SHARDS_ROWS = "shards.rows";

        public const string SHARDS_START = "shards.start";

        /// <summary>
        /// IDs of the shard documents
        /// </summary>
        public const string IDS = "ids";

        /// <summary>
        /// whether the request goes to a shard
        /// </summary>
        public const string IS_SHARD = "isShard";

        /// <summary>
        /// The requested URL for this shard
        /// </summary>
        public const string SHARD_URL = "shard.url";

        /// <summary>
        /// The Request Handler for shard requests
        /// </summary>
        public const string SHARDS_QT = "shards.qt";

        /// <summary>
        /// Request detailed match info for each shard (true/false)
        /// </summary>
        public const string SHARDS_INFO = "shards.info";

        /// <summary>
        /// Should things fail if there is an error? (true/false)
        /// </summary>
        public const string SHARDS_TOLERANT = "shards.tolerant";

        /// <summary>
        /// Should things fail if there is an error? (true/false)
        /// </summary>
        [Obsolete]
        public const string SHARD_KEYS = "shard.keys";

        public const string _ROUTE_ = "_route_";
    }
}