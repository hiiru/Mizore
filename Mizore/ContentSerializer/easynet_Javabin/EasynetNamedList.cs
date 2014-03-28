﻿#region license

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
using System.Linq;
using Mizore.Data;

namespace Mizore.ContentSerializer.easynet_Javabin
{
    /// <summary>
    /// 简单排序集合
    /// </summary>
    [Serializable]
    public class SimpleOrderedMap : EasynetNamedList
    {
    }

    /// <summary>
    /// 简单排序泛型集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SimpleOrderedMap<T> : EasynetNamedList<T>
    {
    }

    /// <summary>
    /// 名称对象集合
    /// </summary>
    [Serializable]
    public class EasynetNamedList : EasynetNamedList<object>, INamedList
    {
    }

    /// <summary>
    /// 名称对象泛型集合
    /// </summary>
    /// <typeparam name="T">集合元素类型</typeparam>
    [Serializable]
    public class EasynetNamedList<T> : INamedList<T>
    {
        protected readonly IList nvPairs = new ArrayList();

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return nvPairs.Count >> 1;
            }
        }

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>名称</returns>
        public string GetName(int index)
        {
            return (string)nvPairs[index << 1];
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>值</returns>
        public T Get(int index)
        {
            return (T)nvPairs[(index << 1) + 1];
        }

        public string GetKey(int i)
        {
            return GetName(i);
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="value">值</param>
        public void Add(string name, T value)
        {
            nvPairs.Add(name);
            nvPairs.Add(value);
        }

        /// <summary>
        /// 设置名称
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="name">名称</param>
        public void SetName(int index, string name)
        {
            nvPairs[index << 1] = name;
        }

        /// <summary>
        /// 设置值
        /// </summary>
        /// <param name="index">位置</param>
        /// <param name="value">值</param>
        /// <returns>原有元素值</returns>
        public T SetValue(int index, T value)
        {
            int idx = (index << 1) + 1;

            T old = (T)nvPairs[idx];
            nvPairs[idx] = value;

            return old;
        }

        /// <summary>
        /// 移除元素
        /// </summary>
        /// <param name="index">位置</param>
        /// <returns>移除元素值</returns>
        public T Remove(int index)
        {
            int idx = (index << 1);

            nvPairs.Remove(idx);
            T val = (T)nvPairs[idx];
            nvPairs.Remove(idx);

            return val;
        }

        /// <summary>
        /// 获取指定名称元素位置位置
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="start">起始位置</param>
        /// <returns>位置</returns>
        public int IndexOf(string name, int start)
        {
            for (int i = start; i < Count; i++)
            {
                string n = GetName(i);

                if (name == null)
                {
                    if (n == null)
                    {
                        return i;
                    }
                }
                else if (name.Equals(n, StringComparison.CurrentCulture))
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// 获取指定名称元素值
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>元素值</returns>
        public T Get(string name)
        {
            return Get(name, 0);
        }

        /// <summary>
        /// 从指定位置起获取指定名称元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="start">其实位置</param>
        /// <returns>元素值</returns>
        public T Get(string name, int start)
        {
            for (int i = start; i < Count; i++)
            {
                string n = GetName(i);

                if (name == null)
                {
                    if (n == null)
                    {
                        return Get(i);
                    }
                }
                else if (name.Equals(n))
                {
                    return Get(i);
                }
            }

            return default(T);
        }

        /// <summary>
        /// 获取所有指定名称的元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>符合条件的元素集合</returns>
        public IList<T> GetAll(string name)
        {
            IList<T> result = new List<T>();

            for (int i = 0; i < Count; i++)
            {
                string n = GetName(i);

                if (name == n || (name != null && name.Equals(n)))
                {
                    result.Add(Get(i));
                }
            }

            return result;
        }

        /// <summary>
        /// 添加迭代元素
        /// </summary>
        /// <param name="args">迭代元素</param>
        /// <returns>添加元素数量是否大于0</returns>
        public bool AddAll(IEnumerable<KeyValuePair<string, T>> args)
        {
            foreach (KeyValuePair<string, T> pair in args)
            {
                Add(pair.Key, pair.Value);
            }

            return args.Count() > 0;
        }

        /// <summary>
        /// 添加名称集合
        /// </summary>
        /// <param name="namedList">名称集合</param>
        /// <returns>名称计划数量</returns>
        public bool AddAll(EasynetNamedList<T> namedList)
        {
            foreach (object obj in namedList.nvPairs)
            {
                nvPairs.Add(obj);
            }

            return namedList.Count > 0;
        }

        /// <summary>
        /// 移除指定名称元素
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>移除元素</returns>
        public T Remove(string name)
        {
            int idx = IndexOf(name, 0);

            if (idx != -1)
            {
                return Remove(idx);
            }

            return default(T);
        }

        public override int GetHashCode()
        {
            return nvPairs.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(EasynetNamedList<T>))
            {
                return false;
            }

            EasynetNamedList<T> nl = (EasynetNamedList<T>)obj;

            return nvPairs.Equals(nl.nvPairs);
        }
    }
}