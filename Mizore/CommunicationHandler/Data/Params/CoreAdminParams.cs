/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.CoreAdminParams
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
    public static class CoreAdminParams
    {
        /// <summary>
        /// What Core are we talking about
        /// </summary>
        public const string CORE = "core";

        /// <summary>
        /// Should the STATUS request include index info
        /// </summary>
        public const string INDEX_INFO = "indexInfo";

        /// <summary>
        /// Persistent -- should it save the cores state?
        /// </summary>
        public const string PERSISTENT = "persistent";

        /// <summary>
        /// If you rename something, what is the new name
        /// </summary>
        public const string NAME = "name";

        /// <summary>
        /// Core data directory
        /// </summary>
        public const string DATA_DIR = "dataDir";

        /// <summary>
        /// Core updatelog directory
        /// </summary>
        public const string ULOG_DIR = "ulogDir";

        /// <summary>
        /// Name of the other core in actions involving 2 cores
        /// </summary>
        public const string OTHER = "other";

        /// <summary>
        /// What action
        /// </summary>
        public const string ACTION = "action";

        /// <summary>
        /// If you specify a schema, what is its name
        /// </summary>
        public const string SCHEMA = "schema";

        /// <summary>
        /// If you specify a configset, what is its name
        /// </summary>
        public const string CONFIGSET = "configSet";

        /// <summary>
        /// If you specify a config, what is its name
        /// </summary>
        public const string CONFIG = "config";

        /// <summary>
        // /Specifies a core instance dir.
        /// </summary>
        public const string INSTANCE_DIR = "instanceDir";

        /// <summary>
        /// If you specify a file, what is its name
        /// </summary>
        public const string FILE = "file";

        /// <summary>
        /// If you merge indexes, what are the index directories.
        /// The directories are specified by multiple indexDir parameters.
        /// </summary>
        public const string INDEX_DIR = "indexDir";

        /// <summary>
        /// If you merge indexes, what is the source core's name
        /// More than one source core can be specified by multiple srcCore parameters
        /// </summary>
        public const string SRC_CORE = "srcCore";

        /// <summary>
        /// The collection name in solr cloud
        /// </summary>
        public const string COLLECTION = "collection";

        /// <summary>
        /// The shard id in solr cloud
        /// </summary>
        public const string SHARD = "shard";

        /// <summary>
        /// The shard range in solr cloud
        /// </summary>
        public const string SHARD_RANGE = "shard.range";

        /// <summary>
        ///The shard range in solr cloud
        /// </summary>
        public const string SHARD_STATE = "shard.state";

        /// <summary>
        ///The parent shard if applicable
        /// </summary>
        public const string SHARD_PARENT = "shard.parent";

        /// <summary>
        ///The target core to which a split index should be written to
        /// Multiple targetCores can be specified by multiple targetCore parameters
        /// </summary>
        public const string TARGET_CORE = "targetCore";

        /// <summary>
        ///The hash ranges to be used to split a shard or an index
        /// </summary>
        public const string RANGES = "ranges";

        public const string ROLES = "roles";

        public const string REQUESTID = "requestid";

        public const string CORE_NODE_NAME = "coreNodeName";

        /// <summary>
        ///Prefix for core property name=value pair
        /// </summary>
        public const string PROPERTY_PREFIX = "property.";

        /// <summary>
        ///If you unload a core, delete the index too
        /// </summary>
        public const string DELETE_INDEX = "deleteIndex";

        public const string DELETE_DATA_DIR = "deleteDataDir";

        public const string DELETE_INSTANCE_DIR = "deleteInstanceDir";

        public const string LOAD_ON_STARTUP = "loadOnStartup";

        public const string TRANSIENT = "transient";

        public enum CoreAdminAction
        {
            STATUS,
            LOAD,
            UNLOAD,
            RELOAD,
            CREATE,
            PERSIST,
            SWAP,
            RENAME,
            MERGEINDEXES,
            SPLIT,
            PREPRECOVERY,
            REQUESTRECOVERY,
            REQUESTSYNCSHARD,
            CREATEALIAS,
            DELETEALIAS,
            REQUESTBUFFERUPDATES,
            REQUESTAPPLYUPDATES,
            LOAD_ON_STARTUP,
            TRANSIENT,
            OVERSEEROP,
            REQUESTSTATUS
        }
    }
}