using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.ViewModels
{
    public partial class NavigationViewModel : BaseViewModel
    {
        [ObservableProperty]
        private BaseViewModel _currentViewModel;

        public NavigationViewModel()
        {
            CurrentViewModel = new OrdersViewModel();
        }

        [RelayCommand]
        private void NavigateTo(string viewModelName)
        {
            CurrentViewModel = viewModelName switch
            {
                "Orders" => new OrdersViewModel(),
                "Users" => new UsersViewModel(),
                "Login" => new LoginViewModel(),
                _ => throw new ArgumentException("Неизвестная страница")
            };
        }
    }
}
