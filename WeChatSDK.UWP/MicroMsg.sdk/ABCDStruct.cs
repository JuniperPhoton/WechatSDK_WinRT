using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MicroMsg
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ABCDStruct
    {
        public uint A;
        public uint B;
        public uint C;
        public uint D;
    }
}
