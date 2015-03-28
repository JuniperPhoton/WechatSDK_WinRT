using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public abstract class WXBaseMessage
{
    // Fields
    public string Description = "";
    private const int DESCRIPTION_LENGTH_LIMIT = 0x400;
    private const string TAG = "MicroMsg.WXBaseMessage";
    private const int THUMB_LENGTH_LIMIT = 0x8000;
    public byte[] ThumbData = new byte[0];
    public string Title = "";
    private const int TITLE_LENGTH_LIMIT = 0x200;
    public const int TYPE_APPDATA = 7;
    public const int TYPE_EMOJI = 8;
    public const int TYPE_FILE = 6;
    public const int TYPE_IMAGE = 2;
    public const int TYPE_MUSIC = 3;
    public const int TYPE_TEXT = 1;
    public const int TYPE_UNKNOWN = 0;
    public const int TYPE_URL = 5;
    public const int TYPE_VIDEO = 4;

    // Methods
    protected WXBaseMessage()
    {
    }

    internal static WXBaseMessage CreateMessage(int type)
    {
        switch (type)
        {
            case 1:
                return new WXTextMessage();

            case 2:
                return new WXImageMessage();

            case 3:
                return new WXMusicMessage();

            case 4:
                return new WXVideoMessage();

            case 5:
                return new WXWebpageMessage();

            case 6:
                return new WXFileMessage();

            case 7:
                return new WXAppExtendMessage();

            case 8:
                return new WXEmojiMessage();
        }
        return null;
    }

    internal abstract void FromProto(object protoObj);
    public void SetThumbImage(byte[] thumbData)
    {
        this.ThumbData = thumbData;
    }

    internal abstract object ToProto();
    public abstract int Type();
    internal virtual bool ValidateData()
    {
        if ((this.Title != null) && (this.Title.Length > 0x200))
        {
            throw new WXException(1, "Title is invalid.");
        }
        if ((this.Description != null) && (this.Description.Length > 0x400))
        {
            throw new WXException(1, "Description is invalid.");
        }
        if ((this.ThumbData != null) && (this.ThumbData.Length > 0x8000))
        {
            throw new WXException(1, "ThumbData is invalid.");
        }
        return true;
    }
}
}
