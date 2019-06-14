using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PogromcaDylematów
{
    /// <summary>
    /// Interaction logic for ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public ChartWindow(List<KeyValuePair<string, double>> data)
        {
            InitializeComponent();
            ((ColumnSeries)DataChart.Series[0]).ItemsSource = null;
            ((ColumnSeries)DataChart.Series[0]).DataContext = null;
            ((ColumnSeries)DataChart.Series[0]).ItemsSource = data;
        }
    }
}
