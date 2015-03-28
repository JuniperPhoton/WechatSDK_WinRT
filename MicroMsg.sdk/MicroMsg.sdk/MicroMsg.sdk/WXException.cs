using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
  public class WXException : Exception
{
    // Fields
    public const int ERR_DATA_ILLEGA = 1;
    public const int ERR_NOT_SUPPORTED = 2;
    public const int ERR_OTHER = 0;
    public const int ERR_PACKAGE_ERR = 3;
    private static Dictionary<string, string> errStringMap;

    // Methods
    public WXException(int errCode, string appendString = "") : base(GetErrString(errCode, appendString))
    {
        base.HResult = errCode;
    }

    public static string GetErrString(int code, string appendString = "")
    {
        if (errStringMap == null)
        {
            initErrStringMap();
        }
        if (appendString == null)
        {
            appendString = "";
        }
        if ((code > 3) || (code < 0))
        {
            code = 0;
        }
        if (errStringMap == null)
        {
            return string.Empty;
        }
        string str = errStringMap[code + "_" + getLanguage()];
        if (!string.IsNullOrEmpty(appendString))
        {
            str = str + ": " + appendString;
        }
        return str;
    }

    private static string getLanguage()
    {
        RegionInfo currentRegion = RegionInfo.CurrentRegion;
        if (currentRegion.Name == "CN")
        {
            return "cn";
        }
        if (currentRegion.Name == "TW")
        {
            return "tw";
        }
        return "en";
    }

    private static void initErrStringMap()
    {
        errStringMap = new Dictionary<string, string>();
        errStringMap.Add(0 + "_en", "Unknown error:");
        errStringMap.Add(0 + "_cn", "未知错误");
        errStringMap.Add(0 + "_tw", "未知錯誤");
        errStringMap.Add(1 + "_en", "Invalid data format");
        errStringMap.Add(1 + "_cn", "数据格式不合法");
        errStringMap.Add(1 + "_tw", "資料格式無效");
        errStringMap.Add(2 + "_en", "Request not supported by your current version");
        errStringMap.Add(2 + "_cn", "当前版本不支持该请求");
        errStringMap.Add(2 + "_tw", "當前版本不支持該請求");
        errStringMap.Add(3 + "_en", "打包数据时发生错误");
        errStringMap.Add(3 + "_cn", "打包数据时发生错误");
        errStringMap.Add(3 + "_tw", "打包数据时发生错误");
    }
}

 

 

}
