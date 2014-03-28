using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mizore.ContentSerializer.easynet_Javabin;
using Mizore.ContentSerializer.JavaBin.ConvertedSolrjClasses;
using Mizore.util;

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
        //private char[] chars;
        #endregion
        private static readonly DateTime utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        //holds all the extern strings
        private readonly List<String> ExternStrings=new List<string>();

        public void WriteJavaBin(INamedList list, Stream stream)
        {
            throw new NotImplementedException();
        }

        public INamedList ReadJavaBin(Stream stream)
        {
            var solrStream = new SolrBufferedStream(stream);
            var version = solrStream.ReadByte();
            if (version != VERSION)
            {
                throw new Exception("Invalid version (expected " + VERSION + ", but " + version + ") or the data in not in 'javabin' format");
            }
            return ReadVal(solrStream) as INamedList;
        }

        public object ReadVal(SolrBufferedStream stream)
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

                    //    if (timestamp <= -315537897600000 || timestamp >= 315537897600000)
                    //{
                        
                    //}
                        DateTime date;
                        try {
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

        private object ReadMapEntry(SolrBufferedStream stream)
        {
            var key = ReadVal(stream);
            var value = ReadVal(stream);
            return new KeyValuePair<object, object>(key, value);
        }

        private object ReadEnumFieldValue(SolrBufferedStream stream)
        {
            var intValue = (int)ReadVal(stream);
            var stringValue = (string)ReadVal(stream);
            return new Tuple<int,string>(intValue, stringValue);
        }

        private object ReadSolrInputDocument(SolrBufferedStream stream)
        {
            int sz = ReadVInt(stream);
            var nl = new EasynetNamedList();
            nl.Add("boost",(float?)ReadVal(stream));

            var dict = new Dictionary<string, INamedList>(sz);
            var children = new ArrayList();
            for (int i = 0; i < sz; i++)
            {
                float boost = 1.0f;
                string fieldName;
                var obj = ReadVal(stream); // could be a boost, a field name, or a child document
                if (obj is float)
                {
                    boost = (float)obj;
                    fieldName = (String)ReadVal(stream);
                } else if (obj is INamedList)
                {
                    children.Add(obj);
                    continue;
                }
                else
                {
                    fieldName = (String)obj;
                }
                Object fieldVal = ReadVal(stream);
                var fields = new EasynetNamedList();
                fields.Add("name", fieldName);
                fields.Add("value", fieldVal);
                fields.Add("boost", boost);
                dict.Add(fieldName,fields);
            }
            if (dict.Count>0)
                nl.Add("fields",dict);
            if (children.Count > 0)
                nl.Add("children", children);
            return nl;
        }

        private IList ReadIterator(SolrBufferedStream stream)
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

        private byte[] ReadByteArray(SolrBufferedStream stream)
        {
            var sz = ReadVInt(stream);
            var arr = new byte[sz];
            stream.Read(arr, 0, sz);
            return arr;
        }

        private INamedList ReadSolrDocumentList(SolrBufferedStream stream)
        {
            var nl = new EasynetNamedList();
            var list = (IList)ReadVal(stream);
            nl.Add("numFound", (long) list[0]);
            nl.Add("start", (long) list[1]);
            nl.Add("maxScore", (float?)list[2]);
            nl.Add("docs",(ArrayList)ReadVal(stream));
            return nl;
        }

        private INamedList ReadSolrDocument(SolrBufferedStream stream)
        {
            return (INamedList)ReadVal(stream);
        }

        private IDictionary<object, object> ReadMap(SolrBufferedStream stream)
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

        private string ReadExternString(SolrBufferedStream stream)
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

        private INamedList ReadNamedList(SolrBufferedStream stream)
        {
            int sz = ReadSize(stream);
            var nl = new EasynetNamedList();
            for (int i = 0; i < sz; i++)
            {
                var key = ReadVal(stream) as string;
                object value = ReadVal(stream);
                nl.Add(key, value);
            }
            return nl;
        }

        private INamedList ReadOrderedMap(SolrBufferedStream stream)
        {
            return ReadNamedList(stream);
            //throw new NotImplementedException();
        }

        private int ReadSize(SolrBufferedStream stream)
        {
            int sz = tagByte & 0x1f;
            if (sz == 0x1f) sz += ReadVInt(stream);
            return sz;
        }

        private int ReadVInt(SolrBufferedStream stream)
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

        private long ReadVLong(SolrBufferedStream stream)
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

        private string ReadString(SolrBufferedStream stream)
        {
            int sz = ReadSize(stream);
            if (bytes == null || bytes.Length < sz) bytes = new byte[sz];
            stream.Read(bytes, 0, sz);
            return Encoding.UTF8.GetString(bytes,0,sz);
        }

        private int ReadSmallInt(SolrBufferedStream stream)
        {
            int v = tagByte & 0x0F;
            if ((tagByte & 0x10) != 0)
                v = (ReadVInt(stream) << 4) | v;
            return v;
        }

        private long ReadSmallLong(SolrBufferedStream stream)
        {
            long v = tagByte & 0x0F;
            if ((tagByte & 0x10) != 0)
                v = (ReadVLong(stream) << 4) | v;
            return v;
        }

        private IList ReadArray(SolrBufferedStream stream)
        {
            var sz = ReadSize(stream);
            var l = new ArrayList(sz);
            for (int i = 0; i < sz; i++)
                l.Add(ReadVal(stream));
            return l;
        }
    }
}
