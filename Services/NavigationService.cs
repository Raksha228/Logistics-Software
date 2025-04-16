using Logistics_Software.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;





namespace Logistics_Software.Services
{
    class NavigationService : INavigationService
    {
        private readonly IServiceProvider _provider;
        private readonly Stack<FrameworkElement> _history = new();

        public NavigationService(IServiceProvider provider)
        {
            _provider = provider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : class
        {
            var viewModel = _provider.GetService(typeof(TViewModel)) as BaseViewModel;
            var view = ViewLocator.ResolveView(viewModel);
            view.DataContext = viewModel;

            Application.Current.MainWindow.Content = view;
            _history.Push(view);
        }

        public void GoBack()
        {
            if (_history.Count > 1)
            {
                _history.Pop();
                Application.Current.MainWindow.Content = _history.Peek();
            }
        }
    }
}
