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
using System.Windows.Shapes;

namespace PogromcaDylematów
{
    /// <summary>
    /// Interaction logic for ValidationResult.xaml
    /// </summary>
    public partial class ValidationResult : Window
    {
        public bool confirm()
        {
            return true;
        }

        public ValidationResult()
        {
            InitializeComponent();
        }

        private void YES_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NO_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
