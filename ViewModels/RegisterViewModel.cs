using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logistics_Software.ViewModels;
using Backend.Models;
using Backend.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics_Software.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly AuthService _authService;
        private readonly NavigationService _navigationService;

        public RegisterViewModel()
        {
            _authService = new AuthService();
            _navigationService = NavigationService.Instance;
        }

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [MinLength(4, ErrorMessage = "Минимум 4 символа")]
        private string username;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Минимум 6 символов")]
        private string password;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Повторите пароль")]
        private string confirmPassword;

        [ObservableProperty]
        private string registrationError;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private UserRole selectedRole = UserRole.Client;

        public Array Roles => Enum.GetValues(typeof(UserRole));

        [RelayCommand]
        private async Task RegisterAsync()
        {
            RegistrationError = string.Empty;

            if (Password != ConfirmPassword)
            {
                RegistrationError = "Пароли не совпадают.";
                return;
            }

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                RegistrationError = "Заполните все поля.";
                return;
            }

            IsLoading = true;

            try
            {
                var result = await _authService.RegisterAsync(Username, Password, SelectedRole);

                if (result.Success)
                {
                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    _navigationService.NavigateTo(new LoginViewModel());
                }
                else
                {
                    RegistrationError = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                RegistrationError = $"Ошибка: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            _navigationService.NavigateTo(new LoginViewModel());
        }
    }
}
