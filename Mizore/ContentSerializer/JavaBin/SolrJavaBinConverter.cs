using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        #endregion

        public void WriteJavaBin(INamedList list, Stream stream)
        {
            throw new NotImplementedException();
        }

        public INamedList ReadJavaBin(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
