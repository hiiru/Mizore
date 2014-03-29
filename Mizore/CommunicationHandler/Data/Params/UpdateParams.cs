/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.UpdateParams
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
    /// A collection of standard params used by Update handlers
    /// </summary>
    public static class UpdateParams
    {
        /// <summary>
        /// Open up a new searcher as part of a commit
        /// </summary>
        public const string OPEN_SEARCHER = "openSearcher";

        /// <summary>
        /// wait for the searcher to be registered/visible
        /// </summary>
        public const string WAIT_SEARCHER = "waitSearcher";

        public const string SOFT_COMMIT = "softCommit";

        /// <summary>
        /// overwrite indexing fields
        /// </summary>
        public const string OVERWRITE = "overwrite";

        /// <summary>
        /// Commit everything after the command completes
        /// </summary>
        public const string COMMIT = "commit";

        /// <summary>
        /// Commit within a certain time period (in ms)
        /// </summary>
        public const string COMMIT_WITHIN = "commitWithin";

        /// <summary>
        /// Optimize the index and commit everything after the command completes
        /// </summary>
        public const string OPTIMIZE = "optimize";

        /// <summary>
        /// expert: calls IndexWriter.prepareCommit
        /// </summary>
        public const string PREPARE_COMMIT = "prepareCommit";

        /// <summary>
        /// Rollback update commands
        /// </summary>
        public const string ROLLBACK = "rollback";

        /// <summary>
        /// Select the update processor chain to use.  A RequestHandler may or may not respect this parameter
        /// </summary>
        public const string UPDATE_CHAIN = "update.chain";

        /// <summary>
        /// Override the content type used for UpdateLoader
        /// </summary>
        public const string ASSUME_CONTENT_TYPE = "update.contentType";

        /// <summary>
        /// If optimizing, set the maximum number of segments left in the index after optimization.  1 is the default (and is equivalent to calling IndexWriter.optimize() in Lucene).
        /// </summary>
        public const string MAX_OPTIMIZE_SEGMENTS = "maxSegments";

        public const string EXPUNGE_DELETES = "expungeDeletes";

        /// <summary>
        /// Return versions of updates?
        /// </summary>
        public const string VERSIONS = "versions";
    }
}