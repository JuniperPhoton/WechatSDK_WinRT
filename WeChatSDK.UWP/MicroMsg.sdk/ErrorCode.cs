using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public class ErrorCode
    {
        // Fields
        public const int ERR_AUTH_DENIED = 4;
        public const int ERR_COMM = 1;
        public const int ERR_OK = 0;
        public const int ERR_PAY_FAILED = 6;
        public const int ERR_SENT_FAILED = 3;
        public const int ERR_UNSUPPORT = 5;
        public const int ERR_USER_CANCEL = 2;
    }

}
