using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PDFiumSharp
{
    internal class Pointer<T> : IDisposable
    {
        private Type _type;
        private T _obj;
        private bool _isDisposed;

        public nint Address { get; private set; }

        public Pointer(T obj)
        {
            _obj = obj;
            _type = _obj.GetType();
            int cb = Marshal.SizeOf(_obj);
            this.Address = Marshal.AllocHGlobal(cb);
            Marshal.StructureToPtr(_obj, this.Address, fDeleteOld: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                Marshal.DestroyStructure(this.Address, _type);
                Marshal.FreeHGlobal(this.Address);

                _isDisposed = true;
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                }
            }
        }
    }
}
