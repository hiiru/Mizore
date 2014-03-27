using System;
using System.IO;

namespace Mizore.ContentSerializer.JavaBin.ConvertedSolrjClasses
{
    public class SolrBufferedStream : Stream
    {
        protected readonly Stream InputStream;
        protected readonly byte[] ReadBuffer = new byte[8];

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

        public override long Position { get { return InputStream.Position; } set { InputStream.Position = value; } }

        public override int ReadByte()
        {
            return InputStream.ReadByte();
        }

        public short ReadShort()
        {
            Read(ReadBuffer, 0, 2);
            return BitConverter.ToInt16(ReadBuffer, 0);
        }

        public int ReadInt()
        {
            Read(ReadBuffer, 0, 4);
            return BitConverter.ToInt32(ReadBuffer, 0);
        }

        public long ReadLong()
        {
            Read(ReadBuffer, 0, 8);
            return BitConverter.ToInt64(ReadBuffer, 0);
        }

        public float ReadFloat()
        {
            Read(ReadBuffer, 0, 4);
            return BitConverter.ToSingle(ReadBuffer, 0);
        }

        public double ReadDouble()
        {
            Read(ReadBuffer, 0, 8);
            return BitConverter.ToDouble(ReadBuffer, 0);
        }
    }
}