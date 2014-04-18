using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Mizore.ContentSerializer.JsonNet
{
    public class SolrJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            writer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            if (value is SolrUpdateList)
                WriteUpdateList(writer, value as SolrUpdateList);
            else
                WriteNamedList(writer, value as INamedList);
        }

        private void WriteUpdateList(JsonWriter writer, SolrUpdateList solrUpdateList)
        {
            writer.WriteStartObject();
            var add = solrUpdateList.Get("add") as INamedList;
            if (add != null)
            {
                var overwrite = add.Get("overwrite") as bool?;
                var commitWithin = add.Get("commitWithin") as int?;
                var docs = add.GetOrDefault<List<SolrInputDocument>>("doc");
                if (!docs.IsNullOrEmpty())
                    foreach (var doc in docs)
                    {
                        writer.WritePropertyName("add");
                        writer.WriteStartObject();
                        if (overwrite.HasValue)
                        {
                            writer.WritePropertyName("overwrite");
                            writer.WriteValue(overwrite.Value);
                        }
                        if (commitWithin.HasValue)
                        {
                            writer.WritePropertyName("commitWithin");
                            writer.WriteValue(commitWithin.Value);
                        }
                        if (doc.DocBoost.HasValue && doc.DocBoost.Value != 1f)
                        {
                            writer.WritePropertyName("boost");
                            writer.WriteValue(doc.DocBoost.Value);
                        }
                        writer.WritePropertyName("doc");
                        writer.WriteStartObject();
                        foreach (var solrInputField in doc.Fields)
                        {
                            writer.WritePropertyName(solrInputField.Key);
                            if (solrInputField.Value.Boost != 1f)
                            {
                                writer.WriteStartObject();
                                writer.WritePropertyName("boost");
                                writer.WriteValue(solrInputField.Value.Boost);
                                writer.WritePropertyName("value");
                                if (solrInputField.Value.Value is IList)
                                {
                                    WriteArray(writer, (IList)solrInputField.Value.Value);
                                    //writer.WriteStartArray();
                                    //foreach (var item in solrInputField.Value.Value as IList)
                                    //{
                                    //    writer.WriteValue(item);
                                    //}
                                    //writer.WriteEndArray();
                                }
                                else
                                    writer.WriteValue(solrInputField.Value.Value);
                                writer.WriteEndObject();
                            }
                            else
                            {
                                if (solrInputField.Value.Value is IList)
                                    WriteArray(writer, (IList)solrInputField.Value.Value);
                                else
                                    writer.WriteValue(solrInputField.Value.Value);
                            }
                        }
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
            }
            var delete = solrUpdateList.Get("delete") as INamedList;
            if (delete != null)
            {
                for (int i = 0; i < delete.Count; i++)
                {
                    writer.WritePropertyName("delete");
                    writer.WriteStartObject();
                    var key = delete.GetKey(i);
                    writer.WritePropertyName(key);
                    var value = delete.Get(i);
                    writer.WriteValue(value);
                    writer.WriteEndObject();
                }
            }
            var commit = solrUpdateList.Get("commit") as INamedList;
            if (commit != null)
            {
                var key = commit.GetKey(0);
                writer.WritePropertyName(key);
                WriteNamedList(writer, commit.Get(0) as INamedList);
            }

            writer.WriteEndObject();
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
            //reader.DateTimeZoneHandling = DateTimeZoneHandling.Local;
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
                        object value;
                        //Workaround because JSON doesn't know the exact type, so it would treat everything as NamedList, which differs from other formats
                        switch (propertyName)
                        {
                            //used for GET Requests
                            case "doc":
                                value = new SolrDocument(ReadNamedList(reader));
                                break;
                            //used for Logging Requests
                            case "history":
                            //used for Select Requests
                            case "response":
                                value = ReadSolrDocumentList(reader);
                                break;
                            default:
                                value = ReadNamedList(reader);
                                break;
                        }
                        list.Add(propertyName,value);
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

        protected object ReadSolrDocumentList(JsonReader reader)
        {
            if (reader.TokenType != JsonToken.StartObject)
                throw new ArgumentException("requires StartObject token", "reader");
            var nl = ReadNamedList(reader);
            // return the named list of the counts are wrong to be a document list
            if (nl.Count < 3 || nl.Count > 4)
                return nl;
            var sdl = new SolrDocumentList();
            for (int i = 0; i < nl.Count; i++)
            {
                switch (nl.GetKey(i))
                {
                    case "numFound":
                        sdl.NumFound = nl.GetOrDefaultStruct<long>(i);
                        break;

                    case "start":
                        sdl.Start = nl.GetOrDefaultStruct<long>(i);
                        break;

                    case "maxScore":
                        sdl.MaxScore = nl.GetOrDefault<object>(i) as float?;
                        break;

                    case "docs":
                        var docs = nl.GetOrDefault<IList>(i);
                        if (!docs.IsNullOrEmpty())
                        {
                            for (int j = 0; j < docs.Count; j++)
                                sdl.Add(new SolrDocument(docs[j] as INamedList));
                        }
                        break;

                    default:
                        //Unexpected name, returning namedlist as fallback
                        return nl;
                }
            }
            return sdl;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(INamedList).IsAssignableFrom(objectType);
        }
    }
}