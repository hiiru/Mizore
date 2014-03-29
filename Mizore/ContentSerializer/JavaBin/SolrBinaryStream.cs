using System.IO;
using System.Runtime.InteropServices;

namespace Mizore.ContentSerializer.JavaBin
{
    public class SolrBinaryStream : Stream
    {
        protected readonly Stream InputStream;
        protected readonly byte[] ReadBuffer = new byte[8];
        protected FloatingPointConverter converter = new FloatingPointConverter();

        public SolrBinaryStream(Stream stream)
        {
            InputStream = stream;
        }

        #region Stream Implementation

        public override void Flush()
        {
            InputStream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return InputStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            InputStream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return InputStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            InputStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get { return InputStream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return InputStream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return InputStream.CanWrite; }
        }

        public override long Length
        {
            get { return InputStream.Length; }
        }

        public override long Position
        {
            get { return InputStream.Position; }
            set { InputStream.Position = value; }
        }

        public override int ReadByte()
        {
            return InputStream.ReadByte();
        }

        #endregion Stream Implementation

        #region Read

        public short ReadShort()
        {
            Read(ReadBuffer, 0, 2);
            return (short)((ReadBuffer[0] << 8) | ReadBuffer[1]);
        }

        public int ReadInt()
        {
            Read(ReadBuffer, 0, 4);
            return ((ReadBuffer[0] << 24)
                    | (ReadBuffer[1] << 16)
                    | (ReadBuffer[2] << 8)
                    | ReadBuffer[3]);
        }

        public long ReadLong()
        {
            Read(ReadBuffer, 0, 8);

            return (((long)ReadBuffer[0]) << 56)
                   | (((long)ReadBuffer[1]) << 48)
                   | (((long)ReadBuffer[2]) << 40)
                   | (((long)ReadBuffer[3]) << 32)
                   | (((long)ReadBuffer[4]) << 24)
                   | (uint)(ReadBuffer[5] << 16)
                   | (uint)(ReadBuffer[6] << 8)
                   | (uint)(ReadBuffer[7]);
            //return BitConverter.ToInt64(ReadBuffer, 0);
        }

        public float ReadFloat()
        {
            converter.Int = ReadInt();
            return converter.Single;
            //Read(ReadBuffer, 0, 4);
            //return BitConverter.ToSingle(ReadBuffer, 0);
        }

        public double ReadDouble()
        {
            converter.Long = ReadLong();
            return converter.Double;
            //Read(ReadBuffer, 0, 8);
            //return BitConverter.ToDouble(ReadBuffer, 0);
        }

        #endregion Read

        #region Write

        /// <summary>
        ///
        /// </summary>
        /// <param name="b"></param>
        public void Write(int b)
        {
            InputStream.WriteByte((byte)b);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="b"></param>
        public void Write(byte[] b)
        {
            InputStream.Write(b, 0, b.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteBoolean(bool v)
        {
            Write(v ? 1 : 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteShort(int v)
        {
            Write((byte)(v >> 8));
            Write((byte)v);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteChar(int v)
        {
            WriteShort(v);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteInt(int v)
        {
            WriteByte((byte)(v >> 24));
            WriteByte((byte)(v >> 16));
            WriteByte((byte)(v >> 8));
            WriteByte((byte)(v));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteLong(long v)
        {
            WriteByte((byte)(v >> 56));
            WriteByte((byte)(v >> 48));
            WriteByte((byte)(v >> 40));
            WriteByte((byte)(v >> 32));
            WriteByte((byte)(v >> 24));
            WriteByte((byte)(v >> 16));
            WriteByte((byte)(v >> 8));
            WriteByte((byte)(v));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteFloat(float v)
        {
            converter.Single = v;
            WriteInt(converter.Int);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="v"></param>
        public void WriteDouble(double v)
        {
            converter.Double = v;
            WriteLong(converter.Long);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        public void WriteBytes(string s)
        {
            foreach (char c in s)
            {
                WriteByte((byte)c);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="s"></param>
        public void WriteChars(string s)
        {
            foreach (char c in s)
            {
                WriteChar(c);
            }
        }

        #endregion Write

        [StructLayout(LayoutKind.Explicit)]
        protected struct FloatingPointConverter
        {
            [FieldOffset(0)]
            public float Single;

            [FieldOffset(0)]
            public int Int;

            [FieldOffset(1)]
            public double Double;

            [FieldOffset(1)]
            public long Long;
        }
    }
}