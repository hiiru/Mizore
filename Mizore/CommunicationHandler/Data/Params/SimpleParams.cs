/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.SimpleParams
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
    /// Parameters used by the SimpleQParser.
    /// </summary>
    public static class SimpleParams
    {
        /// <summary>
        /// Query fields and boosts.
        /// </summary>
        public const string QF = "qf";

        /// <summary>
        /// Override the currently enabled/disabled query operators.
        /// </summary>
        public const string QO = "q.operators";

        /// <summary>
        /// Enables {@code AND} operator (+)
        /// </summary>
        public const string AND_OPERATOR = "AND";

        /// <summary>
        /// Enables {@code NOT} operator (-)
        /// </summary>
        public const string NOT_OPERATOR = "NOT";

        /// <summary>
        /// Enables {@code OR} operator (|)
        /// </summary>
        public const string OR_OPERATOR = "OR";

        /// <summary>
        /// Enables {@code PREFIX} operator (*)
        /// </summary>
        public const string PREFIX_OPERATOR = "PREFIX";

        /// <summary>
        /// Enables {@code PHRASE} operator (")
        /// </summary>
        public const string PHRASE_OPERATOR = "PHRASE";

        /// <summary>
        /// Enables {@code PRECEDENCE} operators: {@code (} and {@code )}
        /// </summary>
        public const string PRECEDENCE_OPERATORS = "PRECEDENCE";

        /// <summary>
        /// Enables {@code ESCAPE} operator (\)
        /// </summary>
        public const string ESCAPE_OPERATOR = "ESCAPE";

        /// <summary>
        /// Enables {@code WHITESPACE} operators: ' ' '\n' '\r' '\t'
        /// </summary>
        public const string WHITESPACE_OPERATOR = "WHITESPACE";

        /// <summary>
        /// Enables {@code FUZZY} operator (~)
        /// </summary>
        public const string FUZZY_OPERATOR = "FUZZY";

        /// <summary>
        /// Enables {@code NEAR} operator (~)
        /// </summary>
        public const string NEAR_OPERATOR = "NEAR";
    }
}