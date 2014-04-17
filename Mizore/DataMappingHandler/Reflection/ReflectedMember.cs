using Mizore.DataMappingHandler.Attributes;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mizore.DataMappingHandler.Reflection
{
    public class ReflectedMember<T>
    {
        public ReflectedMember(MemberInfo member, SolrFieldAttribute solrAttr)
        {
            if (member == null)
                throw new ArgumentNullException("member");
            if (member is PropertyInfo || member is FieldInfo)
            {
                MemberName = member.Name;
                Type = GetMemberType(member);
                Get = GetGetter(member);
                Set = GetSetter(member);
            }
            else
                throw new ArgumentException("Invalid member type, only Properties and Fields are supported", "member");
            if (solrAttr == null)
                throw new ArgumentNullException("solrAttr");

            SolrField = solrAttr.Field ?? MemberName;
            SolrFieldBoost = solrAttr.Boost;
        }

        private Type GetMemberType(MemberInfo member)
        {
            if (member is PropertyInfo)
                return ((PropertyInfo)member).PropertyType;
            return ((FieldInfo)member).FieldType;
        }

        private static readonly MethodInfo CastAndListMethod = typeof(ReflectedMember<T>).GetMethod("CastAndList", BindingFlags.Static | BindingFlags.NonPublic);

        private static IList CastAndList(IList items, Type listType, Type itemType)
        {
            var list = Activator.CreateInstance(listType.GetGenericTypeDefinition().MakeGenericType(itemType)) as IList;
            foreach (var item in items)
            {
                list.Add(Convert.ChangeType(item, itemType));
            }
            return list;
        }

        private static readonly MethodInfo ChangeTypeMethod = typeof(Convert).GetMethod("ChangeType", new[] { typeof(object), typeof(Type) });

        protected Action<T, object> GetSetter(MemberInfo member)
        {
            ParameterExpression targetExp = Expression.Parameter(typeof(T), "targetObject");
            ParameterExpression valueExp = Expression.Parameter(typeof(object), "value");
            UnaryExpression convert;
            if (Type.IsClass)
            {
                if (Type.IsGenericType)
                {
                    if (typeof(IList).IsAssignableFrom(Type))
                    {
                        //TODO: test if this performs... it feels like a workaround...
                        var listitemType = Type.GetGenericArguments().FirstOrDefault();
                        Expression convertedObject = Expression.Call(CastAndListMethod, Expression.Convert(valueExp, typeof(IList)), Expression.Constant(Type), Expression.Constant(listitemType));
                        convert = Expression.Convert(convertedObject, Type);
                    }
                    else
                        throw new NotSupportedException("Generics aren't supported.");
                }
                else
                {
                    convert = Expression.Convert(valueExp, Type);
                }
            }
            else
            {
                Expression convertedObject = Expression.Call(ChangeTypeMethod, valueExp, Expression.Constant(Type));
                convert = Expression.Convert(convertedObject, Type);
            }
            MemberExpression memberExpression;
            if (member is PropertyInfo)
                memberExpression = Expression.Property(targetExp, (PropertyInfo)member);
            else
                memberExpression = Expression.Field(targetExp, (FieldInfo)member);
            return Expression.Lambda<Action<T, object>>(Expression.Assign(memberExpression, convert), targetExp, valueExp).Compile();
        }

        protected Func<T, object> GetGetter(MemberInfo member)
        {
            ParameterExpression targetExp = Expression.Parameter(typeof(T), "targetObject");
            MemberExpression memberExpression;
            if (member is PropertyInfo)
                memberExpression = Expression.Property(targetExp, (PropertyInfo)member);
            else
                memberExpression = Expression.Field(targetExp, (FieldInfo)member);
            var convert = Expression.TypeAs(memberExpression, typeof(object));
            return (Func<T, object>)Expression.Lambda(convert, targetExp).Compile();
        }

        public string MemberName { get; protected set; }

        public string SolrField { get; protected set; }

        public float? SolrFieldBoost { get; protected set; }

        public Type Type { get; protected set; }

        public Func<T, object> Get { get; protected set; }

        public Action<T, object> Set { get; protected set; }
    }
}