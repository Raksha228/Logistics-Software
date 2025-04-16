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
    public class ManagerDashboardViewModel : BaseViewModel
    {
        private readonly OrderService _orderService;
        private readonly ReviewService _reviewService;
        private readonly RouteService _routeService;

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => SetProperty(ref _orders, value);
        }

        private ObservableCollection<Review> _reviews;
        public ObservableCollection<Review> Reviews
        {
            get => _reviews;
            set => SetProperty(ref _reviews, value);
        }

        private ObservableCollection<Route> _routes;
        public ObservableCollection<Route> Routes
        {
            get => _routes;
            set => SetProperty(ref _routes, value);
        }

        public ICommand RefreshOrdersCommand { get; }
        public ICommand RefreshReviewsCommand { get; }
        public ICommand RefreshRoutesCommand { get; }
        public ICommand AddRouteCommand { get; }

        public ManagerDashboardViewModel()
        {
            _orderService = new OrderService();
            _reviewService = new ReviewService();
            _routeService = new RouteService();

            RefreshOrdersCommand = new RelayCommand(LoadOrders);
            RefreshReviewsCommand = new RelayCommand(LoadReviews);
            RefreshRoutesCommand = new RelayCommand(LoadRoutes);
            AddRouteCommand = new RelayCommand(AddRoute);
            LoadOrders();
            LoadReviews();
            LoadRoutes();
        }

        private async void LoadOrders()
        {
            Orders = new ObservableCollection<Order>(await _orderService.GetPendingOrdersAsync());
        }

        private async void LoadReviews()
        {
            Reviews = new ObservableCollection<Review>(await _reviewService.GetAllReviewsAsync());
        }

        private async void LoadRoutes()
        {
            Routes = new ObservableCollection<Route>(await _routeService.GetAllRoutesAsync());
        }

        private async void AddRoute()
        {
            // Пример добавления маршрута — в реальности это должно открывать диалог/форму
            var newRoute = new Route
            {
                From = "Москва",
                To = "Ростов",
                Vehicle = "Машина-123",
                LogisticianPhone = "+7 900 000 00 00",
                LogisticianEmail = "logist@example.com"
            };

            await _routeService.AddRouteAsync(newRoute);
            LoadRoutes();
        }
    }
}
