using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class ConstantsAPI
    {
        // Fields
        public const int COMMAND_GETMESSAGE_FROM_WX = 3;
        public const int COMMAND_SENDAUTH = 1;
        public const int COMMAND_SENDMESSAGE_TO_WX = 2;
        public const int COMMAND_SENDPAY = 5;
        public const int COMMAND_SHOWMESSAGE_FROM_WX = 4;
        public const int COMMAND_UNKNOWN = 0;
        internal const byte SDK_MAIN_VERSION = 1;
        internal const byte SDK_SUB_VERSION = 5;
        public const string SDK_TEMP_DIR_PATH = "wechat_sdk";
        public const string SDK_TEMP_FILE_PATH = @"wechat_sdk\wp.wechat";
        public const string SDK_VERSION = "1.5";
        public const string THIRD_APP_ID = "wechatapp";
        public const string WECHAT_APP_ID = "wechat";

        // Methods
        public ConstantsAPI() { }
    }

 

}
