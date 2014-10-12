using System;
using System.Windows;

namespace Replicator_NS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            IDisposable disposableViewModel = null;

            Startup += (sender, args) =>
            {
                MainWindow = new MainWindow();
                disposableViewModel = MainWindow.DataContext as IDisposable;

                MainWindow.Show();
            };

            DispatcherUnhandledException += (sender, args) =>
            {
                if (disposableViewModel != null) disposableViewModel.Dispose();
            };

            Exit += (sender, args) =>
            {
                if (disposableViewModel != null) disposableViewModel.Dispose();
            };
        }
    }
}
