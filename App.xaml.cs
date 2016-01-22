namespace BrowserPicker
{
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var picker = new BrowserLauncher(Environment.GetCommandLineArgs());
                picker.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            Application.Current.Shutdown();
        }
    }
}
