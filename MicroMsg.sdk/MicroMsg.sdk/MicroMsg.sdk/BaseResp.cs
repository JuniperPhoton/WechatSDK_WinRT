using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    public abstract class BaseResp
    {
        // Fields
        public int ErrCode;
        public string ErrStr = "";
        public string Transaction;

        // Methods
        protected BaseResp()
        {
        }

        internal abstract void FromProto(object protoObj);
        internal abstract object ToProto();
        public abstract int Type();
        internal abstract bool ValidateData();
    }

 

}
