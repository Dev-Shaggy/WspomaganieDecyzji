﻿using System;
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
    /// Interaction logic for EditCriteriaWindow.xaml
    /// </summary>
    public partial class EditCriteriaWindow : Window
    {
        public EditCriteriaWindow(string defaultName="")
        {
            InitializeComponent();
            InputCriteriaName.Text = defaultName;
        }

        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        public string GetCriteriaName()
        {
            if (InputCriteriaName.Text.Length > 0)
                return InputCriteriaName.Text;
            else return "Brak nazwy";
        }
    }
}
