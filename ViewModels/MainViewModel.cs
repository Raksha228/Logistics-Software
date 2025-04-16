using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        public ObservableCollection<string> Pages { get; } = new() { "Заказы", "Пользователи", "Настройки" };

        [ObservableProperty]
        private string _selectedPage;

        public MainViewModel()
        {
            Title = "Главная";
            SelectedPage = Pages[0]; // Открываем первую страницу
        }

        [RelayCommand]
        private void ChangePage(string page)
        {
            SelectedPage = page;
        }
    }
}
