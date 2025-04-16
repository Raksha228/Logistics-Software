using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;


namespace Logistics_Software.ViewModels
{
    public partial class UsersViewModel : BaseViewModel
    {
        public ObservableCollection<string> Users { get; } = new();

        public UsersViewModel()
        {
            Title = "Пользователи";
            LoadUsers();
        }

        private void LoadUsers()
        {
            Users.Clear();
            Users.Add("Андрей (Администратор)");
            Users.Add("Марина (Менеджер)");
            Users.Add("Иван (Клиент)");
        }

        [RelayCommand]
        private void AddUser(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                Users.Add(userName);
            }
        }
    }
}
