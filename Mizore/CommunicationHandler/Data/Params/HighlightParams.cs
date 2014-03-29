/** License / Porting note
 * This File is ported from the apache solr sourcecode
 * org.apache.solr.common.params.HighlightParams
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
    public static class HighlightParams
    {
        public const string HIGHLIGHT = "hl";
        public const string Q = HIGHLIGHT + ".q";
        public const string QPARSER = HIGHLIGHT + ".qparser";
        public const string FIELDS = HIGHLIGHT + ".fl";
        public const string SNIPPETS = HIGHLIGHT + ".snippets";
        public const string FRAGSIZE = HIGHLIGHT + ".fragsize";
        public const string INCREMENT = HIGHLIGHT + ".increment";
        public const string MAX_CHARS = HIGHLIGHT + ".maxAnalyzedChars";
        public const string FORMATTER = HIGHLIGHT + ".formatter";
        public const string ENCODER = HIGHLIGHT + ".encoder";
        public const string FRAGMENTER = HIGHLIGHT + ".fragmenter";
        public const string PRESERVE_MULTI = HIGHLIGHT + ".preserveMulti";
        public const string FRAG_LIST_BUILDER = HIGHLIGHT + ".fragListBuilder";
        public const string FRAGMENTS_BUILDER = HIGHLIGHT + ".fragmentsBuilder";
        public const string BOUNDARY_SCANNER = HIGHLIGHT + ".boundaryScanner";
        public const string BS_MAX_SCAN = HIGHLIGHT + ".bs.maxScan";
        public const string BS_CHARS = HIGHLIGHT + ".bs.chars";
        public const string BS_TYPE = HIGHLIGHT + ".bs.type";
        public const string BS_LANGUAGE = HIGHLIGHT + ".bs.language";
        public const string BS_COUNTRY = HIGHLIGHT + ".bs.country";
        public const string BS_VARIANT = HIGHLIGHT + ".bs.variant";
        public const string FIELD_MATCH = HIGHLIGHT + ".requireFieldMatch";
        public const string DEFAULT_SUMMARY = HIGHLIGHT + ".defaultSummary";
        public const string ALTERNATE_FIELD = HIGHLIGHT + ".alternateField";
        public const string ALTERNATE_FIELD_LENGTH = HIGHLIGHT + ".maxAlternateFieldLength";
        public const string MAX_MULTIVALUED_TO_EXAMINE = HIGHLIGHT + ".maxMultiValuedToExamine";
        public const string MAX_MULTIVALUED_TO_MATCH = HIGHLIGHT + ".maxMultiValuedToMatch";

        public const string USE_PHRASE_HIGHLIGHTER = HIGHLIGHT + ".usePhraseHighlighter";
        public const string HIGHLIGHT_MULTI_TERM = HIGHLIGHT + ".highlightMultiTerm";

        public const string MERGE_CONTIGUOUS_FRAGMENTS = HIGHLIGHT + ".mergeContiguous";

        public const string USE_FVH = HIGHLIGHT + ".useFastVectorHighlighter";
        public const string TAG_PRE = HIGHLIGHT + ".tag.pre";
        public const string TAG_POST = HIGHLIGHT + ".tag.post";
        public const string TAG_ELLIPSIS = HIGHLIGHT + ".tag.ellipsis";
        public const string PHRASE_LIMIT = HIGHLIGHT + ".phraseLimit";
        public const string MULTI_VALUED_SEPARATOR = HIGHLIGHT + ".multiValuedSeparatorChar";

        // Formatter
        public const string SIMPLE = "simple";

        public const string SIMPLE_PRE = HIGHLIGHT + "." + SIMPLE + ".pre";
        public const string SIMPLE_POST = HIGHLIGHT + "." + SIMPLE + ".post";

        // Regex fragmenter
        public const string REGEX = "regex";

        public const string SLOP = HIGHLIGHT + "." + REGEX + ".slop";
        public const string PATTERN = HIGHLIGHT + "." + REGEX + ".pattern";
        public const string MAX_RE_CHARS = HIGHLIGHT + "." + REGEX + ".maxAnalyzedChars";

        // Scoring parameters
        public const string SCORE = "score";

        public const string SCORE_K1 = HIGHLIGHT + "." + SCORE + ".k1";
        public const string SCORE_B = HIGHLIGHT + "." + SCORE + ".b";
        public const string SCORE_PIVOT = HIGHLIGHT + "." + SCORE + ".pivot";
    }
}