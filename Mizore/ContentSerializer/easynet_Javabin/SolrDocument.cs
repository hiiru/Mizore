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

using System;
using System.Collections;
using System.Collections.Generic;

namespace Mizore.ContentSerializer.easynet_Javabin
{
    /// <summary>
    /// Solr返回文档
    /// </summary>
    public class SolrDocument : IDictionary<string, object>
    {
        private readonly IDictionary<string, object> fields = new LinkedHashMap<string, object>();

        /// <summary>
        /// 所有字段名称集合
        /// </summary>
        /// <returns></returns>
        public ICollection<String> GetFieldNames()
        {
            return fields.Keys;
        }

        /// <summary>
        /// 根据字段名称移除字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveFields(string name)
        {
            return fields.Remove(name);
        }

        /// <summary>
        /// 设置字段值
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <param name="value">字段值</param>
        public void SetField(string name, object value)
        {
            if (value is object[])
            {
                value = new ArrayList((object[])value);
            }
            else if (value is string || value is ICollection || value is EasynetNamedList)
            {
            }
            else if (value is IEnumerable)
            {
                IList<object> lst = new List<object>();

                foreach (object o in (IEnumerable)value)
                {
                    lst.Add(o);
                }

                value = lst;
            }

            fields[name] = value;
        }

        #region IDictionary<string,object> Members

        public void Add(string key, object value)
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

        public bool TryGetValue(string key, out object value)
        {
            return fields.TryGetValue(key, out value);
        }

        public ICollection<object> Values
        {
            get { return fields.Values; }
        }

        public object this[string key]
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

        #endregion IDictionary<string,object> Members

        #region ICollection<KeyValuePair<string,object>> Members

        public void Add(KeyValuePair<string, object> item)
        {
            fields.Add(item);
        }

        public void Clear()
        {
            fields.Clear();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return fields.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<string, object> item)
        {
            return fields.Remove(item);
        }

        #endregion ICollection<KeyValuePair<string,object>> Members

        #region IEnumerable<KeyValuePair<string,object>> Members

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return fields.GetEnumerator();
        }

        #endregion IEnumerable<KeyValuePair<string,object>> Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Members
    }
}