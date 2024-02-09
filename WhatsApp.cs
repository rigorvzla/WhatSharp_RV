using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatSharp_RV
{
    public class WhatsApp
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public static void Message(string Telefono, string Mensaje)
        {
            Process.Start($"https://web.whatsapp.com/send?phone=+{Telefono}&text={Mensaje}");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 16000;
            timer.AutoReset = false;
            timer.Elapsed += (source, elapsedArgs) =>
            {
                CursorPoint();
                SendKeys.SendWait("{ENTER}");
                Task.Delay(1000).Wait();
                SendKeys.SendWait("^(w)");
                timer.Stop();
                timer.Dispose();

                Environment.Exit(0);
            };

            timer.Start();
            Console.ReadKey();
        }

        public static void MessageGroup(string Codigo, string Mensaje)
        {
            Process.Start($"https://web.whatsapp.com/accept?code={Codigo}");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 16000;
            timer.AutoReset = false;
            timer.Elapsed += (source, elapsedArgs) =>
            {
                CursorPoint();
                SendKeys.SendWait(Mensaje);
                Task.Delay(500).Wait();
                SendKeys.SendWait("{ENTER}");
                Task.Delay(1000).Wait();
                SendKeys.SendWait("^(w)");
                timer.Stop();
                timer.Dispose();

                Environment.Exit(0);
            };

            timer.Start();
            Console.ReadKey();
        }

        private static void CursorPoint()
        {
            int centerX = Screen.PrimaryScreen.Bounds.Width / 2;
            int centerY = Screen.PrimaryScreen.Bounds.Height / 2;

            Cursor.Position = new System.Drawing.Point(centerX, centerY);

            MouseEvent(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, centerX, centerY);
        }

        private static void MouseEvent(uint dwFlags, int dx, int dy)
        {
            mouse_event(dwFlags, dx, dy, 0, 0);
        }
    }
}
