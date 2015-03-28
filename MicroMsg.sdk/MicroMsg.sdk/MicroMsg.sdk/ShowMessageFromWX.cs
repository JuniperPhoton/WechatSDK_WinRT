using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class ShowMessageFromWX
{
    // Methods
    private ShowMessageFromWX()
    {
    }

    // Nested Types
    public class Req : BaseReq
    {
        // Fields
        public WXBaseMessage Message;

        // Methods
        internal Req()
        {
        }

        public Req(WXBaseMessage message)
        {
            this.Message = message;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                ShowMessageFromWXReq req = protoObj as ShowMessageFromWXReq;
                if (req != null)
                {
                    base.Transaction = req.Base.Transaction;
                    this.Message = WXBaseMessage.CreateMessage((int) req.Msg.Type);
                    if (this.Message != null)
                    {
                        this.Message.FromProto(req.Msg);
                    }
                }
            }
        }

        internal override object ToProto()
        {
            BaseReqP.Builder builder = BaseReqP.CreateBuilder();
            builder.Type = (uint) this.Type();
            builder.Transaction = base.Transaction;
            ShowMessageFromWXReq.Builder builder2 = ShowMessageFromWXReq.CreateBuilder();
            builder2.Base = builder.Build();
            if (this.Message != null)
            {
                WXMessageP ep = this.Message.ToProto() as WXMessageP;
                builder2.Msg = ep;
            }
            return builder2.Build();
        }

        public override int Type()
        {
            return 4;
        }

        internal override bool ValidateData()
        {
            if (this.Message == null)
            {
                throw new WXException(1, "Message can't be null.");
            }
            return this.Message.ValidateData();
        }
    }

    public class Resp : BaseResp
    {
        // Methods
        public Resp()
        {
        }

        public Resp(string transaction, int errCode, string errString)
        {
            base.Transaction = transaction;
            base.ErrCode = errCode;
            base.ErrStr = errString;
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
            return builder2.Build();
        }

        public override int Type()
        {
            return 4;
        }

        internal override bool ValidateData()
        {
            return true;
        }
    }
}

 

 

}
