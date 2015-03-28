using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
   public class WXFileMessage : WXBaseMessage
{
    // Fields
    private const int CONTENT_LENGTH_LIMIT = 0xa00000;
    public byte[] FileData;
    public string FileName;
    private const string TAG = "MicroMsg.WXFileMessage";

    // Methods
    public WXFileMessage()
    {
    }

    public WXFileMessage(byte[] fileData, string FileName)
    {
        this.FileData = fileData;
        this.FileName = FileName;
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
                if (ep.FileMessage != null)
                {
                    this.FileData = ep.FileMessage.FileData.ToByteArray();
                    this.FileName = ep.FileMessage.FileName;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXFileMessageP.Builder builder = WXFileMessageP.CreateBuilder();
        builder.FileData = ByteString.CopyFrom(this.FileData);
        builder.FileName = this.FileName;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.FileMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 6;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if ((this.FileName == null) || (this.FileName.Length == 0))
        {
            throw new WXException(1, "FileName is invalid.");
        }
        if ((this.FileData != null) && ((this.FileData.Length == 0) || (this.FileData.Length > 0xa00000)))
        {
            throw new WXException(1, "FileData is invalid.");
        }
        return true;
    }
}

 

 

}
