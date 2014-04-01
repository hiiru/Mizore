using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler.Attributes;

namespace Mizore.DataMappingHandler.Reflection
{
    public class ReflectionDataMapper<T> : IDataMappingHandlery<T> where T : class, new()
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