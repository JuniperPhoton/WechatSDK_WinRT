using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class SendMessageToWX
{
    // Methods
    private SendMessageToWX()
    {
    }

    // Nested Types
    public class Req : BaseReq
    {
        // Fields
        public WXBaseMessage Message;
        public int Scene;
        private const string TAG = "MicroMsg.SendMessageToWX.Req";
        public const int WXSceneChooseByUser = 0;
        public const int WXSceneSession = 1;
        public const int WXSceneTimeline = 2;

        // Methods
        internal Req()
        {
        }

        public Req(WXBaseMessage message, int scene = 1)
        {
            this.Message = message;
            this.Scene = scene;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                SendMessageToWXReq req = protoObj as SendMessageToWXReq;
                if (req != null)
                {
                    base.Transaction = req.Base.Transaction;
                    this.Scene = (int) req.Scene;
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
            SendMessageToWXReq.Builder builder2 = SendMessageToWXReq.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.Scene = (uint) this.Scene;
            if (this.Message != null)
            {
                WXMessageP ep = this.Message.ToProto() as WXMessageP;
                builder2.Msg = ep;
            }
            return builder2.Build();
        }

        public override int Type()
        {
            return 2;
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
                SendMessageToWXResp resp = protoObj as SendMessageToWXResp;
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
            SendMessageToWXResp.Builder builder2 = SendMessageToWXResp.CreateBuilder();
            builder2.Base = builder.Build();
            return builder2.Build();
        }

        public override int Type()
        {
            return 2;
        }

        internal override bool ValidateData()
        {
            return true;
        }
    }
}

}
