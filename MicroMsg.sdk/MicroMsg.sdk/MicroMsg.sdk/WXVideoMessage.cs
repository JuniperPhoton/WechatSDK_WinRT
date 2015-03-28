using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXVideoMessage : WXBaseMessage
{
    // Fields
    private const int LENGTH_LIMIT = 0x2800;
    private const string TAG = "MicroMsg.WXVideoMessage";
    public string VideoLowBandUrl = "";
    public string VideoUrl = "";

    // Methods
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
                if (ep.VideoMessage != null)
                {
                    this.VideoUrl = ep.VideoMessage.VideoUrl;
                    this.VideoLowBandUrl = ep.VideoMessage.VideoLowBandUrl;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXVideoMessageP.Builder builder = WXVideoMessageP.CreateBuilder();
        builder.VideoUrl = this.VideoUrl;
        builder.VideoLowBandUrl = this.VideoLowBandUrl;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.VideoMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 4;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if (((this.VideoUrl == null) || (this.VideoUrl.Length == 0)) && ((this.VideoLowBandUrl == null) || (this.VideoLowBandUrl.Length == 0)))
        {
            throw new WXException(1, "Both arguments are null.");
        }
        if ((this.VideoUrl != null) && (this.VideoUrl.Length > 0x2800))
        {
            throw new WXException(1, "VideoUrl is too long.");
        }
        if ((this.VideoLowBandUrl != null) && (this.VideoLowBandUrl.Length > 0x2800))
        {
            throw new WXException(1, "VideoLowBandUrl is too long.");
        }
        return true;
    }
}

 

 

}
