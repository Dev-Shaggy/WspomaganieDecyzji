using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PogromcaDylematów.Model
{
    public class QuestionModel
    {
        public Slider slider { get; set; }
        public Label OptionB { get; set; }
        public Label OptionA { get; set; }
        public int compare { get; set; }

        public double getValueCritery()
        {
            return -slider.Value;
        }
        public double getValueAlternative()
        {
            return slider.Value;
        }
        public double getValue()
        { 
            return -slider.Value;
        }
        public QuestionModel(string optionA, string optionB, int cmp)
        {
            compare = cmp;
            OptionA = new Label();
            OptionA.Content = optionA;
            OptionA.Height = 40;


            OptionB = new Label();
            OptionB.Content = optionB;
            OptionB.Height = 40;

            slider = new Slider();
            slider.Height = 40;
            slider.Minimum = -8;
            slider.Maximum = 8;
            slider.IsSnapToTickEnabled = true;
            slider.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            slider.TickFrequency = 1;
            slider.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.Both;
        }
    }
}
