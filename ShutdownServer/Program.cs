using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace ShutdownServer
{
    internal class Program
    {
        const int Hide = 0;
        const int Show = 1;

        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {
            var handle = GetConsoleWindow();
            ShowWindow(handle, Hide);
            Console.WriteLine("Shutdown CLient !");

            TcpClient Reciver = new TcpClient();
            Reciver.Connect("127.0.0.1",9999);

            using (StreamReader Reader = new StreamReader(Reciver.GetStream()))
                while (true)
                {
                    String line = Reader.ReadLine();
                
                        if (line.StartsWith("SHUTDOWNALL"))
                        {
                            string strCmdText;
                            strCmdText = "/C shutdown /s /t 0";
                            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                            //Console.WriteLine("Gets Shutdown!");
                        }
                    
                }


        }
    }
}
