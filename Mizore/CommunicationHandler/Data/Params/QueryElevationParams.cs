/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.QueryElevationParams
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
    /// Parameters used with the QueryElevationComponent
    /// </summary>
    public static class QueryElevationParams
    {
        public const string ENABLE = "enableElevation";
        public const string EXCLUSIVE = "exclusive";
        public const string FORCE_ELEVATION = "forceElevation";
        public const string IDS = "elevateIds";
        public const string EXCLUDE = "excludeIds";
        /// <summary>
        /// The name of the field that editorial results will be written out as when using the QueryElevationComponent, which
        /// automatically configures the EditorialMarkerFactory.  The default name is "elevated"
        /// <br/>
        /// See http://wiki.apache.org/solr/DocTransformers

        /// </summary>
        public const string EDITORIAL_MARKER_FIELD_NAME = "editorialMarkerFieldName";

        /// <summary>
        /// The name of the field that excluded editorial results will be written out as when using the QueryElevationComponent, which
        /// automatically configures the EditorialMarkerFactory.  The default name is "excluded".  This is only used
        /// when {@link #MARK_EXCLUDES} is set to true at query time.
        /// <br/>
        /// See http://wiki.apache.org/solr/DocTransformers

        /// </summary>
        public const string EXCLUDE_MARKER_FIELD_NAME = "excludeMarkerFieldName";

        /// <summary>
        /// Instead of removing excluded items from the results, passing in this parameter allows you to get back the excluded items, but to mark them
        /// as excluded.

        /// </summary>
        public const string MARK_EXCLUDES = "markExcludes";
    }
}