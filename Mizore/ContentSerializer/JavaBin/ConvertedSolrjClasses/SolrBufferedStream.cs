using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mizore.ContentSerializer.JavaBin.ConvertedSolrjClasses
{
    public class SolrBufferedStream : Stream
    {
        protected readonly Stream InputStream;
        protected readonly byte[] ReadBuffer = new byte[8];
        protected FloatingPointConverter converter = new FloatingPointConverter();

        public SolrBufferedStream(Stream stream)
        {
            InputStream = stream;
        }

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

        public short ReadShort()
        {
            Read(ReadBuffer, 0, 2);
            return (short) ((ReadBuffer[0] << 8) | ReadBuffer[1]);
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

            return (((long) ReadBuffer[0]) << 56)
                   | (((long) ReadBuffer[1]) << 48)
                   | (((long) ReadBuffer[2]) << 40)
                   | (((long) ReadBuffer[3]) << 32)
                   | (((long) ReadBuffer[4]) << 24)
                   | (uint) (ReadBuffer[5] << 16)
                   | (uint) (ReadBuffer[6] << 8)
                   | (uint) (ReadBuffer[7]);
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