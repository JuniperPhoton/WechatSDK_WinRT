using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg

{

    public abstract class BaseReq
    {
        // Fields
        public string Transaction;

        // Methods
        protected BaseReq()
        {
        }

        internal abstract void FromProto(object protoObj);
        internal abstract object ToProto();
        public abstract int Type();
        internal abstract bool ValidateData();
    }

 

}
