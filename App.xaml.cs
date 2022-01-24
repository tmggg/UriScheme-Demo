using System;
using System.Windows;

namespace UriScheme.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] Args { get; private set; }
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Args ??= Array.Empty<string>();
            Args = e.Args;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (SingleInstance.AlreadyRunning())
            {
                var p = SingleInstance.FindRunningProcess();
                if (p != IntPtr.Zero)
                    SingleInstance.SendMessage2RunningApp(Args[0], p);
                Current.Shutdown(); // Just shutdown the current application,if any instance found.
            }
            var w = new MainWindow();
            this.MainWindow = w;
            w.Show();
        }
    }
}
