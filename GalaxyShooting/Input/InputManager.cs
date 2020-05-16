using System;
using System.Runtime.InteropServices;

namespace GalaxyShooting.Input
{
    public static class InputManager
    {
        [DllImport("user32.dll")]
        private static extern Int16 GetAsyncKeyState(VK vKey);

        public static bool IsPressed(VK key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }
    }
}
