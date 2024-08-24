using System.Diagnostics;
using System.Text;

namespace PDFiumSharp
{
    public class PdfForm : IDisposable
    {
        private PdfDocument _doc;
        private PdfPage _page;
        private bool disposedValue;

        public PdfForm(PdfDocument doc, PdfPage page) { 
            _doc = doc;
            _page = page;

            PDFium.FORM_OnAfterLoadPage(_page.Handle, _doc.FormFillEnvironment.Handle);
            PDFium.FORM_DoPageAAction(_page.Handle, _doc.FormFillEnvironment.Handle, 0);
        }

        public int HasFormFieldAtPoint(double x, double y)
        {
            return PDFium.FPDFPage_HasFormFieldAtPoint(_doc.FormFillEnvironment.Handle, _page.Handle, x, y);
        }

        public bool OnFocus(int modifier, double x, double y)
        {
            return PDFium.FORM_OnFocus(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnLButtonDoubleClick(int modifier, float x, float y)
        {
            return PDFium.FORM_OnLButtonDoubleClick(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnLButtonDown(int modifier, float x, float y)
        {
            return PDFium.FORM_OnLButtonDown(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnLButtonUp(int modifier, double x, double y)
        {
            return PDFium.FORM_OnLButtonUp(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnRButtonDown(int modifier, float x, float y)
        {
            return PDFium.FORM_OnLButtonDown(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnRButtonUp(int modifier, double x, double y)
        {
            return PDFium.FORM_OnLButtonUp(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnMouseMove(int modifier, double x, double y)
        {
            return PDFium.FORM_OnMouseMove(_doc.FormFillEnvironment.Handle, _page.Handle, modifier, x, y);
        }

        public bool OnKeyDown(int modifier, int keyCode)
        {
            return PDFium.FORM_OnKeyDown(_doc.FormFillEnvironment.Handle, _page.Handle, keyCode, modifier);
        }

        public bool OnKeyUp(int modifier, int keyCode)
        {
            return PDFium.FORM_OnKeyUp(_doc.FormFillEnvironment.Handle, _page.Handle, keyCode, modifier);
        }

        public bool OnChar(int modifier, int c)
        {
            return PDFium.FORM_OnChar(_doc.FormFillEnvironment.Handle, _page.Handle, c, modifier);
        }

        public bool SelectAllText()
        {
            return PDFium.FORM_SelectAllText(_doc.FormFillEnvironment.Handle, _page.Handle);
        }

        public string GetSelectedText()
        {
            byte[] buffer = new byte[4096];
            ulong buflen = 4096;
            buflen = PDFium.FORM_GetSelectedText(_doc.FormFillEnvironment.Handle, _page.Handle, buffer, buflen);
            return Encoding.Unicode.GetString(buffer).Trim((char)0);
        }

        public void ReplaceSelection(string text)
        {
            byte[] buffer = Encoding.Unicode.GetBytes(text);
            PDFium.FORM_ReplaceSelection(_doc.FormFillEnvironment.Handle, _page.Handle, buffer);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                PDFium.FORM_DoPageAAction(_page.Handle, _doc.FormFillEnvironment.Handle, 1);
                PDFium.FORM_OnBeforeClosePage(_page.Handle, _doc.FormFillEnvironment.Handle);
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PdfForm()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
