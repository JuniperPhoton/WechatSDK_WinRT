using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXWebpageMessage : WXBaseMessage
{
    // Fields
    private const int LENGTH_LIMIT = 0x2800;
    private const string TAG = "MicroMsg.WXWebpageMessage";
    public string WebpageUrl;

    // Methods
    public WXWebpageMessage()
    {
    }

    public WXWebpageMessage(string url)
    {
        this.WebpageUrl = url;
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
                if (ep.WebpageMessage != null)
                {
                    this.WebpageUrl = ep.WebpageMessage.WebpageUrl;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXWebpageMessageP.Builder builder = WXWebpageMessageP.CreateBuilder();
        builder.WebpageUrl = this.WebpageUrl;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.WebpageMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 5;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if (((this.WebpageUrl == null) || (this.WebpageUrl.Length == 0)) || (this.WebpageUrl.Length > 0x2800))
        {
            throw new WXException(1, "WebpageUrl is invalid.");
        }
        return true;
    }
}

}
