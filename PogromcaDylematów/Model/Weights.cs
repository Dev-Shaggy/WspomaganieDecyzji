using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class Weights
    {
        public double[,] weights { get; set; }
        public int Lenght;

        public Weights(int lenght)
        {
            Lenght = lenght;
            weights = new double[Lenght, Lenght];

            for (int i = 0; i < lenght; i++)
            {
                weights[i, i] = 1;
            }
        }
        public void InsertToTable(int i, int j, double value)
        {
            if (value < 0)
            {
                weights[j, i] = -value + 1;
                weights[i, j] = 1 / weights[j, i];
            }
            else
            {
                weights[i, j] = value + 1;
                weights[j, i] = 1 / weights[i, j];
            }
        }
    }
}
