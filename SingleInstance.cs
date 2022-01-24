using System;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace UriScheme.Demo
{
    public sealed class SingleInstance
    {
        const int WM_COPYDATA = 0x004A;
        public static bool AlreadyRunning()
        {
            bool running = false;
            try
            {
                // Getting collection of process  
                Process currentProcess = Process.GetCurrentProcess();

                // Check with other process already running   
                foreach (var p in Process.GetProcesses())
                {
                    if (p.Id != currentProcess.Id) // Check running process   
                    {
                        if (p.ProcessName.Equals(currentProcess.ProcessName) == true)
                        {
                            running = true;
                            IntPtr hFound = p.MainWindowHandle;
                            if (User32API.IsIconic(hFound)) // If application is in ICONIC mode then  
                                User32API.ShowWindow(hFound, User32API.SW_RESTORE);
                            User32API.SetForegroundWindow(
                                hFound); // Activate the window, if process is already running  
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return running;
        }

        public static void SendMessage2RunningApp(string s, IntPtr hWnd)
        {
            byte[] str = Encoding.Default.GetBytes(s);
            int len = str.Length;
            COPYDATASTRUCT cds;
            cds.dwData = (IntPtr)0;
            cds.cbData = len + 1;
            cds.lpData = s;
            User32API.SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds);
        }

        public static IntPtr FindRunningProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (var p in Process.GetProcesses())
            {
                if (p.Id != currentProcess.Id) // Check running process   
                {
                    if (p.ProcessName.Equals(currentProcess.ProcessName) == true)
                    {
                        IntPtr hFound = p.MainWindowHandle;
                        return hFound;
                    }
                }
            }
            return IntPtr.Zero;
        }
    }
}
