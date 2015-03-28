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
        Task<string> SendReq(BaseReq request, string targetAppID = "wechat");
        Task<bool> SendResp(BaseResp response, string targetAppID = "wechat");
    }

}
