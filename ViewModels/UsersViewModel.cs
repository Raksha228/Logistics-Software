using CommonWin32.API;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Backend.Models;
using Backend.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace Logistics_Software.ViewModels
{
    public partial class UsersViewModel : ObservableObject
    {
        private readonly UserService _userService;

        public UsersViewModel()
        {
            _userService = new UserService();
            LoadUsers();
        }

        [ObservableProperty]
        private ObservableCollection<User> users;

        [ObservableProperty]
        private User selectedUser;

        [ObservableProperty]
        private string newUsername;

        [ObservableProperty]
        private string newPassword;

        [ObservableProperty]
        private string newRole;

        [RelayCommand]
        private async void LoadUsers()
        {
            var userList = await _userService.GetAllUsersAsync();
            Users = new ObservableCollection<User>(userList);
        }

        [RelayCommand]
        private async void AddUser()
        {
            if (string.IsNullOrWhiteSpace(NewUsername) || string.IsNullOrWhiteSpace(NewPassword) || string.IsNullOrWhiteSpace(NewRole))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = new User
            {
                Username = NewUsername,
                Password = NewPassword, // Можно добавить хэширование
                Role = NewRole
            };

            var result = await _userService.AddUserAsync(user);
            if (result)
            {
                Users.Add(user);
                NewUsername = string.Empty;
                NewPassword = string.Empty;
                NewRole = string.Empty;
            }
            else
            {
                MessageBox.Show("Ошибка при добавлении пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async void UpdateUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = await _userService.UpdateUserAsync(SelectedUser);
            if (!result)
            {
                MessageBox.Show("Ошибка при обновлении пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private async void DeleteUser()
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = await _userService.DeleteUserAsync(SelectedUser.Id);
            if (result)
            {
                Users.Remove(SelectedUser);
                SelectedUser = null;
            }
            else
            {
                MessageBox.Show("Ошибка при удалении пользователя.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
