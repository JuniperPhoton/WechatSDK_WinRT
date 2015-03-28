using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
   public class WXEmojiMessage : WXBaseMessage
{
    // Fields
    private const int CONTENT_LENGTH_LIMIT = 0xa00000;
    public byte[] EmojiData;
    private const string TAG = "MicroMsg.WXEmojiMessage";

    // Methods
    public WXEmojiMessage()
    {
    }

    public WXEmojiMessage(byte[] emojiData)
    {
        this.EmojiData = emojiData;
    }

    internal override void FromProto(object protoObj)
    {
        if (protoObj != null)
        {
            WXMessageP ep = protoObj as WXMessageP;
            if (ep != null)
            {
                base.Title = ep.Title;
                base.Description = ep.Description;
                base.ThumbData = ep.ThumbData.ToByteArray();
                if (ep.EmojiMessage != null)
                {
                    this.EmojiData = ep.EmojiMessage.EmojiData.ToByteArray();
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXEmojiMessageP.Builder builder = WXEmojiMessageP.CreateBuilder();
        builder.EmojiData = ByteString.CopyFrom(this.EmojiData);
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.EmojiMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 8;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if ((this.EmojiData == null) || (this.EmojiData.Length == 0))
        {
            throw new WXException(1, "EmojiData is invalid.");
        }
        if ((this.EmojiData != null) && (this.EmojiData.Length > 0xa00000))
        {
            throw new WXException(1, "EmojiData is invalid.");
        }
        return true;
    }
}

 

 

}
