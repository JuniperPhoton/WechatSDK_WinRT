
using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class GetMessageFromWX
{
    // Methods
    private GetMessageFromWX()
    {
    }

    // Nested Types
    public class Req : BaseReq
    {
        // Fields
        public string Username;

        // Methods
        internal Req()
        {
        }

        public Req(string username)
        {
            this.Username = username;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                GetMessageFromWXReq req = protoObj as GetMessageFromWXReq;
                if (req != null)
                {
                    base.Transaction = req.Base.Transaction;
                    this.Username = req.Username;
                }
            }
        }

        internal override object ToProto()
        {
            BaseReqP.Builder builder = BaseReqP.CreateBuilder();
            builder.Type = (uint) this.Type();
            builder.Transaction = base.Transaction;
            GetMessageFromWXReq.Builder builder2 = GetMessageFromWXReq.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.Username = this.Username;
            return builder2.Build();
        }

        public override int Type()
        {
            return 3;
        }

        internal override bool ValidateData()
        {
            if (string.IsNullOrEmpty(this.Username))
            {
                throw new WXException(1, "Username can't be empty.");
            }
            return true;
        }
    }

    public class Resp : BaseResp
    {
        // Fields
        public WXBaseMessage Message;
        public string Username;

        // Methods
        internal Resp()
        {
            this.Username = "";
        }

        public Resp(string transaction, int errCode, string errString)
        {
            this.Username = "";
            base.Transaction = transaction;
            base.ErrCode = errCode;
            base.ErrStr = errString;
        }

        public Resp(string transaction, string username, WXBaseMessage message)
        {
            this.Username = "";
            this.Username = username;
            this.Message = message;
            base.Transaction = transaction;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                GetMessageFromWXResp resp = protoObj as GetMessageFromWXResp;
                if (resp != null)
                {
                    base.Transaction = resp.Base.Transaction;
                    base.ErrCode = (int) resp.Base.ErrCode;
                    base.ErrStr = resp.Base.ErrStr;
                    this.Username = resp.Username;
                    this.Message = WXBaseMessage.CreateMessage((int) resp.Msg.Type);
                    if (this.Message != null)
                    {
                        this.Message.FromProto(resp.Msg);
                    }
                }
            }
        }

        internal override object ToProto()
        {
            BaseRespP.Builder builder = BaseRespP.CreateBuilder();
            builder.Type = (uint) this.Type();
            builder.Transaction = base.Transaction;
            builder.ErrCode = (uint) base.ErrCode;
            builder.ErrStr = base.ErrStr;
            GetMessageFromWXResp.Builder builder2 = GetMessageFromWXResp.CreateBuilder();
            builder2.Base = builder.Build();
            if (this.Message != null)
            {
                WXMessageP ep = this.Message.ToProto() as WXMessageP;
                builder2.Msg = ep;
            }
            builder2.Username = this.Username;
            return builder2.Build();
        }

        public override int Type()
        {
            return 3;
        }

        internal override bool ValidateData()
        {
            if (string.IsNullOrEmpty(this.Username))
            {
                throw new WXException(1, "Username can't be empty.");
            }
            if (base.ErrCode != 0)
            {
                return true;
            }
            if (this.Message == null)
            {
                throw new WXException(1, "Message can't be null.");
            }
            return this.Message.ValidateData();
        }
    }
}


}
