using PDFiumSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFiumSharp.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public class FPDF_FORMFILLINFO 
    {
        public int version = 1;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public ReleaseCallback Release;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_InvalidateCallback FFI_Invalidate;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_OutputSelectedRectCallback FFI_OutputSelectedRect;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_SetCursorCallback FFI_SetCursor;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_SetTimerCallback FFI_SetTimer;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_KillTimerCallback FFI_KillTimer;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_GetLocalTimeCallback FFI_GetLocalTime;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_OnChangeCallback FFI_OnChange;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_GetPageCallback FFI_GetPage;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_GetCurrentPageCallback FFI_GetCurrentPage;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_GetRotationCallback FFI_GetRotation;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_ExecuteNamedActionCallback FFI_ExecuteNamedAction;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_SetTextFieldFocusCallback FFI_SetTextFieldFocus;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_DoURIActionCallback FFI_DoURIAction;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public FFI_DoGoToActionCallback FFI_DoGoToAction;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ReleaseCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_InvalidateCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, FPDF_PAGE page, [MarshalAs(UnmanagedType.R8)] double left, [MarshalAs(UnmanagedType.R8)] double top, [MarshalAs(UnmanagedType.R8)] double right, [MarshalAs(UnmanagedType.R8)] double bottom);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_OutputSelectedRectCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, FPDF_PAGE page, [MarshalAs(UnmanagedType.R8)] double left, [MarshalAs(UnmanagedType.R8)] double top, [MarshalAs(UnmanagedType.R8)] double right, [MarshalAs(UnmanagedType.R8)] double bottom);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_SetCursorCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, CursorType nCursorType);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int FFI_SetTimerCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, int uElapse, TimerCallback lpTimerFunc);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_KillTimerCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, int nTimerID);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Struct)]
    public delegate FPDF_SYSTEMTIME FFI_GetLocalTimeCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_OnChangeCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate FPDF_PAGE FFI_GetPageCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, FPDF_DOCUMENT document, int nPageIndex);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate FPDF_PAGE FFI_GetCurrentPageCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, FPDF_DOCUMENT document);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate PageOrientations FFI_GetRotationCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, IntPtr FPDF_PAGE);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_ExecuteNamedActionCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, [MarshalAs(UnmanagedType.LPStr)] string namedAction);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_SetTextFieldFocusCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, [MarshalAs(UnmanagedType.LPWStr)] string value, [MarshalAs(UnmanagedType.I4)] int valueLen, [MarshalAs(UnmanagedType.Bool)] bool is_focus, IntPtr field);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_DoURIActionCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, [MarshalAs(UnmanagedType.LPStr)] string bsURI);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void FFI_DoGoToActionCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FORMFILLINFO pThis, int nPageIndex, ZoomType zoomMode, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.R4, SizeParamIndex = 4)] float[] fPosArray, int sizeofArray);
  
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void TimerCallback(int idEvent);
}
