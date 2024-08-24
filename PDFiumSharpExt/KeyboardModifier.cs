using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFiumSharp
{
    public enum KeyboardModifier
    {
        None = 0,
        ShiftKey = 1,
        ControlKey = 2,
        AltKey = 4,
        MetaKey = 8,
        KeyPad = 0x10,
        AutoRepeat = 0x20,
        LeftButtonDown = 0x40,
        MiddleButtonDown = 0x80,
        RightButtonDown = 0x100
    }
}
