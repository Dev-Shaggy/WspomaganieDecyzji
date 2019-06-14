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
    /// Interaction logic for EditAlternativeWindow.xaml
    /// </summary>
    public partial class EditAlternativeWindow : Window
    {
        public Alternative alternative;
        public List<string> Categorys;
        public List<string> Values;
        public string name;
        public List<ScrollRow> rows;


        public EditAlternativeWindow(Criterias listCriteria)
        {
            InitializeComponent();
            Categorys = listCriteria.CriteriasNames;
            Values = new List<string>();
            name = "";
            InputAlternativeName.Text = name;
            rows = new List<ScrollRow>();

            for (int i = 0; i < Categorys.Count; i++)
            {
                scrollView.RowDefinitions.Add(new RowDefinition());
                rows.Add(new ScrollRow(Categorys[i], ""));
                Grid.SetColumn(rows[i].Name, 0);
                Grid.SetColumn(rows[i].Value, 1);
                Grid.SetRow(rows[i].Name, i);
                Grid.SetRow(rows[i].Value, i);
                scrollView.Children.Add(rows[i].Name);
                scrollView.Children.Add(rows[i].Value);
            }
        }
        public EditAlternativeWindow(Alternative tmp, Criterias listCriteria)
        {
            InitializeComponent();
            Categorys = listCriteria.CriteriasNames;
            Values = new List<string>();
            name = tmp.Name;
            InputAlternativeName.Text = name;
            rows = new List<ScrollRow>();

            for (int i = 0; i < Categorys.Count; i++)
            {
                scrollView.RowDefinitions.Add(new RowDefinition());
                rows.Add(new ScrollRow(Categorys[i], tmp.parameters.parameters[i]));
                Grid.SetColumn(rows[i].Name, 0);
                Grid.SetColumn(rows[i].Value, 1);
                Grid.SetRow(rows[i].Name, i);
                Grid.SetRow(rows[i].Value, i);
                scrollView.Children.Add(rows[i].Name);
                scrollView.Children.Add(rows[i].Value);
            }
        }


        public Alternative getResult()
        {
            return alternative;
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (InputAlternativeName.Text.Length > 0)
                name = InputAlternativeName.Text;
            else
                name = "Brak nazwy";
            for (int i = 0; i < Categorys.Count; i++)
            {
                if(rows[i].Value.Text.Length>0)
                    Values.Add(rows[i].Value.Text);
                else
                    Values.Add("Brak nazwy");
            }
            Parameters p = new Parameters();
            p.AddParameters(Values);
            alternative = new Alternative(name, p);
            this.DialogResult = true;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
