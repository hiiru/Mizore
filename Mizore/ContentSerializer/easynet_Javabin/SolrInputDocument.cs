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

using System.Collections;
using System.Collections.Generic;

namespace Mizore.ContentSerializer.easynet_Javabin
{
    /// <summary>
    /// Solr输入文档
    /// </summary>
    public class SolrInputDocument : IDictionary<string, SolrInputField>, IEnumerable<SolrInputField>
    {
        private readonly IDictionary<string, SolrInputField> fields = new LinkedHashMap<string, SolrInputField>();

        /// <summary>
        /// 文档评分
        /// </summary>
        public float? Boost { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SolrInputDocument()
        {
            Boost = 1.0f;
        }

        #region IDictionary<string,SolrInputField> Members

        public void Add(string key, SolrInputField value)
        {
            fields.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return fields.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return fields.Keys; }
        }

        public bool Remove(string key)
        {
            return fields.Remove(key);
        }

        public bool TryGetValue(string key, out SolrInputField value)
        {
            return fields.TryGetValue(key, out value);
        }

        public ICollection<SolrInputField> Values
        {
            get { return fields.Values; }
        }

        public SolrInputField this[string key]
        {
            get
            {
                return fields[key];
            }
            set
            {
                fields[key] = value;
            }
        }

        #endregion IDictionary<string,SolrInputField> Members

        #region ICollection<KeyValuePair<string,SolrInputField>> Members

        public void Add(KeyValuePair<string, SolrInputField> item)
        {
            fields.Add(item);
        }

        public void Clear()
        {
            fields.Clear();
        }

        public bool Contains(KeyValuePair<string, SolrInputField> item)
        {
            return fields.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, SolrInputField>[] array, int arrayIndex)
        {
            fields.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return fields.Count; }
        }

        public bool IsReadOnly
        {
            get { return fields.IsReadOnly; }
        }

        public bool Remove(KeyValuePair<string, SolrInputField> item)
        {
            return fields.Remove(item);
        }

        #endregion ICollection<KeyValuePair<string,SolrInputField>> Members

        #region IEnumerable<KeyValuePair<string,SolrInputField>> Members

        public IEnumerator<KeyValuePair<string, SolrInputField>> GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        #endregion IEnumerable<KeyValuePair<string,SolrInputField>> Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Members

        #region IEnumerable<SolrInputField> Members

        IEnumerator<SolrInputField> IEnumerable<SolrInputField>.GetEnumerator()
        {
            return fields.Values.GetEnumerator();
        }

        #endregion IEnumerable<SolrInputField> Members
    }
}