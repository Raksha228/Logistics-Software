using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Backend.DataAccess;
using Backend.Models;
using Backend.Services;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics_Software.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        public LoginViewModel()
        {
            _authService = new AuthService();
            _navigationService = NavigationService.Instance;
        }

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string loginError;

        [RelayCommand]
        private async Task LoginAsync()
        {
            LoginError = string.Empty;
            IsLoading = true;

            try
            {
                var user = await _authService.LoginAsync(Username, Password);

                if (user != null)
                {
                    // Навигация в зависимости от роли
                    switch (user.Role)
                    {
                        case UserRole.Client:
                            _navigationService.NavigateTo(new ClientDashboardViewModel(user));
                            break;
                        case UserRole.Manager:
                            _navigationService.NavigateTo(new ManagerDashboardViewModel(user));
                            break;
                        case UserRole.Administrator:
                            _navigationService.NavigateTo(new AdminDashboardViewModel(user));
                            break;
                        default:
                            LoginError = "Неизвестная роль пользователя.";
                            break;
                    }
                }
                else
                {
                    LoginError = "Неверный логин или пароль.";
                }
            }
            catch (Exception ex)
            {
                LoginError = $"Ошибка входа: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            _navigationService.NavigateTo(new RegisterViewModel());
        }
    }
}
