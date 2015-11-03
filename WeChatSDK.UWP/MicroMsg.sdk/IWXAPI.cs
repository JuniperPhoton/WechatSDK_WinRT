using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public interface IWXAPI
    {
        // Methods
        void OpenWXApp();
        Task<bool> SendReqAsync(BaseReq request, string targetAppID = "wechat");
        Task<bool> SendRespAsync(BaseResp response, string targetAppID = "wechat");
    }

}
