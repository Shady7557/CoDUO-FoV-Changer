using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HotkeyHandling
{
    class HotkeyHandler
    {
        [DllImport("user32.dll")]
        static extern ushort GetAsyncKeyState(int vKey);

        public static bool IsKeyPushedDown(Keys vKey) { return (GetAsyncKeyState((int)vKey) & 0x8000) != 0; }
    }
}
