using Desktop.Model;
using System.Windows.Controls;
using System;
using Desktop.ViewModel;

namespace Desktop.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly APIService _model;
        private Boolean _isLoading;

        public DelegateCommand LoginCommand { get; private set; }

        public String Username { get; set; }

        public Boolean IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler LoginSucceeded;

        public event EventHandler LoginFailed;

        public LoginViewModel(APIService model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _model = model;
            Username = String.Empty;

            LoginCommand = new DelegateCommand(_ => !IsLoading, param => LoginAsync(param as PasswordBox));
        }

        private async void LoginAsync(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                IsLoading = true;
                bool result = await _model.LoginAsync(Username, passwordBox.Password);
                IsLoading = false;

                if (result)
                    OnLoginSuccess();
                else
                    OnLoginFailed();
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void OnLoginSuccess()
        {
            LoginSucceeded?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoginFailed()
        {
            LoginFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}
