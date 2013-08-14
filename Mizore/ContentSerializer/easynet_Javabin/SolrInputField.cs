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
#endregion
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mizore.ContentSerializer.easynet_Javabin
{
	/// <summary>
	/// Solr输入文档字段
	/// </summary>
	[Serializable]
	public class SolrInputField : IEnumerable
	{
		/// <summary>
		/// 字段名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 字段值
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		/// 字段评分
		/// </summary>
		public float? Boost { get; set; }

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">字段名称</param>
		public SolrInputField(string name)
		{
			Name = name;
			Boost = 1.0f;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">字段名称，字段值</param>
		/// <param name="value"></param>
		public SolrInputField(string name, object value)
			: this(name, value, 1.0f)
		{

		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="name">字段名称</param>
		/// <param name="value">字段值</param>
		/// <param name="boost">字段评分</param>
		public SolrInputField(string name, object value, float? boost)
		{
			Name = name;
			SetValue(value, boost);
		}

		/// <summary>
		/// 设置字段值
		/// </summary>
		/// <param name="value">字段值</param>
		/// <param name="boost">评分</param>
		public void SetValue(object value, float? boost)
		{
			Boost = boost;

			if (value is object[])
			{
				object[] array = (object[])value;

				ICollection<object> c = new List<object>(array.Length);

				foreach (object o in array)
				{
					c.Add(o);
				}

				Value = c;
			}
			else
			{
				Value = value;
			}
		}

		/// <summary>
		/// 添加字段值
		/// </summary>
		/// <param name="value">字段值</param>
		/// <param name="boost">评分</param>
		public void AddValue(object value, float? boost)
		{
			if (Value == null)
			{
				SetValue(value, boost);

				return;
			}

			Boost *= boost;

			ICollection<object> values = null;

			if (Value is ICollection)
			{
				values = (ICollection<object>)Value;
			}
			else
			{
				values = new List<object>(3);
				values.Add(Value);
				Value = values;
			}

			if (value is IEnumerable)
			{
				foreach (object o in (IEnumerable)value)
				{
					values.Add(o);
				}
			}
			else if (value is object[])
			{
				foreach (object o in (object[])value)
				{
					values.Add(o);
				}
			}
			else
			{
				values.Add(value);
			}
		}

		/// <summary>
		/// 获取字段值
		/// </summary>
		/// <returns>字段值集合</returns>
		public ICollection<object> GetValues()
		{
			if (Value is ICollection<object>)
			{
				return (ICollection<object>)Value;
			}

			if (Value != null)
			{
				ICollection<object> vals = new List<object>(1);

				vals.Add(Value);

				return vals;
			}

			return null;
		}

		/// <summary>
		/// 获取字段值数量
		/// </summary>
		/// <returns></returns>
		public int GetValueCount()
		{
			if (Value is ICollection)
			{
				return ((ICollection)Value).Count;
			}

			return Value == null ? 0 : 1;
		}

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			if (Value is ICollection)
			{
				return ((ICollection)Value).GetEnumerator();
			}
			else
			{
				ICollection<object> vals = new List<object>(1);

				vals.Add(Value);

				return vals.GetEnumerator();
			}
		}

		#endregion
	}
}