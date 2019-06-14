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
    /// Interaction logic for NewFileWindow.xaml
    /// </summary>
    public partial class NewFileWindow : Window
    {
        private List<string> Names;
        public NewFileWindow(List<string> names,string defaultName="")
        {
            InitializeComponent();
            InputFileName.Text = defaultName;
            Names = names;
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (Names.Contains(InputFileName.Text))
            {
                FileExistWindow isExist = new FileExistWindow();
                if (isExist.ShowDialog() == true)
                {
                    this.DialogResult = true;
                }
            }
            else
            {
                this.DialogResult = true;
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public string GetFileName()
        {

            if (InputFileName.Text.Length > 0)
                return InputFileName.Text;
            else return "Brak nazwy";
        }
    }
}
