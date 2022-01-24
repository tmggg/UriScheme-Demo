using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace UriScheme.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;

            if (hwndSource != null)

            {

                IntPtr handle = hwndSource.Handle;

                hwndSource.AddHook(new HwndSourceHook(WndProc));

            }
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            int WM_COPYDATA = 0x004A;
            if (msg == WM_COPYDATA)
            {
                COPYDATASTRUCT cds = (COPYDATASTRUCT)Marshal.PtrToStructure(lParam, typeof(COPYDATASTRUCT)); // 接收封装的消息
                var datacontext = DataContext as MainWindowViewMdel;
                if (datacontext != null)
                {
                    datacontext.NewParams(new[] { cds.lpData });
                }
            }
            return hwnd;
        }
    }
}
