using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
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
    public class ClientDashboardViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private readonly UserService _userService;

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set => SetProperty(ref _currentUser, value);
        }

        public ICommand RefreshOrdersCommand { get; }

        public ClientDashboardViewModel()
        {
            _orderService = new OrderService();
            _userService = new UserService();

            RefreshOrdersCommand = new RelayCommand(LoadOrders);

            LoadUserInfo();
            LoadOrders();
        }

        private async void LoadUserInfo()
        {
            // Предположим, что ID пользователя хранится глобально
            var userId = AppState.CurrentUserId;
            CurrentUser = await _userService.GetUserByIdAsync(userId);
        }

        private async void LoadOrders()
        {
            var userId = AppState.CurrentUserId;
            var orders = await _orderService.GetOrdersByClientIdAsync(userId);
            Orders = new ObservableCollection<Order>(orders);
        }
    }
}
