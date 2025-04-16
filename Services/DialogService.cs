﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Logistics_Software.Services
{
    class DialogService
    {
        public bool ShowConfirmation(string message, string title = "Подтвердите действие") =>
           MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;

        public void ShowError(string message, string title = "Ошибка") =>
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

        public void ShowInfo(string message, string title = "Информация") =>
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
