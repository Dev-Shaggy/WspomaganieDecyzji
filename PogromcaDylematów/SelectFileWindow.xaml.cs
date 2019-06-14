using PogromcaDylematów.Model;
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
    /// Interaction logic for SelectFileWindow.xaml
    /// </summary>
    public partial class SelectFileWindow : Window
    {
        private LoadListFiles LoadFile;

        private int id;

        public SelectFileWindow()
        {
            InitializeComponent();
            FileExplorer.Items.Clear();
            LoadFile = new LoadListFiles();
            LoadFile.LoadData();
            foreach (string item in LoadFile.Data)
            {
                FileExplorer.Items.Add(item);
            }
            FileExplorer.SelectionMode = SelectionMode.Single;
            
        }
        public int getID()
        {
            return id;
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            id = FileExplorer.SelectedIndex;
            if(id != -1)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Aby zaakceptować wybierz plik");
            }
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
