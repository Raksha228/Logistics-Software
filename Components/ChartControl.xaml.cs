using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для ChartControl.xaml
    /// </summary>
    public partial class ChartControl : UserControl
    {
        public static readonly DependencyProperty ChartDataProperty =
            DependencyProperty.Register("ChartData", typeof(SeriesCollection), typeof(ChartControl));

        public SeriesCollection ChartData
        {
            get => (SeriesCollection)GetValue(ChartDataProperty);
            set => SetValue(ChartDataProperty, value);
        }



        public ChartControl()
        {
            InitializeComponent();

            ChartData = new SeriesCollection
            {
                new LineSeries { Values = new ChartValues<double> { 3, 5, 7, 4 } }
            };
        }
    }
}
