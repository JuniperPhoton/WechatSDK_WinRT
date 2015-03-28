using Google.ProtocolBuffers;
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXAppExtendMessage : WXBaseMessage
{
    // Fields
    private const int CONTENT_Length_LIMIT = 0xa00000;
    public string ExtInfo;
    private const int EXTINFO_Length_LIMIT = 0x800;
    public byte[] FileData;
    public string FileName;
    private const int PATH_Length_LIMIT = 0x2800;
    private const string TAG = "MicroMsg.WXAppExtendMessage";

    // Methods
    public WXAppExtendMessage()
    {
        this.ExtInfo = "";
        this.FileName = "";
        this.FileData = new byte[0];
    }

    public WXAppExtendMessage(string extInfo, string fileName, byte[] fileData)
    {
        this.ExtInfo = "";
        this.FileName = "";
        this.FileData = new byte[0];
        this.ExtInfo = extInfo;
        this.FileData = fileData;
        this.FileName = fileName;
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
                if (ep.AppExtendMessage != null)
                {
                    this.FileData = ep.AppExtendMessage.FileData.ToByteArray();
                    this.ExtInfo = ep.AppExtendMessage.ExtInfo;
                    this.FileName = ep.AppExtendMessage.FileName;
                }
            }
        }
    }

    internal override object ToProto()
    {
        WXAppExtendMessageP.Builder builder = WXAppExtendMessageP.CreateBuilder();
        builder.FileData = ByteString.CopyFrom(this.FileData);
        builder.ExtInfo = this.ExtInfo;
        builder.FileName = this.FileName;
        WXMessageP.Builder builder2 = WXMessageP.CreateBuilder();
        builder2.Type = (uint) this.Type();
        builder2.Title = base.Title;
        builder2.Description = base.Description;
        builder2.ThumbData = ByteString.CopyFrom(base.ThumbData);
        builder2.AppExtendMessage = builder.Build();
        return builder2.Build();
    }

    public override int Type()
    {
        return 7;
    }

    internal override bool ValidateData()
    {
        if (!base.ValidateData())
        {
            return false;
        }
        if ((((this.ExtInfo == null) || (this.ExtInfo.Length == 0)) && ((this.FileName == null) || (this.FileName.Length == 0))) && ((this.FileData == null) || (this.FileData.Length == 0)))
        {
            throw new WXException(1, "All arguments is null.");
        }
        if ((this.ExtInfo != null) && (this.ExtInfo.Length > 0x800))
        {
            throw new WXException(1, "ExtInfo is invalid.");
        }
        if ((this.FileName != null) && (this.FileName.Length > 0x2800))
        {
            throw new WXException(1, "FilePath is invalid.");
        }
        if ((this.FileData != null) && (this.FileData.Length > 0xa00000))
        {
            throw new WXException(1, "FileData is invalid.");
        }
        return true;
    }
}
}
