using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logistics_Software.Components
{
    /// <summary>
    /// Логика взаимодействия для OrderStatusCard.xaml
    /// </summary>
    public partial class OrderStatusCard : UserControl
    {
        public static readonly DependencyProperty OrderTitleProperty =
            DependencyProperty.Register("OrderTitle", typeof(string), typeof(OrderStatusCard));

        public static readonly DependencyProperty OrderStatusProperty =
            DependencyProperty.Register("OrderStatus", typeof(string), typeof(OrderStatusCard));

        public static readonly DependencyProperty StatusColorProperty =
            DependencyProperty.Register("StatusColor", typeof(Brush), typeof(OrderStatusCard));

        public static readonly DependencyProperty ProgressProperty =
            DependencyProperty.Register("Progress", typeof(double), typeof(OrderStatusCard));

        public string OrderTitle
        {
            get => (string)GetValue(OrderTitleProperty);
            set => SetValue(OrderTitleProperty, value);
        }

        public string OrderStatus
        {
            get => (string)GetValue(OrderStatusProperty);
            set => SetValue(OrderStatusProperty, value);
        }

        public Brush StatusColor
        {
            get => (Brush)GetValue(StatusColorProperty);
            set => SetValue(StatusColorProperty, value);
        }

        public double Progress
        {
            get => (double)GetValue(ProgressProperty);
            set => SetValue(ProgressProperty, value);
        }

        public OrderStatusCard()
        {
            InitializeComponent();
        }
    }
}
