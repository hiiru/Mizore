using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mizore.ContentSerializer.Data;
using Mizore.ContentSerializer.Data.Solr;

namespace Mizore.ContentSerializer.JavaBin
{
    public class SolrJavaBinConverter
    {
        #region Solr JavaBin Definition (based on solr's JavaBinCodec from 12.03.2014)

        protected const byte
            NULL = 0,
            BOOL_TRUE = 1,
            BOOL_FALSE = 2,
            BYTE = 3,
            SHORT = 4,
            DOUBLE = 5,
            INT = 6,
            LONG = 7,
            FLOAT = 8,
            DATE = 9,
            MAP = 10,
            SOLRDOC = 11,
            SOLRDOCLST = 12,
            BYTEARR = 13,
            ITERATOR = 14,
            END = 15,
            SOLRINPUTDOC = 16,
            SOLRINPUTDOC_CHILDS = 17,
            ENUM_FIELD_VALUE = 18,
            MAP_ENTRY = 19,
            STR = 32,
            SINT = 64,
            SLONG = 96,
            ARR = 128,
            ORDERED_MAP = 160,
            NAMED_LST = 192,
            EXTERN_STRING = 224;

        protected const byte VERSION = 2;

        protected static readonly object END_OBJ = new object();

        protected byte tagByte;
        private byte[] bytes;

        #endregion Solr JavaBin Definition (based on solr's JavaBinCodec from 12.03.2014)

        private static readonly DateTime utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        //holds all the extern strings
        private readonly List<string> ExternStrings = new List<string>();

        private readonly Dictionary<string, int> ExtenStringsMap = new Dictionary<string, int>();

        public void WriteJavaBin(object list, Stream stream)
        {
            var writeStream = new SolrBinaryStream(stream);
            try
            {
                writeStream.Write(VERSION);
                WriteVal(list, writeStream);
            }
            finally
            {
                writeStream.Flush();
                writeStream.Close();
            }
        }

        public object ReadJavaBin(Stream stream)
        {
            var solrStream = new SolrBinaryStream(stream);
            var version = solrStream.ReadByte();
            if (version != VERSION)
            {
                throw new Exception("Invalid version (expected " + VERSION + ", but " + version + ") or the data in not in 'javabin' format");
            }
            return ReadVal(solrStream);
        }

        #region Read

        public object ReadVal(SolrBinaryStream stream)
        {
            tagByte = Convert.ToByte(stream.ReadByte());

            //Reduced bitshifting to required minimum (each case is static, instead of bitshifted
            switch (tagByte >> 5)
            {
                case 1: //STR >> 5:
                    return ReadString(stream);

                case 2: //SINT >> 5:
                    return ReadSmallInt(stream);

                case 3: //SLONG >> 5:
                    return ReadSmallLong(stream);

                case 4: //ARR >> 5:
                    return ReadArray(stream);

                case 5: //ORDERED_MAP >> 5:
                    return ReadOrderedMap(stream);

                case 6: //NAMED_LST >> 5:
                    return ReadNamedList(stream);

                case 7: //EXTERN_STRING >> 5:
                    return ReadExternString(stream);
            }

            switch (tagByte)
            {
                case NULL:
                    return null;

                case DATE:
                    var timestamp = stream.ReadLong();
                    if (timestamp < -62135596800000)
                        return DateTime.MinValue;
                    DateTime date;
                    try
                    {
                        date = utcDateTime.AddMilliseconds(timestamp).ToLocalTime();
                    }
                    catch
                    {
                        date = DateTime.MinValue;
                    }
                    return date;

                case INT:
                    return stream.ReadInt();

                case BOOL_TRUE:
                    return true;

                case BOOL_FALSE:
                    return false;

                case FLOAT:
                    return stream.ReadFloat();

                case DOUBLE:
                    return stream.ReadDouble();

                case LONG:
                    return stream.ReadLong();

                case BYTE:
                    return stream.ReadByte();

                case SHORT:
                    return stream.ReadShort();

                case MAP:
                    return ReadMap(stream);

                case SOLRDOC:
                    return ReadSolrDocument(stream);

                case SOLRDOCLST:
                    return ReadSolrDocumentList(stream);

                case BYTEARR:
                    return ReadByteArray(stream);

                case ITERATOR:
                    return ReadIterator(stream);

                case END:
                    return END_OBJ;

                case SOLRINPUTDOC:
                    return ReadSolrInputDocument(stream);

                case ENUM_FIELD_VALUE:
                    return ReadEnumFieldValue(stream);

                case MAP_ENTRY:
                    return ReadMapEntry(stream);
            }

            throw new Exception("Unknown type " + tagByte);
        }

        private object ReadMapEntry(SolrBinaryStream stream)
        {
            var key = ReadVal(stream);
            var value = ReadVal(stream);
            return new KeyValuePair<object, object>(key, value);
        }

        private object ReadEnumFieldValue(SolrBinaryStream stream)
        {
            var intValue = (int)ReadVal(stream);
            var stringValue = (string)ReadVal(stream);
            return new Tuple<int, string>(intValue, stringValue);
        }

        private object ReadSolrInputDocument(SolrBinaryStream stream)
        {
            int sz = ReadVInt(stream);
            var sid = new SolrInputDocument();
            sid.DocBoost = (float?)ReadVal(stream);
            for (int i = 0; i < sz; i++)
            {
                var boost = 1.0f;
                string name;
                var obj = ReadVal(stream); // could be a boost, a field name, or a child document
                if (obj is float)
                {
                    boost = (float)obj;
                    name = (string)ReadVal(stream);
                }
                else if (obj is SolrInputDocument)
                {
                    if (sid.ChildDocuments == null)
                        sid.ChildDocuments = new List<SolrInputDocument>();
                    sid.ChildDocuments.Add(obj as SolrInputDocument);
                    continue;
                }
                else
                {
                    name = (string)obj;
                }
                var value = ReadVal(stream);
                sid.Fields.Add(name, new SolrInputField(name) { Boost = boost, Value = value });
            }
            return sid;
        }

        private IList ReadIterator(SolrBinaryStream stream)
        {
            var l = new ArrayList();
            while (true)
            {
                var o = ReadVal(stream);
                if (o == END_OBJ) break;
                l.Add(o);
            }
            return l;
        }

        private byte[] ReadByteArray(SolrBinaryStream stream)
        {
            var sz = ReadVInt(stream);
            var arr = new byte[sz];
            stream.Read(arr, 0, sz);
            return arr;
        }

        private SolrDocumentList ReadSolrDocumentList(SolrBinaryStream stream)
        {
            var sdl = new SolrDocumentList();
            var metadata = (IList)ReadVal(stream);
            sdl.NumFound = (long)metadata[0];
            sdl.Start = (long)metadata[1];
            sdl.MaxScore = (float?)metadata[2];
            var arr = ReadVal(stream) as ArrayList;
            if (arr != null)
                sdl.AddRange(arr.Cast<SolrDocument>());
            return sdl;
        }

        private SolrDocument ReadSolrDocument(SolrBinaryStream stream)
        {
            return new SolrDocument(ReadVal(stream) as INamedList);
        }

        private IDictionary<object, object> ReadMap(SolrBinaryStream stream)
        {
            int sz = ReadVInt(stream);
            var m = new Dictionary<object, object>();
            for (int i = 0; i < sz; i++)
            {
                var key = ReadVal(stream);
                var val = ReadVal(stream);
                m[key] = val;
            }
            return m;
        }

        private string ReadExternString(SolrBinaryStream stream)
        {
            int idx = ReadSize(stream);
            // idx != 0 is the index of the extern string
            if (idx != 0)
                return ExternStrings[idx - 1];

            // idx == 0 means it has a string value
            var s = ReadVal(stream) as string;
            ExternStrings.Add(s);
            return s;
        }

        private INamedList ReadNamedList(SolrBinaryStream stream)
        {
            int sz = ReadSize(stream);
            var nl = new SerializationNamedList();
            for (int i = 0; i < sz; i++)
            {
                var key = ReadVal(stream) as string;
                object value = ReadVal(stream);
                nl.Add(key, value);
            }
            return nl;
        }

        private INamedList ReadOrderedMap(SolrBinaryStream stream)
        {
            return ReadNamedList(stream);
            //throw new NotImplementedException();
        }

        private int ReadSize(SolrBinaryStream stream)
        {
            int sz = tagByte & 0x1f;
            if (sz == 0x1f) sz += ReadVInt(stream);
            return sz;
        }

        private int ReadVInt(SolrBinaryStream stream)
        {
            var b = Convert.ToByte(stream.ReadByte());
            int i = b & 0x7F;
            for (int shift = 7; (b & 0x80) != 0; shift += 7)
            {
                b = Convert.ToByte(stream.ReadByte());
                i |= (b & 0x7F) << shift;
            }
            return i;
        }

        private long ReadVLong(SolrBinaryStream stream)
        {
            var b = Convert.ToByte(stream.ReadByte());
            long i = b & 0x7F;
            for (int shift = 7; (b & 0x80) != 0; shift += 7)
            {
                b = Convert.ToByte(stream.ReadByte());
                i |= (long)(b & 0x7F) << shift;
            }
            return i;
        }

        private string ReadString(SolrBinaryStream stream)
        {
            int sz = ReadSize(stream);
            if (bytes == null || bytes.Length < sz) bytes = new byte[sz];
            stream.Read(bytes, 0, sz);
            return Encoding.UTF8.GetString(bytes, 0, sz);
        }

        private int ReadSmallInt(SolrBinaryStream stream)
        {
            int v = tagByte & 0x0F;
            if ((tagByte & 0x10) != 0)
                v = (ReadVInt(stream) << 4) | v;
            return v;
        }

        private long ReadSmallLong(SolrBinaryStream stream)
        {
            long v = tagByte & 0x0F;
            if ((tagByte & 0x10) != 0)
                v = (ReadVLong(stream) << 4) | v;
            return v;
        }

        private IList ReadArray(SolrBinaryStream stream)
        {
            var sz = ReadSize(stream);
            var l = new ArrayList(sz);
            for (int i = 0; i < sz; i++)
                l.Add(ReadVal(stream));
            return l;
        }

        #endregion Read

        #region Write

        public void WriteVal(object value, SolrBinaryStream stream)
        {
            if (WritePrimitive(value, stream))
                return;
            if (value is INamedList)
                WriteNamedList(value as INamedList, stream);
            if (value is SolrDocumentList)
                WriteSolrDocumentList(value as SolrDocumentList, stream);
            if (value is SolrDocument)
                WriteSolrDocument(value as SolrDocument, stream);
            if (value is SolrInputDocument)
                WriteSolrInputDocument(value as SolrInputDocument, stream);
            if (value is IList)
                WriteArray(value as IList, stream);
            if (value is object[])
                WriteArray((object[])value, stream);
            if (value is IDictionary)
                WriteMap((IDictionary)value, stream);
            if (value is IEnumerator)
                WriteIterator((IEnumerator)value, stream);
            if (value is Tuple<int, string>)
                WriteEnumFieldValue(value as Tuple<int, string>, stream);
            if (value is KeyValuePair<object, object>)
                WriteMapEntry((KeyValuePair<object, object>)value, stream);
        }

        private void WriteMapEntry(KeyValuePair<object, object> value, SolrBinaryStream stream)
        {
            WriteTag(MAP_ENTRY, stream);
            WriteVal(value.Key, stream);
            WriteVal(value.Value, stream);
        }

        private void WriteEnumFieldValue(Tuple<int, string> value, SolrBinaryStream stream)
        {
            WriteTag(ENUM_FIELD_VALUE, stream);
            WriteInt(value.Item1, stream);
            WriteString(value.Item2, stream);
        }

        private void WriteIterator(IEnumerator value, SolrBinaryStream stream)
        {
            WriteTag(ITERATOR, stream);
            while (value.MoveNext())
                WriteVal(value.Current, stream);
            WriteVal(END_OBJ, stream);
        }

        private void WriteMap(IDictionary value, SolrBinaryStream stream)
        {
            WriteTag(MAP, stream, value.Count);
            foreach (DictionaryEntry entry in value)
            {
                if (entry.Key is string)
                    WriteExternString(entry.Key as string, stream);
                else
                    WriteVal(entry.Key, stream);
                WriteVal(entry.Value, stream);
            }
        }

        private void WriteSolrInputDocument(SolrInputDocument sid, SolrBinaryStream stream)
        {
            var length = sid.Fields.Count + (sid.ChildDocuments != null ? sid.ChildDocuments.Count : 0);
            WriteTag(SOLRINPUTDOC, stream, length);
            WriteFloat(sid.DocBoost.HasValue ? sid.DocBoost.Value : 1f, stream);
            foreach (SolrInputField field in sid.Fields.Values)
            {
                if (field.Boost != 1.0f)
                    WriteFloat(field.Boost, stream);
                WriteExternString(field.Name, stream);
                WriteVal(field.Value, stream);
            }
            if (sid.ChildDocuments != null)
            {
                foreach (var doc in sid.ChildDocuments)
                    WriteVal(doc, stream);
            }
        }

        private void WriteSolrDocument(SolrDocument solrDocument, SolrBinaryStream stream)
        {
            WriteTag(SOLRDOC, stream);
            WriteTag(ORDERED_MAP, stream, solrDocument.Fields.Count);
            foreach (var entry in solrDocument.Fields)
            {
                WriteExternString(entry.Key, stream);
                WriteVal(entry.Value, stream);
            }
        }

        private void WriteSolrDocumentList(SolrDocumentList value, SolrBinaryStream stream)
        {
            WriteTag(SOLRDOCLST, stream);
            IList l = new ArrayList(3);
            l.Add(value.NumFound);
            l.Add(value.Start);
            l.Add(value.MaxScore);
            WriteArray(l, stream);
            WriteArray(value, stream);
        }

        private void WriteArray(IList value, SolrBinaryStream stream)
        {
            WriteTag(ARR, stream, value.Count);
            foreach (var item in value)
                WriteVal(item, stream);
        }

        private void WriteNamedList(INamedList value, SolrBinaryStream stream)
        {
            //TODO: handle orderedMap? value is SimpleOrderedMap ? ORDERED_MAP : NAMED_LST
            WriteTag(NAMED_LST, stream, value.Count);
            for (int i = 0; i < value.Count; i++)
            {
                String name = value.GetKey(i);
                WriteExternString(name, stream);
                Object val = value.Get(i);
                WriteVal(val, stream);
            }
        }

        private void WriteExternString(string value, SolrBinaryStream stream)
        {
            if (value == null)
            {
                WriteTag(NULL, stream);
                return;
            }
            int idx;
            if (ExtenStringsMap.TryGetValue(value, out idx))
            {
                WriteTag(EXTERN_STRING, stream, idx);
            }
            else
            {
                WriteString(value, stream);
                ExtenStringsMap[value] = ExtenStringsMap.Count;
            }
        }

        private bool WritePrimitive(object value, SolrBinaryStream stream)
        {
            if (value == null)
            {
                stream.Write(NULL);
                return true;
            }
            if (value is string)
            {
                WriteString((string)value, stream);
                return true;
            }
            if (value is int)
            {
                WriteInt((int)value, stream);
                return true;
            }
            if (value is long)
            {
                WriteLong((long)value, stream);
                return true;
            }
            if (value is float)
            {
                WriteFloat((float)value, stream);
                return true;
            }
            if (value is double)
            {
                stream.Write(DOUBLE);
                stream.WriteDouble((double)value);
                return true;
            }
            if (value is byte)
            {
                stream.Write(BYTE);
                stream.Write((byte)value);
                return true;
            }
            if (value is short)
            {
                stream.Write(SHORT);
                stream.WriteShort((short)value);
                return true;
            }
            if (value is DateTime)
            {
                stream.Write(DATE);
                stream.WriteLong((long)((DateTime)value).ToUniversalTime().Subtract(utcDateTime).TotalMilliseconds);
                return true;
            }
            if (value is bool)
            {
                if ((bool)value) stream.Write(BOOL_TRUE);
                else stream.Write(BOOL_FALSE);
                return true;
            }
            if (value is byte[])
            {
                var buf = (byte[])value;
                WriteByteArray(buf, 0, buf.Length, stream);
                return true;
            }
            if (value == END_OBJ)
            {
                WriteTag(END, stream);
                return true;
            }
            return false;
        }

        private void WriteByteArray(byte[] buf, int offset, int length, SolrBinaryStream stream)
        {
            WriteTag(BYTEARR, stream, length);
            stream.Write(buf, offset, length);
        }

        private void WriteFloat(float value, SolrBinaryStream stream)
        {
            stream.Write(FLOAT);
            stream.WriteFloat(value);
        }

        private void WriteLong(long value, SolrBinaryStream stream)
        {
            if (((ulong)value & 0xff00000000000000L) == 0)
            {
                int b = SLONG | ((int)value & 0x0f);
                if (value >= 0x0f)
                {
                    b |= 0x10;
                    stream.Write(b);
                    WriteVLong(value >> 4, stream);
                }
                else
                    stream.Write(b);
            }
            else
            {
                stream.Write(LONG);
                stream.WriteLong(value);
            }
        }

        private void WriteVLong(long value, SolrBinaryStream stream)
        {
            while ((value & ~0x7F) != 0)
            {
                stream.Write((byte)((value & 0x7f) | 0x80));
                value >>= 7;
            }
            stream.Write((byte)value);
        }

        private void WriteInt(int value, SolrBinaryStream stream)
        {
            if (value > 0)
            {
                int b = SINT | (value & 0x0f);
                if (value >= 0x0f)
                {
                    b |= 0x10;
                    stream.Write(b);
                    WriteVInt(value >> 4, stream);
                }
                else
                    stream.Write(b);
            }
            else
            {
                stream.Write(INT);
                stream.WriteInt(value);
            }
        }

        private void WriteString(string value, SolrBinaryStream stream)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            WriteTag(STR, stream, bytes.Length);
            stream.Write(bytes, 0, bytes.Length);
        }

        private void WriteTag(byte tag, SolrBinaryStream stream, int? length = null)
        {
            if (length.HasValue)
            {
                if ((tag & 0xe0) != 0)
                {
                    if (length.Value < 0x1f)
                    {
                        stream.Write(tag | length.Value);
                    }
                    else
                    {
                        stream.Write(tag | 0x1f);
                        WriteVInt(length.Value - 0x1f, stream);
                    }
                }
                else
                {
                    stream.Write(tag);
                    WriteVInt(length.Value, stream);
                }
            }
            else
            {
                stream.Write(tag);
            }
        }

        private void WriteVInt(int value, SolrBinaryStream stream)
        {
            while ((value & ~0x7F) != 0)
            {
                stream.Write(((value & 0x7f) | 0x80));
                value >>= 7;
            }
            stream.Write(value);
        }

        #endregion Write
    }
}