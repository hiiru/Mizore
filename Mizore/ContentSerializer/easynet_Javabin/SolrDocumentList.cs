#region license

// This File is based on the easynet Project (http://easynet.codeplex.com) created by the Terry Liang.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion license

using System.Collections.Generic;

namespace Mizore.ContentSerializer.easynet_Javabin
{
    /// <summary>
    /// Solr返回文档集合
    /// </summary>
    public class SolrDocumentList : List<SolrDocument>
    {
        /// <summary>
        /// 符合条件的记录数
        /// </summary>
        public long NumFound { get; set; }

        /// <summary>
        /// 起始记录
        /// </summary>
        public long Start { get; set; }

        /// <summary>
        /// 最大评分
        /// </summary>
        public float? MaxScore { get; set; }
    }
}