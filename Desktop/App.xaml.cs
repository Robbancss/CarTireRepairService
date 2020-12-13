using Desktop.Model;
using Desktop.View;
using Desktop.ViewModel;
using System;
using System.Configuration;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private APIService _service;
        private LoginViewModel _loginViewModel;
        private LoginWindow _loginView;

        private MainViewModel _mainViewModel;
        private MainWindow _mainView;
        private BuildWorkshopWindow _buildWorkshopView;

        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
            Exit += new ExitEventHandler(App_Exit);
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new APIService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);
            _loginViewModel.LoginSucceeded += new EventHandler(ViewModel_LoginSucceeded);
            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _loginView.Show();
        }

        private async void App_Exit(object sender, ExitEventArgs e)
        {
            if (_service.IsUserLoggedIn)
                await _service.LogoutAsync();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.LogoutSucceeded += new EventHandler(ViewModel_LogoutSucceeded);
            _mainViewModel.MessageApplication += new EventHandler<MessageEventArgs>(ViewModel_MessageApplication);
            _mainViewModel.OpenBuildWorkshopWindow += new EventHandler(ViewModel_OpenBuildWorkshopWindow);
            _mainViewModel.SaveBuildBuildWorkshopWindow += new EventHandler(ViewModel_SaveBuildBuildWorkshopWindow);
            _mainViewModel.CancelBuildWorkshopWindow += new EventHandler(ViewModel_CancelBuildWorkshopWindow);

            _mainViewModel.OkCancelDialog = (msg, caption) => MessageBox.Show(msg, caption, MessageBoxButton.OKCancel) == MessageBoxResult.OK;

            _mainView = new MainWindow();
            _mainView.DataContext = _mainViewModel;

            _mainView.Show();
            _loginView.Hide();
        }

        private void ViewModel_CancelBuildWorkshopWindow(object sender, EventArgs e)
        {
            _buildWorkshopView.Close();
        }

        private void ViewModel_SaveBuildBuildWorkshopWindow(object sender, EventArgs e)
        {
            _buildWorkshopView.Close();
        }

        private void ViewModel_OpenBuildWorkshopWindow(object sender, EventArgs e)
        {
            _buildWorkshopView = new BuildWorkshopWindow();
            _buildWorkshopView.DataContext = _mainViewModel;
            _buildWorkshopView.Show();
        }

        private void ViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            if (!_service.IsUserLoggedIn)
            {
                _mainView.Hide();
                _loginView.Show();
            }
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "CTR", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
