using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.ViewModels
{
    public partial class OrdersViewModel : BaseViewModel
    {
        public ObservableCollection<string> Orders { get; } = new();

        public OrdersViewModel()
        {
            Title = "Заказы";
            LoadOrders();
        }

        private void LoadOrders()
        {
            Orders.Clear();
            Orders.Add("Заказ #1001");
            Orders.Add("Заказ #1002");
            Orders.Add("Заказ #1003");
        }

        [RelayCommand]
        private async Task RefreshOrdersAsync()
        {
            IsBusy = true;
            await Task.Delay(1000);
            LoadOrders();
            IsBusy = false;
        }
    }
}
