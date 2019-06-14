using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PogromcaDylematów.Model
{
    public class ScrollRow
    {
        public Label Name { get; set; }
        public TextBox Value { get; set; }

        public ScrollRow(string name, string value)
        {
            Name = new Label();
            Value = new TextBox();
            Value.Height = 30;
            Name.Height = 30;
            Name.Content = name;
            Value.Text = value;
            Value.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            Name.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
        }
    }
}
