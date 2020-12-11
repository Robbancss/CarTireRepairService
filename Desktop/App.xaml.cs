using Desktop.Model;
using Desktop.View;
using Desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private  APIService _service;
        private LoginViewModel _loginViewModel;
        private LoginWindow _loginView;

        private MainViewModel _mainViewModel;
        private MainWindow _mainView;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new APIService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);
            _loginViewModel.LoginSucceeded += ViewModel_LoginSucceeded;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _loginView.Show();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _mainViewModel = new MainViewModel(_service);
            
            _mainView = new MainWindow();
            _mainView.DataContext = _mainViewModel;


            _mainView.Show();
            _loginView.Close();
        }
    }
}
