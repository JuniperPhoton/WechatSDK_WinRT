using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXImageMessage : WXBaseMessage
{
    // Fields
    private const int CONTENT_LENGTH_LIMIT = 0xa00000;
    public byte[] ImageData;
    public string ImageUrl;
    private const int PATH_LENGTH_LIMIT = 0x2800;
    private const string TAG = "MicroMsg.WXImageMessage";
    private const int URL_LENGTH_LIMIT = 0x2800;

    // Methods
    public WXImageMessage()
    {
        this.ImageData = new byte[0];
        this.ImageUrl = "";
    }

    public WXImageMessage(byte[] imageData)
    {
        this.ImageData = new byte[0];
        this.ImageUrl = "";
        this.ImageData = imageData;
    }

    public WXImageMessage(string imageUrl)
    {
        this.ImageData = new byte[0];
        this.ImageUrl = "";
        this.ImageUrl = imageUrl;
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
                if (ep.ImageMessage != null)
                {
                    this.ImageData = ep.ImageMessage.ImageData.ToByteArray();
                    this.ImageUrl = ep.ImageMessage.ImageUrl;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXImageMessageP.Builder builder = WXImageMessageP.CreateBuilder();
        builder.ImageData = ByteString.CopyFrom(this.ImageData);
        builder.ImageUrl = this.ImageUrl;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.ImageMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 2;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if (((this.ImageData == null) || (this.ImageData.Length == 0)) && ((this.ImageUrl == null) || (this.ImageUrl.Length == 0)))
        {
            throw new WXException(1, "All arguments are null.");
        }
        if ((this.ImageData != null) && (this.ImageData.Length > 0xa00000))
        {
            throw new WXException(1, "ImageData is invalid.");
        }
        if ((this.ImageUrl != null) && (this.ImageUrl.Length > 0x2800))
        {
            throw new WXException(1, "ImageUrl is invalid.");
        }
        return true;
    }
}

 

 

}
