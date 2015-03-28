using protobuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
   public class TransactData
{
    // Fields
    public string AppID;
    public string CheckContent;
    public string CheckSummary;
    public int ConmandID;
    public BaseReq Req;
    public BaseResp Resp;
    public string SdkVersion;

    // Methods
    public bool CheckSupported()
    {
        if ((this.Req == null) && (this.Resp == null))
        {
            return false;
        }
        bool flag = false;
        switch (this.ConmandID)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                flag = true;
                break;

            default:
                flag = false;
                break;
        }
        WXBaseMessage message = null;
        if ((this.Req != null) && (this.Req is SendMessageToWX.Req))
        {
            SendMessageToWX.Req req = this.Req as SendMessageToWX.Req;
            message = req.Message;
        }
        if ((this.Req != null) && (this.Req is ShowMessageFromWX.Req))
        {
            ShowMessageFromWX.Req req2 = this.Req as ShowMessageFromWX.Req;
            message = req2.Message;
        }
        if ((this.Resp != null) && (this.Resp is GetMessageFromWX.Resp))
        {
            GetMessageFromWX.Resp resp = this.Resp as GetMessageFromWX.Resp;
            message = resp.Message;
        }
        if (message != null)
        {
            flag &= (message.Type() >= 0) && (message.Type() <= 8);
        }
        return flag;
    }

    internal void FromProto(object protoObj)
    {
        if (protoObj != null)
        {
            TransactDataP ap = protoObj as TransactDataP;
            if (ap != null)
            {
                this.ConmandID = (int) ap.ConmandID;
                this.AppID = ap.AppID;
                this.SdkVersion = ap.SdkVersion;
                this.CheckContent = ap.CheckContent;
                this.CheckSummary = ap.CheckSummary;
                if (!string.IsNullOrEmpty(ap.GetReq.Base.Transaction))
                {
                    this.Req = new GetMessageFromWX.Req();
                    this.Req.FromProto(ap.GetReq);
                }
                if (!string.IsNullOrEmpty(ap.AuthReq.Base.Transaction))
                {
                    this.Req = new SendAuth.Req();
                    this.Req.FromProto(ap.AuthReq);
                }
                if (!string.IsNullOrEmpty(ap.SendReq.Base.Transaction))
                {
                    this.Req = new SendMessageToWX.Req();
                    this.Req.FromProto(ap.SendReq);
                }
                if (!string.IsNullOrEmpty(ap.ShowReq.Base.Transaction))
                {
                    this.Req = new ShowMessageFromWX.Req();
                    this.Req.FromProto(ap.ShowReq);
                }
                if (!string.IsNullOrEmpty(ap.GetResp.Base.Transaction))
                {
                    this.Resp = new GetMessageFromWX.Resp();
                    this.Resp.FromProto(ap.GetResp);
                }
                if (!string.IsNullOrEmpty(ap.AuthResp.Base.Transaction))
                {
                    this.Resp = new SendAuth.Resp();
                    this.Resp.FromProto(ap.AuthResp);
                }
                if (!string.IsNullOrEmpty(ap.SendResp.Base.Transaction))
                {
                    this.Resp = new SendMessageToWX.Resp();
                    this.Resp.FromProto(ap.SendResp);
                }
                if (!string.IsNullOrEmpty(ap.ShowResp.Base.Transaction))
                {
                    this.Resp = new ShowMessageFromWX.Resp();
                    this.Resp.FromProto(ap.ShowResp);
                }
                if (!string.IsNullOrEmpty(ap.PayReq.Base.Transaction))
                {
                    this.Req = new SendPay.Req();
                    this.Req.FromProto(ap.PayReq);
                }
                if (!string.IsNullOrEmpty(ap.PayResp.Base.Transaction))
                {
                    this.Resp = new SendPay.Resp();
                    this.Resp.FromProto(ap.PayResp);
                }
            }
        }
    }

    public async static Task<TransactData> ReadFromFile(string fileName)
    {
        byte[] sourceArray =await FileUtil.readFromFile(fileName, 0, 0x40);
        byte[] destinationArray = new byte[4];
        Array.Copy(sourceArray, 2, destinationArray, 0, 4);
        int count = BitConverter.ToInt32(destinationArray, 0);
        TransactDataP protoObj = TransactDataP.ParseFrom((await FileUtil.readFromFile(fileName, 0x40, count)));
        TransactData data = new TransactData();
        data.FromProto(protoObj);
        return data;
    }

    internal TransactDataP ToProto()
    {
        TransactDataP.Builder builder = TransactDataP.CreateBuilder();
        builder.ConmandID = (uint) this.ConmandID;
        builder.AppID = this.AppID;
        builder.SdkVersion = this.SdkVersion;
        builder.CheckContent = this.CheckContent;
        builder.CheckSummary = this.CheckSummary;
        if (this.Req != null)
        {
            switch (this.Req.Type())
            {
                case 1:
                    builder.AuthReq = this.Req.ToProto() as SendAuthReq;
                    break;

                case 2:
                    builder.SendReq = this.Req.ToProto() as SendMessageToWXReq;
                    break;

                case 3:
                    builder.GetReq = this.Req.ToProto() as GetMessageFromWXReq;
                    break;

                case 4:
                    builder.ShowReq = this.Req.ToProto() as ShowMessageFromWXReq;
                    break;

                case 5:
                    builder.PayReq = this.Req.ToProto() as SendPayReqP;
                    break;
            }
        }
        if (this.Resp != null)
        {
            switch (this.Resp.Type())
            {
                case 1:
                    builder.AuthResp = this.Resp.ToProto() as SendAuthResp;
                    break;

                case 2:
                    builder.SendResp = this.Resp.ToProto() as SendMessageToWXResp;
                    break;

                case 3:
                    builder.GetResp = this.Resp.ToProto() as GetMessageFromWXResp;
                    break;

                case 4:
                    builder.ShowResp = this.Resp.ToProto() as ShowMessageFromWXResp;
                    break;

                case 5:
                    builder.PayResp = this.Resp.ToProto() as SendPayRespP;
                    break;
            }
        }
        return builder.Build();
    }

    public bool ValidateData(bool checkSummary = true)
    {
        if (string.IsNullOrEmpty(this.AppID))
        {
            throw new WXException(1, "AppID can't be empty.");
        }
        if ((string.IsNullOrEmpty(this.SdkVersion) || string.IsNullOrEmpty(this.CheckContent)) || string.IsNullOrEmpty(this.CheckSummary))
        {
            return false;
        }
        if ((this.Req == null) && (this.Resp == null))
        {
            return false;
        }
        if ((this.Req != null) && !this.Req.ValidateData())
        {
            return false;
        }
        if ((this.Resp != null) && !this.Resp.ValidateData())
        {
            return false;
        }
        if (checkSummary)
        {
            string str = WXApiImplV1.getCheckSummary(this.CheckContent, this.SdkVersion, this.AppID);
            if ((str == null) || !str.Equals(this.CheckSummary, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
        }
        return true;
    }

    public async static void WriteToFile(TransactData data, string fileName,string folderName)
    {
        byte[] destinationArray = new byte[0x40];
        destinationArray[0] = 1;
        destinationArray[1] = 5;
        byte[] buffer2 = data.ToProto().ToByteArray();
        int length = buffer2.Length;
        Array.Copy(BitConverter.GetBytes(length), 0, destinationArray, 2, 4);
        await FileUtil.writeToFile(fileName,folderName, destinationArray, true);
        FileUtil.appendToFile(fileName, folderName,buffer2);
    }
}

 

 

}
