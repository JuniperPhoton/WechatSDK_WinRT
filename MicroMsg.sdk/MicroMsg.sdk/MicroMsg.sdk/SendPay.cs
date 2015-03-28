using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
   public class SendPay
{
    // Methods
    private SendPay()
    {
    }

    // Nested Types
    public class Req : BaseReq
    {
        // Fields
        private const int LENGTH_LIMIT = 0x400;
        public string NonceStr;
        public string Package;
        public string PartnerId;
        public string PrepayId;
        public string Sign;
        private const string TAG = "MicroMsg.SendPay.Req";
        public uint TimeStamp;

        // Methods
        internal Req()
        {
        }

        public Req(string partnerId, string prepayId, string nonceStr, uint timeStamp, string package, string sign)
        {
            this.PartnerId = partnerId;
            this.PrepayId = prepayId;
            this.NonceStr = nonceStr;
            this.TimeStamp = timeStamp;
            this.Package = package;
            this.Sign = sign;
        }

        internal override void FromProto(object protoObj)
        {
            if (protoObj != null)
            {
                SendPayReqP qp = protoObj as SendPayReqP;
                if (qp != null)
                {
                    base.Transaction = qp.Base.Transaction;
                    this.PartnerId = qp.PartnerId;
                    this.PrepayId = qp.PrepayId;
                    this.NonceStr = qp.NonceStr;
                    this.TimeStamp = qp.TimeStamp;
                    this.Package = qp.Package;
                    this.Sign = qp.Sign;
                }
            }
        }

        internal override object ToProto()
        {
            BaseReqP.Builder builder = BaseReqP.CreateBuilder();
            builder.Type = (uint) this.Type();
            builder.Transaction = base.Transaction;
            SendPayReqP.Builder builder2 = SendPayReqP.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.PartnerId = this.PartnerId;
            builder2.PrepayId = this.PrepayId;
            builder2.NonceStr = this.NonceStr;
            builder2.TimeStamp = this.TimeStamp;
            builder2.Package = this.Package;
            builder2.Sign = this.Sign;
            return builder2.Build();
        }

        public override int Type()
        {
            return 5;
        }

        internal override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(this.PartnerId) || (this.PartnerId.Length > 0x400))
            {
                throw new WXException(1, "PartnerId is invalid.");
            }
            if (string.IsNullOrWhiteSpace(this.PrepayId) || (this.PrepayId.Length > 0x400))
            {
                throw new WXException(1, "PrepayId is invalid.");
            }
            if (string.IsNullOrWhiteSpace(this.NonceStr) || (this.NonceStr.Length > 0x400))
            {
                throw new WXException(1, "NonceStr is invalid.");
            }
            if (this.TimeStamp == 0)
            {
                throw new WXException(1, "TimeStamp is invalid.");
            }
            if (string.IsNullOrWhiteSpace(this.Package) || (this.Package.Length > 0x400))
            {
                throw new WXException(1, "Package is invalid.");
            }
            if (string.IsNullOrWhiteSpace(this.Sign) || (this.Sign.Length > 0x400))
            {
                throw new WXException(1, "Sign is invalid.");
            }
            return true;
        }
    }

    public class Resp : BaseResp
    {
        // Fields
        private const int LENGTH_LIMIT = 0x400;
        public string ReturnKey;
        private const string TAG = "MicroMsg.SendPay.Resp";

        // Methods
        internal Resp()
        {
        }

        public Resp(string returnKey)
        {
            this.ReturnKey = returnKey;
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
                SendPayRespP pp = protoObj as SendPayRespP;
                if (pp != null)
                {
                    base.Transaction = pp.Base.Transaction;
                    base.ErrCode = (int) pp.Base.ErrCode;
                    base.ErrStr = pp.Base.ErrStr;
                    this.ReturnKey = pp.ReturnKey;
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
            SendPayRespP.Builder builder2 = SendPayRespP.CreateBuilder();
            builder2.Base = builder.Build();
            builder2.ReturnKey = string.IsNullOrEmpty(this.ReturnKey) ? "" : this.ReturnKey;
            return builder2.Build();
        }

        public override int Type()
        {
            return 5;
        }

        internal override bool ValidateData()
        {
            return true;
        }
    }
}

}
