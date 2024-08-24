using PDFiumSharp.Types;

namespace PDFiumSharp
{
    public class PdfTextPage : NativeWrapper<FPDF_TEXTPAGE>
    {
        internal PdfTextPage(PdfPage page, FPDF_TEXTPAGE textPage) : base(textPage)
        {
            if (textPage.IsNull)
                throw new PDFiumException();
        }

        internal static PdfTextPage Load(PdfPage page) => new PdfTextPage(page, PDFium.FPDFText_LoadPage(page.Handle));

        public int CountChars
        {
            get
            {
                return PDFium.FPDFText_CountChars(Handle);
            }
        }

        public int CountRects(int start_index, int count)
        {
            return PDFium.FPDFText_CountRects(Handle, start_index, count);
        }

        public int GetCharIndexAtPos(float x, float y, float xTolerance, float yTolerance)
        {
            return PDFium.FPDFText_GetCharIndexAtPos(Handle, x, y, xTolerance, yTolerance);
        }

        public FS_RECTF GetRect(int index)
        {
            PDFium.FPDFText_GetRect(Handle, index, out double left, out double top, out double right, out double bottom);
            return new FS_RECTF((float)left, (float)top, (float)right, (float)bottom);
        }

        public int GetTextIndexFromCharIndex(int nCharIndex)
        {
            return PDFium.FPDFText_GetTextIndexFromCharIndex(Handle, nCharIndex);
        }

        public string GetText(int start_index, int count)
        {
            return PDFium.FPDFText_GetText(Handle, start_index, count);
        }

        protected override void Dispose(FPDF_TEXTPAGE handle)
        {
            PDFium.FPDFText_ClosePage(handle);
        }
    }
}
