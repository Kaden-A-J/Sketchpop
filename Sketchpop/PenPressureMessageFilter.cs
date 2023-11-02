using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Sketchpop
{
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public class PenPressureMessageFilter : IMessageFilter
    {

        internal enum POINTER_INPUT_TYPE
        {
            PT_POINTER = 1,
            PT_TOUCH = 2,
            PT_PEN = 3,
            PT_MOUSE = 4,
            PT_TOUCHPAD = 5
        }

        [Flags]
        internal enum PEN_FLAGS
        {
            NONE = 0x00000000,
            BARREL = 0x00000001,
            INVERTED = 0x00000002,
            ERASER = 0x00000004,
        }

        [Flags]
        internal enum PEN_MASK
        {
            NONE = 0x00000000,
            PRESSURE = 0x00000001,
            ROTATION = 0x00000002,
            TILT_X = 0x00000004,
            TILT_Y = 0x00000008,
        }

        enum POINTER_BUTTON_CHANGE_TYPE
        {
            POINTER_CHANGE_NONE,
            POINTER_CHANGE_FIRSTBUTTON_DOWN,
            POINTER_CHANGE_FIRSTBUTTON_UP,
            POINTER_CHANGE_SECONDBUTTON_DOWN,
            POINTER_CHANGE_SECONDBUTTON_UP,
            POINTER_CHANGE_THIRDBUTTON_DOWN,
            POINTER_CHANGE_THIRDBUTTON_UP,
            POINTER_CHANGE_FOURTHBUTTON_DOWN,
            POINTER_CHANGE_FOURTHBUTTON_UP,
            POINTER_CHANGE_FIFTHBUTTON_DOWN,
            POINTER_CHANGE_FIFTHBUTTON_UP
        }

        struct POINTER_INFO
        {
            public POINTER_INPUT_TYPE pointerType;
            public uint pointerId;
            public uint frameId;
            public int pointerFlags;
            public IntPtr sourceDevice;
            public IntPtr hwndTarget;
            public Point ptPixelLocation; // NOTE: must be Int32 Points, so System.Drawing not System.Windows.
            public Point ptHimetricLocation;
            public Point ptPixelLocationRaw;
            public Point ptHimetricLocationRaw;
            public uint dwTime; 
            public uint historyCount;
            public int InputData; // 0
            public uint dwKeyStates; // 0
            public ulong PerformanceCount; // REALLY HIGH
            public POINTER_BUTTON_CHANGE_TYPE ButtonChangeType; // 0
        }

        [StructLayout(LayoutKind.Sequential)]
        struct POINTER_PEN_INFO
        {
            [MarshalAs(UnmanagedType.Struct)]
            public POINTER_INFO pointerInfo;
            public PEN_FLAGS penFlags;
            public PEN_MASK penMask;
            public uint pressure;
            public uint rotation;
            public int tiltX;
            public int tiltY;
        }

        //pinvoking a c++ function
        [DllImport("User32.dll", SetLastError = true)]
        private static extern int GetPointerInfo(uint pointerId, out POINTER_INFO pointerInfo);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern int GetPointerPenInfo(uint pointerId, out POINTER_PEN_INFO penInfo);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int EnableMouseInPointer([MarshalAs(UnmanagedType.Bool), In] bool enable);

        // use for debugging. Call when GetPointerInfo or GetPointerPenInfo return 0 to throw and get the error code
        internal static void CheckLastError()
        {
            int errCode = Marshal.GetLastWin32Error();
            if (errCode != 0)
            {
                throw new Win32Exception(errCode);
            }
        }

        private readonly Canvas_Manager canvas_Manager;
        public PenPressureMessageFilter(Canvas_Manager canvas_manager)
        {
            canvas_Manager = canvas_manager;
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x0245) // WM_POINTERUPDATE constant; triggers when touch/mouse/pen moves, whether pressed down or hovering. 
            {
                uint pointerId = (uint)m.WParam & 0xFFFF;
                POINTER_INFO pInfo;
                int pointerInfoResult = GetPointerInfo(pointerId, out pInfo);
                if (pointerInfoResult == 0)
                {
                    Console.WriteLine("Error getting pointer info");
                    //CheckLastError();
                }
                if (pInfo.pointerType == POINTER_INPUT_TYPE.PT_PEN)
                {
                    POINTER_PEN_INFO penInfo;
                    GetPointerPenInfo(pointerId, out penInfo);
                    canvas_Manager.pressure = penInfo.pressure;
                }
            }
            return false;
        }
    }
}
