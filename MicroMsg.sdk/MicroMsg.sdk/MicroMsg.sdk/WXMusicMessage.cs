using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXMusicMessage : WXBaseMessage
{
    // Fields
    private const int LENGTH_LIMIT = 0x2800;
    public string MusicLowBandUrl = "";
    public string MusicUrl = "";
    private const string TAG = "MicroMsg.WXMusicMessage";

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
                if (ep.MusicMessage != null)
                {
                    this.MusicUrl = ep.MusicMessage.MusicUrl;
                    this.MusicLowBandUrl = ep.MusicMessage.MusicLowBandUrl;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXMusicMessageP.Builder builder = WXMusicMessageP.CreateBuilder();
        builder.MusicUrl = this.MusicUrl;
        builder.MusicLowBandUrl = this.MusicLowBandUrl;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.MusicMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 3;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if (((this.MusicUrl == null) || (this.MusicUrl.Length == 0)) && ((this.MusicLowBandUrl == null) || (this.MusicLowBandUrl.Length == 0)))
        {
            throw new WXException(1, "Both arguments are null.");
        }
        if ((this.MusicUrl != null) && (this.MusicUrl.Length > 0x2800))
        {
            throw new WXException(1, "MusicUrl is too long.");
        }
        if ((this.MusicLowBandUrl != null) && (this.MusicLowBandUrl.Length > 0x2800))
        {
            throw new WXException(1, "MusicLowBandUrl is too long.");
        }
        return true;
    }
}

 

 

}
