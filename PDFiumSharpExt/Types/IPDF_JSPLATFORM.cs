using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PDFiumSharp.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public class IPDF_JSPLATFORM 
    {
        public int version = 1;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public app_alert_callback app_alert;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public app_beep_callback app_beep;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public app_response_callback app_response;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Doc_getFilePath_callback Doc_getFilePath;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Doc_mail_callback Doc_mail;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Doc_print_callback Doc_print;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Doc_submitForm_callback Doc_submitForm;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Doc_gotoPage_callback Doc_gotoPage;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Field_browse_callback Field_browse;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate DialogResult app_alert_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, [MarshalAs(UnmanagedType.LPWStr)] string Msg, [MarshalAs(UnmanagedType.LPWStr)] string Title, ButtonType Type, IconType Icon);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void app_beep_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, BeepType nType);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int app_response_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, [MarshalAs(UnmanagedType.LPWStr)] string Question, [MarshalAs(UnmanagedType.LPWStr)] string Title, [MarshalAs(UnmanagedType.LPWStr)] string Default, [MarshalAs(UnmanagedType.LPWStr)] string cLabel, [MarshalAs(UnmanagedType.Bool)] bool Password, IntPtr buffer, int buflen);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Doc_getFilePath_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, IntPtr filePath, int length);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Doc_mail_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] mailData, int length, [MarshalAs(UnmanagedType.Bool)] bool bUI, [MarshalAs(UnmanagedType.LPStr)] string To, [MarshalAs(UnmanagedType.LPStr)] string Subject, [MarshalAs(UnmanagedType.LPStr)] string Cc, [MarshalAs(UnmanagedType.LPStr)] string Bcc, [MarshalAs(UnmanagedType.LPStr)] string Msg);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Doc_print_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, [MarshalAs(UnmanagedType.Bool)] bool bUI, int nStart, int nEnd, [MarshalAs(UnmanagedType.Bool)] bool bSilent, [MarshalAs(UnmanagedType.Bool)] bool bShrinkToFit, [MarshalAs(UnmanagedType.Bool)] bool bPrintAsImage, [MarshalAs(UnmanagedType.Bool)] bool bReverse, [MarshalAs(UnmanagedType.Bool)] bool bAnnotations);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Doc_submitForm_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] formData, int length, [MarshalAs(UnmanagedType.LPWStr)] string Url);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void Doc_gotoPage_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, int nPageNum);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int Field_browse_callback([MarshalAs(UnmanagedType.LPStruct)] IPDF_JSPLATFORM pThis, IntPtr filePath, int length);
}
