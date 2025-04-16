using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Logistics_Software.Services;
using Logistics_Software.ViewModels;
using Backend.Models;
using Backend.Services;
using Backend.Utils;

namespace Logistics_Software.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private ObservableCollection<Order> _orders;
        private string _selectedStatus;
        private string _searchText;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> Statuses { get; set; } = new()
        {
            "Все", "Создан", "В обработке", "Доставляется", "Выполнен", "Отменён"
        };

        public string SelectedStatus
        {
            get => _selectedStatus;
            set { _selectedStatus = value; OnPropertyChanged(); FilterOrders(); }
        }

        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); FilterOrders(); }
        }

        private ObservableCollection<Order> _allOrders;

        public ICommand RefreshOrdersCommand { get; }
        public ICommand UpdateOrderStatusCommand { get; }

        public OrdersViewModel()
        {
            _orderService = new OrderService();

            RefreshOrdersCommand = new RelayCommand(LoadOrders);
            UpdateOrderStatusCommand = new RelayCommand<Order>(UpdateOrderStatus);
            LoadOrders();
        }

        private async void LoadOrders()
        {
            _allOrders = new ObservableCollection<Order>(await _orderService.GetAllOrdersAsync());
            FilterOrders();
        }

        private void FilterOrders()
        {
            var filtered = _allOrders.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filtered = filtered.Where(o =>
                    o.Id.ToString().Contains(SearchText) ||
                    (!string.IsNullOrWhiteSpace(o.ClientName) && o.ClientName.Contains(SearchText)));
            }

            if (!string.IsNullOrWhiteSpace(SelectedStatus) && SelectedStatus != "Все")
            {
                filtered = filtered.Where(o => o.Status == SelectedStatus);
            }

            Orders = new ObservableCollection<Order>(filtered);
        }

        private async void UpdateOrderStatus(Order order)
        {
            if (order != null)
            {
                await _orderService.UpdateOrderAsync(order);
                LoadOrders();
            }
        }
    }
}
