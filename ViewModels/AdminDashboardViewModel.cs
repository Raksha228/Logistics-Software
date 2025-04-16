using System.Collections.ObjectModel;
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
    public class AdminDashboardViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private readonly OrderService _orderService;
        private readonly LogService _logService;
        private readonly AnalyticsService _analyticsService;

        public ObservableCollection<User> Users { get; private set; }
        public ObservableCollection<Order> Orders { get; private set; }
        public ObservableCollection<LogEntry> Logs { get; private set; }
        public AnalyticsData Analytics { get; private set; }

        public ICommand RefreshUsersCommand { get; }
        public ICommand RefreshOrdersCommand { get; }
        public ICommand RefreshLogsCommand { get; }
        public ICommand RefreshAnalyticsCommand { get; }

        public ICommand DeleteUserCommand { get; }
        public ICommand UpdateUserCommand { get; }

        public AdminDashboardViewModel()
        {
            _userService = new UserService();
            _orderService = new OrderService();
            _logService = new LogService();
            _analyticsService = new AnalyticsService();

            RefreshUsersCommand = new RelayCommand(LoadUsers);
            RefreshOrdersCommand = new RelayCommand(LoadOrders);
            RefreshLogsCommand = new RelayCommand(LoadLogs);
            RefreshAnalyticsCommand = new RelayCommand(LoadAnalytics);

            DeleteUserCommand = new RelayCommand<User>(DeleteUser);
            UpdateUserCommand = new RelayCommand<User>(UpdateUser);

            LoadUsers();
            LoadOrders();
            LoadLogs();
            LoadAnalytics();
        }

        private async void LoadUsers()
        {
            Users = new ObservableCollection<User>(await _userService.GetAllUsersAsync());
            OnPropertyChanged(nameof(Users));
        }

        private async void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(await _orderService.GetAllOrdersAsync());
            OnPropertyChanged(nameof(Orders));
        }

        private async void LoadLogs()
        {
            Logs = new ObservableCollection<LogEntry>(await _logService.GetAllLogsAsync());
            OnPropertyChanged(nameof(Logs));
        }

        private async void LoadAnalytics()
        {
            Analytics = await _analyticsService.GetAnalyticsAsync();
            OnPropertyChanged(nameof(Analytics));
        }

        private async void DeleteUser(User user)
        {
            if (user != null)
            {
                await _userService.DeleteUserAsync(user.Id);
                LoadUsers();
            }
        }

        private async void UpdateUser(User user)
        {
            if (user != null)
            {
                await _userService.UpdateUserAsync(user);
                LoadUsers();
            }
        }
    }
}
