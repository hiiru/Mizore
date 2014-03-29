/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.FacetParams
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
    /// Facet parameters
    /// </summary>
    public static class FacetParams
    {
        /// <summary>
        /// Should facet counts be calculated?

        /// </summary>
        public const string FACET = "facet";

        /// <summary>
        /// Numeric option indicating the maximum number of threads to be used
        /// in counting facet field vales

        /// </summary>
        public const string FACET_THREADS = FACET + ".threads";

        /// <summary>
        /// What method should be used to do the faceting
        /// </summary>
        public const string FACET_METHOD = FACET + ".method";

        /// <summary>
        /// Value for FACET_METHOD param to indicate that Solr should enumerate over terms
        /// in a field to calculate the facet counts.

        /// </summary>
        public const string FACET_METHOD_enum = "enum";

        /// <summary>
        /// Value for FACET_METHOD param to indicate that Solr should enumerate over documents
        /// and count up terms by consulting an uninverted representation of the field values
        /// (such as the FieldCache used for sorting).

        /// </summary>
        public const string FACET_METHOD_fc = "fc";

        /// <summary>
        /// Value for FACET_METHOD param, like FACET_METHOD_fc but counts per-segment.

        /// </summary>
        public const string FACET_METHOD_fcs = "fcs";

        /// <summary>
        /// Any lucene formated queries the user would like to use for
        /// Facet Constraint Counts (multi-value)

        /// </summary>
        public const string FACET_QUERY = FACET + ".query";

        /// <summary>
        /// Any field whose terms the user wants to enumerate over for
        /// Facet Constraint Counts (multi-value)

        /// </summary>
        public const string FACET_FIELD = FACET + ".field";

        /// <summary>
        /// The offset into the list of facets.
        /// Can be overridden on a per field basis.

        /// </summary>
        public const string FACET_OFFSET = FACET + ".offset";

        /// <summary>
        /// Numeric option indicating the maximum number of facet field counts
        /// be included in the response for each field - in descending order of count.
        /// Can be overridden on a per field basis.

        /// </summary>
        public const string FACET_LIMIT = FACET + ".limit";

        /// <summary>
        /// Numeric option indicating the minimum number of hits before a facet should
        /// be included in the response.  Can be overridden on a per field basis.

        /// </summary>
        public const string FACET_MINCOUNT = FACET + ".mincount";

        /// <summary>
        /// Boolean option indicating whether facet field counts of "0" should
        /// be included in the response.  Can be overridden on a per field basis.

        /// </summary>
        public const string FACET_ZEROS = FACET + ".zeros";

        /// <summary>
        /// Boolean option indicating whether the response should include a
        /// facet field count for all records which have no value for the
        /// facet field. Can be overridden on a per field basis.

        /// </summary>
        public const string FACET_MISSING = FACET + ".missing";

        /// <summary>
        /// Comma separated list of fields to pivot
        ///
        /// example: author,type  (for types by author / types within author)

        /// </summary>
        public const string FACET_PIVOT = FACET + ".pivot";

        /// <summary>
        /// Minimum number of docs that need to match to be included in the sublist
        ///
        /// default value is 1

        /// </summary>
        public const string FACET_PIVOT_MINCOUNT = FACET_PIVOT + ".mincount";

        /// <summary>
        /// String option: "count" causes facets to be sorted
        /// by the count, "index" results in index order.

        /// </summary>
        public const string FACET_SORT = FACET + ".sort";

        public const string FACET_SORT_COUNT = "count";
        public const string FACET_SORT_COUNT_LEGACY = "true";
        public const string FACET_SORT_INDEX = "index";
        public const string FACET_SORT_INDEX_LEGACY = "false";

        /// <summary>
        /// Only return constraints of a facet field with the given prefix.

        /// </summary>
        public const string FACET_PREFIX = FACET + ".prefix";

        /// <summary>
        /// When faceting by enumerating the terms in a field,
        /// only use the filterCache for terms with a df >= to this parameter.

        /// </summary>
        public const string FACET_ENUM_CACHE_MINDF = FACET + ".enum.cache.minDf";

        /// <summary>
        /// Any field whose terms the user wants to enumerate over for
        /// Facet Contraint Counts (multi-value)

        /// </summary>
        public const string FACET_DATE = FACET + ".date";

        /// <summary>
        /// Date string indicating the starting point for a date facet range.
        /// Can be overriden on a per field basis.

        /// </summary>
        public const string FACET_DATE_START = FACET_DATE + ".start";

        /// <summary>
        /// Date string indicating the endinging point for a date facet range.
        /// Can be overriden on a per field basis.

        /// </summary>
        public const string FACET_DATE_END = FACET_DATE + ".end";

        /// <summary>
        /// Date Math string indicating the interval of sub-ranges for a date
        /// facet range.
        /// Can be overriden on a per field basis.

        /// </summary>
        public const string FACET_DATE_GAP = FACET_DATE + ".gap";

        /// <summary>
        /// Boolean indicating how counts should be computed if the range
        /// between 'start' and 'end' is not evenly divisible by 'gap'.  If
        /// this value is true, then all counts of ranges involving the 'end'
        /// point will use the exact endpoint specified -- this includes the
        /// 'between' and 'after' counts as well as the last range computed
        /// using the 'gap'.  If the value is false, then 'gap' is used to
        /// compute the effective endpoint closest to the 'end' param which
        /// results in the range between 'start' and 'end' being evenly
        /// divisible by 'gap'.
        /// The default is false.
        /// Can be overriden on a per field basis.

        /// </summary>
        public const string FACET_DATE_HARD_END = FACET_DATE + ".hardend";

        /// <summary>
        /// String indicating what "other" ranges should be computed for a
        /// date facet range (multi-value).
        /// Can be overriden on a per field basis.
        /// @see FacetRangeOther

        /// </summary>
        public const string FACET_DATE_OTHER = FACET_DATE + ".other";

        /// <summary>
        /// <p>
        /// Multivalued string indicating what rules should be applied to determine
        /// when the the ranges generated for date faceting should be inclusive or
        /// exclusive of their end points.
        /// </p>
        /// <p>
        /// The default value if none are specified is: [lower,upper,edge] <i>(NOTE: This is different then FACET_RANGE_INCLUDE)</i>
        /// </p>
        /// <p>
        /// Can be overriden on a per field basis.
        /// </p>
        /// @see FacetRangeInclude
        /// @see #FACET_RANGE_INCLUDE

        /// </summary>
        public const string FACET_DATE_INCLUDE = FACET_DATE + ".include";

        /// <summary>
        /// Any numerical field whose terms the user wants to enumerate over
        /// Facet Contraint Counts for selected ranges.

        /// </summary>
        public const string FACET_RANGE = FACET + ".range";

        /// <summary>
        /// Number indicating the starting point for a numerical range facet.
        /// Can be overriden on a per field basis.

        /// </summary>
        public const string FACET_RANGE_START = FACET_RANGE + ".start";

        /// <summary>
        /// Number indicating the ending point for a numerical range facet.
        /// Can be overriden on a per field basis.
        /// </summary>
        public const string FACET_RANGE_END = FACET_RANGE + ".end";

        /// <summary>
        /// Number indicating the interval of sub-ranges for a numerical
        /// facet range.
        /// Can be overriden on a per field basis.
        /// </summary>
        public const string FACET_RANGE_GAP = FACET_RANGE + ".gap";

        /// <summary>
        /// Boolean indicating how counts should be computed if the range
        /// between 'start' and 'end' is not evenly divisible by 'gap'.  If
        /// this value is true, then all counts of ranges involving the 'end'
        /// point will use the exact endpoint specified -- this includes the
        /// 'between' and 'after' counts as well as the last range computed
        /// using the 'gap'.  If the value is false, then 'gap' is used to
        /// compute the effective endpoint closest to the 'end' param which
        /// results in the range between 'start' and 'end' being evenly
        /// divisible by 'gap'.
        /// The default is false.
        /// Can be overriden on a per field basis.
        /// </summary>
        public const string FACET_RANGE_HARD_END = FACET_RANGE + ".hardend";

        /// <summary>
        /// String indicating what "other" ranges should be computed for a
        /// numerical range facet (multi-value).
        /// Can be overriden on a per field basis.
        /// </summary>
        public const string FACET_RANGE_OTHER = FACET_RANGE + ".other";

        /// <summary>
        /// <p>
        /// Multivalued string indicating what rules should be applied to determine
        /// when the the ranges generated for numeric faceting should be inclusive or
        /// exclusive of their end points.
        /// </p>
        /// <p>
        /// The default value if none are specified is: lower
        /// </p>
        /// <p>
        /// Can be overriden on a per field basis.
        /// </p>
        /// @see FacetRangeInclude
        /// </summary>
        public const string FACET_RANGE_INCLUDE = FACET_RANGE + ".include";

        /// <summary>
        /// An enumeration of the legal values for {@link #FACET_RANGE_OTHER} and {@link #FACET_DATE_OTHER} ...
        /// <ul>
        /// <li>before = the count of matches before the start</li>
        /// <li>after = the count of matches after the end</li>
        /// <li>between = the count of all matches between start and end</li>
        /// <li>all = all of the above (default value)</li>
        /// <li>none = no additional info requested</li>
        /// </ul>
        /// @see #FACET_RANGE_OTHER
        /// @see #FACET_DATE_OTHER
        /// </summary>
        public enum FacetRangeOther
        {
            BEFORE,
            AFTER,
            BETWEEN,
            ALL,
            NONE
        }

        /// <summary>
        /// @deprecated Use {@link FacetRangeOther}
        /// </summary>
        public enum FacetDateOther
        {
            BEFORE, AFTER, BETWEEN, ALL, NONE
        }

        /// <summary>
        /// An enumeration of the legal values for {@link #FACET_DATE_INCLUDE} and {@link #FACET_RANGE_INCLUDE}
        /// <p>
        /// <ul>
        /// <li>lower = all gap based ranges include their lower bound</li>
        /// <li>upper = all gap based ranges include their upper bound</li>
        /// <li>edge = the first and last gap ranges include their edge bounds (ie: lower
        ///     for the first one, upper for the last one) even if the corresponding
        ///     upper/lower option is not specified
        /// </li>
        /// <li>outer = the BEFORE and AFTER ranges
        ///     should be inclusive of their bounds, even if the first or last ranges
        ///     already include those boundaries.
        /// </li>
        /// <li>all = shorthand for lower, upper, edge, and outer</li>
        /// </ul>
        /// @see #FACET_DATE_INCLUDE
        /// @see #FACET_RANGE_INCLUDE
        /// </summary>
        public enum FacetRangeInclude
        {
            ALL, LOWER, UPPER, EDGE, OUTER
        }
    }
}