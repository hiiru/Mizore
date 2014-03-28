using System;
using System.Collections;
using System.Collections.Generic;
using Mizore.Data;
using Newtonsoft.Json;

namespace Mizore.ContentSerializer.JsonNet
{
    public class SolrJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            WriteNamedList(writer, value as INamedList);
        }

        protected void WriteNamedList(JsonWriter writer, INamedList list)
        {
            writer.WriteStartObject();
            if (list.IsNullOrEmpty())
            {
                writer.WriteEndObject();
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                var key = list.GetKey(i);
                writer.WritePropertyName(key);
                var value = list.Get(i);
                if (value is INamedList)
                    WriteNamedList(writer, value as INamedList);
                else if (value is IList)
                    WriteArray(writer, value as IList);
                else
                    writer.WriteValue(value);
            }
            writer.WriteEndObject();
        }

        protected void WriteArray(JsonWriter writer, IList list)
        {
            writer.WriteStartArray();
            if (list.IsNullOrEmpty())
            {
                writer.WriteEndArray();
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                var value = list[i];
                if (value is INamedList)
                    WriteNamedList(writer, value as INamedList);
                else if (value is IList)
                    WriteArray(writer, value as IList);
                else
                    writer.WriteValue(value);
            }
            writer.WriteEndArray();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartArray:
                    return ReadArray(reader);

                case JsonToken.StartObject:
                    return ReadNamedList(reader);

                case JsonToken.StartConstructor:
                    throw new NotSupportedException("Json Constructor not supported.");
                default:
                    throw new ArgumentException("Expected start token");
            }
        }

        protected INamedList ReadNamedList(JsonReader reader)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new ArgumentException("requires StartObject token", "reader");
            var list = new NamedList();
            string propertyName = null;
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Date:
                        if (propertyName == null)
                            throw new InvalidOperationException("propertyName is null...");
                        list.Add(propertyName, ((DateTime)reader.Value).ToLocalTime());
                        propertyName = null;
                        break;
                    case JsonToken.Null:
                    case JsonToken.Boolean:
                    case JsonToken.Bytes:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                    case JsonToken.String:
                        if (propertyName == null)
                            throw new InvalidOperationException("propertyName is null...");

                        list.Add(propertyName, reader.Value);
                        propertyName = null;
                        break;

                    case JsonToken.PropertyName:
                        if (propertyName != null)
                            throw new InvalidOperationException("this shouldn't happen ^^");
                        propertyName = reader.Value as string;
                        break;

                    case JsonToken.StartArray:
                        if (propertyName == null)
                            throw new InvalidOperationException("propertyName is null...");
                        list.Add(propertyName, ReadArray(reader));
                        propertyName = null;
                        break;

                    case JsonToken.StartObject:
                        if (propertyName == null)
                            throw new InvalidOperationException("propertyName is null...");
                        list.Add(propertyName, ReadNamedList(reader));
                        propertyName = null;
                        break;

                    case JsonToken.EndArray:
                    case JsonToken.EndObject:
                        if (propertyName != null)
                            throw new InvalidOperationException("this shouldn't happen ^^, again...");
                        return list;

                    case JsonToken.None:
                    case JsonToken.Comment:
                    case JsonToken.Raw:
                    case JsonToken.Undefined:
                        continue;
                    case JsonToken.StartConstructor:
                    case JsonToken.EndConstructor:
                    default:
                        throw new NotSupportedException("Json Constructor not supported!");
                }
            }
            throw new InvalidOperationException("Some shit hit the fan! - eh i mean, this won't happen, i hope :P");
        }

        protected IList<object> ReadArray(JsonReader reader)
        {
            if (reader.TokenType != JsonToken.StartArray)
                throw new ArgumentException("requires StartArray token", "reader");

            var array = new List<object>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Date:
                        array.Add(((DateTime)reader.Value).ToUniversalTime());
                        break;
                    case JsonToken.Null:
                    case JsonToken.Boolean:
                    case JsonToken.Bytes:
                    case JsonToken.Float:
                    case JsonToken.Integer:
                    case JsonToken.String:
                        array.Add(reader.Value);
                        break;

                    case JsonToken.PropertyName:
                        throw new InvalidOperationException("Json Array can't have a property name!");
                        break;

                    case JsonToken.StartArray:
                        array.Add(ReadArray(reader));
                        break;

                    case JsonToken.StartObject:
                        array.Add(ReadNamedList(reader));
                        break;

                    case JsonToken.EndArray:
                    case JsonToken.EndObject:
                        return array;

                    case JsonToken.None:
                    case JsonToken.Comment:
                    case JsonToken.Raw:
                    case JsonToken.Undefined:
                        continue;

                    case JsonToken.StartConstructor:
                    case JsonToken.EndConstructor:
                    default:
                        throw new NotSupportedException("Json Constructor not supported!");
                }
            }
            throw new InvalidOperationException("Some shit hit the fan! - eh i mean, this won't happen, i hope :P");
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(INamedList).IsAssignableFrom(objectType);
        }
    }
}