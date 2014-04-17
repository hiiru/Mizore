using Mizore.ContentSerializer.Data.Solr;
using Mizore.DataMappingHandler.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Mizore.DataMappingHandler
{
    public class DataMappingCollection : IDataMappingHandler
    {
        protected Dictionary<Type, IDataMappingHandler> Mappers;
        protected List<IDataMappingHandler> Handlers;

        public DataMappingCollection()
        {
            Mappers = new Dictionary<Type, IDataMappingHandler>();
            Handlers = new List<IDataMappingHandler> { new ReflectionDataMapper() };
        }

        public IDataMappingHandler GetMappingHandler(Type type)
        {
            if (Mappers.ContainsKey(type))
                return Mappers[type];
            foreach (var handler in Handlers)
            {
                if (handler.CanHandle(type))
                {
                    var construct = Expression.Lambda(Expression.New(handler.GetGenericType().MakeGenericType(type))).Compile();
                    var genericHandler = construct.DynamicInvoke() as IDataMappingHandler;
                    Mappers.Add(type, genericHandler);
                    return genericHandler;
                }
            }
            return null;
        }

        public IDataMappingHandler<T> GetMappingHandler<T>() where T : class, new()
        {
            return GetMappingHandler(typeof(T)) as IDataMappingHandler<T>;
        }

        public bool CanHandle(Type type)
        {
            return Mappers.ContainsKey(type) || Handlers.Any(handler => handler.CanHandle(type));
        }

        public Type GetGenericType()
        {
            return null;
        }

        public object GetObject(SolrDocument doc)
        {
            throw new NotSupportedException("Can't create object in base class");
        }

        public SolrInputDocument GetDocument(object obj)
        {
            throw new NotSupportedException("Can't create document in base class");
        }
    }
}