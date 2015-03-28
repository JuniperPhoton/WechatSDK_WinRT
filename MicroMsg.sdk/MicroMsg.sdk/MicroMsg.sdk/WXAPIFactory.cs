using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class WXAPIFactory
{
    // Methods
    public static IWXAPI CreateWXAPI(string appID)
    {
        return new WXApiImplV1(appID);
    }
}
}
