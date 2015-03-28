using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class SendAuth
{
    // Methods
    private SendAuth()
    {
    }

    // Nested Types
    public class Req : BaseReq
    {
        // Fields
        private const int LENGTH_LIMIT = 0x400;
        public string Scope;
        public string State;
        private const string TAG = "MicroMsg.SendAuth.Req";

        // Methods
        internal Req()
        {
        }

        public Req(string scope, string state)
        {
            this.Scope = scope;
            this.State = state;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                SendAuthReq req = protoObj as SendAuthReq;
                if (req != null)
                {
                    base.Transaction = req.Base.Transaction;
                    this.Scope = req.Scope;
                    this.State = req.State;
                }
            }
        }

        internal override object ToProto()
        {
            BaseReqP.Builder builder = BaseReqP.CreateBuilder();
            builder.Type = (uint) this.Type();
            builder.Transaction = base.Transaction;
            SendAuthReq.Builder builder2 = SendAuthReq.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.Scope = this.Scope;
            builder2.State = this.State;
            return builder2.Build();
        }

        public override int Type()
        {
            return 1;
        }

        internal override bool ValidateData()
        {
            if (((this.Scope == null) || (this.Scope.Length == 0)) || (this.Scope.Length > 0x400))
            {
                throw new WXException(1, "Scope is invalid.");
            }
            if ((this.State != null) && (this.State.Length > 0x400))
            {
                throw new WXException(1, "State is invalid.");
            }
            return true;
        }
    }

    public class Resp : BaseResp
    {
        // Fields
        public string Code;
        private const int LENGTH_LIMIT = 0x400;
        public string State;
        private const string TAG = "MicroMsg.SendAuth.Resp";
        public string Url;

        // Methods
        internal Resp()
        {
        }

        public Resp(string transaction, int errCode, string errString)
        {
            base.Transaction = transaction;
            base.ErrCode = errCode;
            base.ErrStr = errString;
        }

        public Resp(string code, string state, string url)
        {
            this.Code = code;
            this.State = state;
            this.Url = url;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                SendAuthResp resp = protoObj as SendAuthResp;
                if (resp != null)
                {
                    base.Transaction = resp.Base.Transaction;
                    base.ErrCode = (int) resp.Base.ErrCode;
                    base.ErrStr = resp.Base.ErrStr;
                    this.Code = resp.Code;
                    this.State = resp.State;
                    this.Url = resp.Url;
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
            SendAuthResp.Builder builder2 = SendAuthResp.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.Code = string.IsNullOrEmpty(this.Code) ? "" : this.Code;
            builder2.State = string.IsNullOrEmpty(this.State) ? "" : this.State;
            builder2.Url = string.IsNullOrEmpty(this.Url) ? "" : this.Url;
            return builder2.Build();
        }

        public override int Type()
        {
            return 1;
        }

        internal override bool ValidateData()
        {
            if ((this.State != null) && (this.State.Length > 0x400))
            {
                throw new WXException(1, "State is invalid.");
            }
            return true;
        }
    }
}
}
