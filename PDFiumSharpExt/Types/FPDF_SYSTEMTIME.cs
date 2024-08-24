using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFiumSharp.Types
{
    public struct FPDF_SYSTEMTIME
    {
        [MarshalAs(UnmanagedType.I2)]
        public ushort wYear;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wMonth;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wDayOfWeek;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wDay;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wHour;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wMinute;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wSecond;

        [MarshalAs(UnmanagedType.I2)]
        public ushort wMilliseconds;
    }
}
