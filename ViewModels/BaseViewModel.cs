using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistics_Software.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        // Здесь можно будет добавить общую логику или свойства для всех ViewModel.
        // Например, индикатор загрузки, сообщения об ошибках и т.п.

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _statusMessage;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }
    }
}
