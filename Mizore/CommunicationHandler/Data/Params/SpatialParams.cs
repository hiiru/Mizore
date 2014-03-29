﻿/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.SpatialParams
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
    public static class SpatialParams
    {
        public const string POINT = "pt";
        public const string DISTANCE = "d";
        public const string FIELD = "sfield";  // the field that contains the points we are measuring from "pt"
        /// <summary>
        /// km - kilometers
        /// mi - miles

        /// </summary>
        public const string UNITS = "units";

        /// <summary>
        /// The distance measure to use.

        /// </summary>
        public const string MEASURE = "meas";

        /// <summary>
        /// The radius of the sphere to use to in calculating spherical distances like Haversine

        /// </summary>
        public const string SPHERE_RADIUS = "sphere_radius";
    }
}