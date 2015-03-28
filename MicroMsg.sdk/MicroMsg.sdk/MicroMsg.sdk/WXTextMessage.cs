using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
   public class WXTextMessage : WXBaseMessage
{
    // Fields
    private const int LENGTH_LIMIT = 0x2800;
    private const string TAG = "MicroMsg.WXTextMessage";
    public string Text;

    // Methods
    public WXTextMessage()
    {
    }

    public WXTextMessage(string text)
    {
        this.Text = text;
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
                if (ep.TextMessage != null)
                {
                    this.Text = ep.TextMessage.Text;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXTextMessageP.Builder builder = WXTextMessageP.CreateBuilder();
        builder.Text = this.Text;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        if (base.ThumbData == null)
        {
            base.ThumbData = new byte[0];
        }
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.TextMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 1;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if (((this.Text == null) || (this.Text.Length == 0)) || (this.Text.Length > 0x2800))
        {
            throw new WXException(1, "Text is invalid.");
        }
        return true;
    }
}

}
