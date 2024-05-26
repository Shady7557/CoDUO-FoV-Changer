using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CoDUO_FoV_Changer.Util
{
    internal class ScreenUtil
    {
        #region Display Resolution

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }


        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {
            //Create Graphics object from the current windows handle
            var graphicsObject = Graphics.FromHwnd(IntPtr.Zero);

            //Get Handle to the device context associated with this Graphics object

            var deviceContextHandle = graphicsObject.GetHdc();
            //Call GetDeviceCaps with the Handle to retrieve the Screen Height


            var logicalScreenHeight = GetDeviceCaps(deviceContextHandle, (int)DeviceCap.VERTRES);
            var PhysicalScreenHeight = GetDeviceCaps(deviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);

            //Divide the Screen Heights to get the scaling factor and round it to two decimals

            var screenScalingFactor = Math.Round(PhysicalScreenHeight / (double)logicalScreenHeight, 2);

            //If requested as percentage - convert it

            if (percentage)
                screenScalingFactor *= 100.0;
            

            //Release the Handle and Dispose of the GraphicsObject object

            graphicsObject.ReleaseHdc(deviceContextHandle);
            graphicsObject.Dispose();

            //Return the Scaling Factor

            return screenScalingFactor;
        }

        public static Size GetPrimaryDisplayResolution()
        {
            var sf = GetWindowsScreenScalingFactor(false);

            var screenWidth = Screen.PrimaryScreen.Bounds.Width * sf;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height * sf;

            return new Size((int)screenWidth, (int)screenHeight);
        }

        #endregion

    }
}
