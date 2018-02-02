using System;

namespace Finance.Framework.Core
{
    public class GfxMessage
    {
        public DateTime Time;
        public object Content;
        public Type ContentType;
        public GfxType MessageType;
        public Guid MessageID = Guid.NewGuid();

        public GfxMessage(GfxType messageType, object content)
        {
            Time = DateTime.Now;
            ContentType = content.GetType();
            Content = content;
            MessageType = messageType;
        }

        public override string ToString()
        {
            string[] splitted = ContentType.ToString().Split('.');
            return splitted[splitted.Length - 1];
        }
    }
}
