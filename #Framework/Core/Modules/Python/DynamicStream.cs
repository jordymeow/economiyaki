using System.Text;
using System.IO;

namespace Finance.Framework.Core
{
    public class DynamicStream : Stream
    {
        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return true; }
        }

        public override void Flush()
        {
        }

        public override long Length
        {
            get { return 0; }
        }

        public override long Position
        {
            get
            {
                return -1;
            }
            set
            {
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return -1;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return -1;
        }

        public override void SetLength(long value)
        {
        }

        public delegate void TextReceivedEventHandler(object sender, string txt);
        public event TextReceivedEventHandler TextReceivedEvent;

        public override void Write(byte[] buffer, int offset, int count)
        {
            string s = Encoding.Default.GetString(buffer, offset, count);
            s = s.Trim();
            if (!string.IsNullOrEmpty(s))
                TextReceivedEvent.Invoke(this, s);
        }
    }
}
