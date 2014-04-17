using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Mizore.DataMappingHandler.Reflection
{
    public class ReflectionDataMapper<T> : IDataMappingHandler<T> where T : class, new()
    {
        protected static readonly Type SolrAttributeType = typeof(SolrFieldAttribute);
        protected readonly Type ObjectType;

        protected List<ReflectedMember<T>> mapping;
        protected ReflectedMember<T> uniqueKey;

        public ReflectionDataMapper()
        {
            ObjectType = typeof(T);
            mapping = new List<ReflectedMember<T>>();
            GetMembersWithAttribute(ObjectType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
            GetMembersWithAttribute(ObjectType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
        }

        protected void GetMembersWithAttribute(ICollection<MemberInfo> members)
        {
            if (members == null || members.Count == 0) return;
            foreach (var member in members)
            {
                var attrs = member.GetCustomAttributes(SolrAttributeType, false);
                if (attrs.IsNullOrEmpty()) continue;
                var solrAttr = attrs.FirstOrDefault() as SolrFieldAttribute;
                if (solrAttr == null) continue;
                var reflected = new ReflectedMember<T>(member, solrAttr);
                if (solrAttr is SolrIdFieldAttribute)
                    uniqueKey = reflected;
                mapping.Add(reflected);
            }
        }

        public IDataMappingHandler GetMappingHandler(Type type)
        {
            if (typeof(T) == type)
                return this;
            return null;
        }

        public IDataMappingHandler<T1> GetMappingHandler<T1>() where T1 : class, new()
        {
            if (typeof(T) == typeof(T1))
                return this as IDataMappingHandler<T1>;
            return null;
        }

        public bool CanHandle(Type type)
        {
            var fields = type.GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var attrs = field.GetCustomAttributes(SolrAttributeType, false);
                if (attrs.IsNullOrEmpty()) continue;
                return true;
            }
            return false;
        }

        public Type GetGenericType()
        {
            return typeof(ReflectionDataMapper<>);
        }

        object IDataMappingHandler.GetObject(SolrDocument doc)
        {
            return GetObject(doc);
        }

        public SolrInputDocument GetDocument(object obj)
        {
            return GetDocument(obj as T);
        }

        public T GetObject(SolrDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            var obj = new T();
            foreach (var member in mapping)
            {
                SolrInputField field;
                if (!document.Fields.TryGetValue(member.SolrField, out field)) continue;
                member.Set(obj, field.Value);
            }
            return obj;
        }

        public SolrInputDocument GetDocument(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var doc = new SolrInputDocument();
            foreach (var member in mapping)
            {
                var value = member.Get(obj);
                doc.Fields.Add(member.SolrField, new SolrInputField(member.SolrField, value, member.SolrFieldBoost.HasValue ? member.SolrFieldBoost.Value : 1f));
            }
            return doc;
        }
    }
}