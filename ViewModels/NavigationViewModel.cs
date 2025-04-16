using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Logistics_Software.ViewModels;
using System;

namespace Logistics_Software.ViewModels
{
    public partial class NavigationViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentViewModel;

        public NavigationViewModel()
        {
            NavigateToLogin();
        }

        [RelayCommand]
        private void NavigateToLogin()
        {
            CurrentViewModel = new LoginViewModel(this);
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            CurrentViewModel = new RegisterViewModel(this);
        }

        [RelayCommand]
        private void NavigateToClientDashboard()
        {
            CurrentViewModel = new ClientDashboardViewModel(this);
        }

        [RelayCommand]
        private void NavigateToManagerDashboard()
        {
            CurrentViewModel = new ManagerDashboardViewModel(this);
        }

        [RelayCommand]
        private void NavigateToAdminDashboard()
        {
            CurrentViewModel = new AdminDashboardViewModel(this);
        }

        [RelayCommand]
        private void NavigateToOrders()
        {
            CurrentViewModel = new OrdersViewModel();
        }

        [RelayCommand]
        private void NavigateToUsers()
        {
            CurrentViewModel = new UsersViewModel();
        }

        [RelayCommand]
        private void NavigateToMessages()
        {
            // В качестве примера — передаем userId 1. На практике надо передавать текущего авторизованного пользователя.
            CurrentViewModel = new MessagesViewModel(userId: 1);
        }
    }
}
