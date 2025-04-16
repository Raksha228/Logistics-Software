using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string _username;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _isLoginFailed;

        public LoginViewModel()
        {
            Title = "Вход в систему";
        }

        [RelayCommand]
        private async Task LoginAsync()
        {
            IsBusy = true;
            await Task.Delay(1000); // Симуляция запроса к серверу

            if (_username == "admin" && _password == "1234")
            {
                IsLoginFailed = false;
            }
            else
            {
                IsLoginFailed = true;
            }

            IsBusy = false;
        }
    }
}
