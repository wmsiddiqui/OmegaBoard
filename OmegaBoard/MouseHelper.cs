using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace OmegaBoard
{
    public class MouseHelper
    {
        const int SM_CXDRAG = 68;
        const int SM_CYDRAG = 69;
        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int index);

        public static Point DragThreshold
        {
            get
            {
                return new Point(GetSystemMetrics(SM_CXDRAG), GetSystemMetrics(SM_CYDRAG));
            }
        }

        public static Point MouseStartPosition;

        public static bool DragThresholdMet(Point mouseCurrentPosition)
        {
            if(Math.Abs(mouseCurrentPosition.X - MouseStartPosition.X) > DragThreshold.X ||
               Math.Abs(mouseCurrentPosition.Y - MouseStartPosition.Y) > DragThreshold.Y )
            {
                return true;
            }
            return false;
        }
    }
}
