using PDFiumSharp;
using PDFiumSharp.Types;
using System.Diagnostics;

namespace PDFiumSharp
{
    public class PdfFormFillEnvironment : IDisposable
    {
        private FPDF_FORMFILLINFO _formFillInfo = new FPDF_FORMFILLINFO();
        private Pointer<FPDF_FORMFILLINFO> _formFillInfoPointer;
        private IPDF_JSPLATFORM _jsPlatform = new IPDF_JSPLATFORM();
        private Pointer<IPDF_JSPLATFORM> _jsPlatformPointer;
        private PdfDocument  _doc;
        private bool disposedValue;
        private Dictionary<int, System.Threading.Timer> _timers = new Dictionary<int, Timer>();

        public delegate void InvalidateDelegate(PdfPage page, double left, double top, double right, double bottom);
        public delegate void OutputSelectedRectDelegate(PdfPage page, double left, double top, double right, double bottom);
        public delegate void SetCursorDelegate(CursorType cursor);
        public delegate void BeepDelegate(BeepType beepType);
        public delegate DialogResult AppAlertDelegate(string msg, string title, ButtonType type, IconType icon);
        public delegate int GetCurrentPageIndexDelegate();

        public event InvalidateDelegate? Invalidate;
        public event OutputSelectedRectDelegate? OutputSelectedRect;
        public event SetCursorDelegate? SetCursor;
        public event GetCurrentPageIndexDelegate? GetCurrentPageIndex;
        public event BeepDelegate? Beep;
        public event AppAlertDelegate? AppAlert;

        public FPDF_FORMHANDLE Handle { get; private set; }

        public PdfFormFillEnvironment(PdfDocument doc) {
            _doc = doc;

            _formFillInfo.FFI_DoGoToAction = FFI_DoGoToActionCallback;
            _formFillInfo.FFI_DoURIAction = FFI_DoURIActionCallback;
            _formFillInfo.FFI_ExecuteNamedAction = FFI_ExecuteNamedActionCallback;
            _formFillInfo.FFI_GetCurrentPage = FFI_GetCurrentPageCallback;
            _formFillInfo.FFI_GetLocalTime = FFI_GetLocalTimeCallback;
            _formFillInfo.FFI_GetPage = FFI_GetPageCallback;
            _formFillInfo.FFI_GetRotation = FFI_GetRotationCallback;
            _formFillInfo.FFI_Invalidate = FFI_InvalidateCallback;
            _formFillInfo.FFI_KillTimer = FFI_KillTimerCallback;
            _formFillInfo.FFI_OnChange = FFI_OnChangeCallback;
            _formFillInfo.FFI_OutputSelectedRect = FFI_OutputSelectedRectCallback;
            _formFillInfo.FFI_SetCursor = FFI_SetCursorCallback;
            _formFillInfo.FFI_SetTextFieldFocus = FFI_SetTextFieldFocusCallback;
            _formFillInfo.FFI_SetTimer = FFI_SetTimerCallback;
            _formFillInfo.Release = ReleaseCallback;

            _jsPlatform.app_alert = app_alert_callback;
            _jsPlatform.app_beep = app_beep_callback;
            _jsPlatform.app_response = app_response_callback;
            _jsPlatform.Doc_getFilePath = Doc_getFilePath_callback;
            _jsPlatform.Doc_gotoPage = Doc_gotoPage_callback;
            _jsPlatform.Doc_mail = Doc_mail_callback;
            _jsPlatform.Doc_print = Doc_print_callback;
            _jsPlatform.Doc_submitForm = Doc_submitForm_callback;
            _jsPlatform.Field_browse = Field_browse_callback;

            _formFillInfoPointer = new Pointer<FPDF_FORMFILLINFO>(_formFillInfo);
            _jsPlatformPointer = new Pointer<IPDF_JSPLATFORM>(_jsPlatform);

            this.Handle = PDFium.FPDFDOC_InitFormFillEnvironment(doc.Handle , _formFillInfoPointer.Address, _jsPlatformPointer.Address);

            PDFium.FORM_DoDocumentJSAction(this.Handle);
            PDFium.FORM_DoDocumentOpenAction(this.Handle);
        }

        private DialogResult app_alert_callback(IPDF_JSPLATFORM pThis, string Msg, string Title, ButtonType Type, IconType Icon)
        { 
            if (this.AppAlert != null)
                return this.AppAlert(Msg, Title, Type, Icon);
            else
                return DialogResult.Cancel; 
        }

        private void app_beep_callback(IPDF_JSPLATFORM pThis, BeepType nType)
        { 
            if (this.Beep != null)
                this.Beep(nType);
        }

        private int app_response_callback(IPDF_JSPLATFORM pThis, string Question, string Title, string Default, string cLabel, bool Password, IntPtr buffer, int buflen)
        { return 0; }

        private int Doc_getFilePath_callback(IPDF_JSPLATFORM pThis, IntPtr buffer, int buflen)
        { return 0; }

        private void Doc_gotoPage_callback(IPDF_JSPLATFORM pThis, int nPageNum)
        { }

        private void Doc_mail_callback(IPDF_JSPLATFORM pThis, byte[] mailData, int length, bool bUI, string To, string Subject, string Cc, string Bcc, string Msg)
        { }

        private void Doc_print_callback(IPDF_JSPLATFORM pThis, bool bUI, int nStart, int nEnd, bool bSilent, bool bShrinkToFit, bool bPrintAsImage, bool bReverse, bool bAnnotations)
        { }

        private void Doc_submitForm_callback(IPDF_JSPLATFORM pThis, byte[] formData, int length, string Url)
        { }

        private int Field_browse_callback(IPDF_JSPLATFORM pThis, IntPtr filePath, int length)
        { return 0; }
  
        private void FFI_DoGoToActionCallback(FPDF_FORMFILLINFO pThis, int nPageIndex, ZoomType zoomMode, float[] fPosArray, int sizeofArray)
        {
        }

        private void FFI_DoURIActionCallback(FPDF_FORMFILLINFO pThis, string bsURI)
        { }

        private void FFI_ExecuteNamedActionCallback(FPDF_FORMFILLINFO pThis, string namedAction)
        { }

        private FPDF_PAGE FFI_GetCurrentPageCallback(FPDF_FORMFILLINFO pThis, FPDF_DOCUMENT document)
        {
            int pageIndex = 0;
            if (this.GetCurrentPageIndex != null && !_doc.IsDisposed)
                pageIndex = this.GetCurrentPageIndex();

            return _doc.Pages[pageIndex].Handle;
        }

        private FPDF_SYSTEMTIME FFI_GetLocalTimeCallback(FPDF_FORMFILLINFO pThis)
        {
            DateTime dt = DateTime.Now;

            FPDF_SYSTEMTIME result = default(FPDF_SYSTEMTIME);
            result.wYear = (ushort)dt.Year;
            result.wDay = (ushort)dt.Day;
            result.wDayOfWeek = (ushort)dt.DayOfWeek;
            result.wHour = (ushort)dt.Hour;
            result.wMinute = (ushort)dt.Minute;
            result.wMonth = (ushort)dt.Month;
            result.wSecond = (ushort)dt.Second;
            result.wMilliseconds = (ushort)((dt.Millisecond > 999) ? 999u : ((uint)dt.Millisecond));
            return result;
        }

        private FPDF_PAGE  FFI_GetPageCallback(FPDF_FORMFILLINFO pThis, FPDF_DOCUMENT document, int nPageIndex)
        {
            return _doc.Pages[nPageIndex].Handle;
        }

        private PageOrientations FFI_GetRotationCallback(FPDF_FORMFILLINFO pThis, IntPtr page)
        {
            return _doc.Pages.First(p => p.Handle.Equals(page)).Orientation;
        }

        private void FFI_InvalidateCallback(FPDF_FORMFILLINFO pThis, FPDF_PAGE page, double left, double top, double right, double bottom)
        {
            if (this.Invalidate != null && !_doc.IsDisposed)
                this.Invalidate(_doc.Pages.First(p => p.Handle.Equals(page)), left, top, right, bottom);
        }

        private void FFI_KillTimerCallback(FPDF_FORMFILLINFO pThis, int nTimerID)
        {
            _timers[nTimerID].Change(Timeout.Infinite, Timeout.Infinite);
            _timers[nTimerID].Dispose();
            _timers.Remove(nTimerID);
        }

        private void FFI_OnChangeCallback(FPDF_FORMFILLINFO pThis)
        { }

        private void FFI_OutputSelectedRectCallback(FPDF_FORMFILLINFO pThis, FPDF_PAGE page, double left, double top, double right, double bottom)
        {
            if (this.OutputSelectedRect != null && !_doc.IsDisposed)
                this.OutputSelectedRect(_doc.Pages.First(p => p.Handle.Equals(page)), left, top, right, bottom);
        }

        private void FFI_SetCursorCallback(FPDF_FORMFILLINFO pThis, CursorType nCursorType)
        {
            if (this.SetCursor != null)
                this.SetCursor(nCursorType);
        }

        private void FFI_SetTextFieldFocusCallback(FPDF_FORMFILLINFO pThis, string value, int valueLen, bool is_focus, IntPtr field)
        { }

        private int FFI_SetTimerCallback(FPDF_FORMFILLINFO pThis, int uElapse, PDFiumSharp.Types.TimerCallback lpTimerFunc)
        {
            int timerId = 1;
            if (_timers.Count > 0)
                timerId = _timers.Keys.Max() + 1;
            TimerState state = new TimerState() { TimerCallback = lpTimerFunc, TimerId = timerId };
            Timer timer = new Timer(state => { 
                TimerState? ts = (TimerState?)state;
                if (ts != null && ts.TimerCallback != null)
                {
                    ts.TimerCallback.Invoke(ts.TimerId);
                    Debug.WriteLine("timer " + ts?.TimerId.ToString());
                }
            }, state, uElapse, uElapse);
            _timers.Add(timerId, timer);
            return timerId;
        }

        private class TimerState
        {
            public int TimerId { get; set; }
            public PDFiumSharp.Types.TimerCallback? TimerCallback { get; set; }
        }

        private void ReleaseCallback(FPDF_FORMFILLINFO pThis)
        {

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
                PDFium.FORM_DoDocumentAAction(this.Handle, 0x10); // WC
                PDFium.FPDFDOC_ExitFormFillEnvironment(this.Handle);
                _formFillInfoPointer.Dispose();
                _jsPlatformPointer.Dispose();

                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~PdfFormFillEnvironment()
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
