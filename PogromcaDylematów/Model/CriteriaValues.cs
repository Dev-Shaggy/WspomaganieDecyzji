using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class CriteriaValues
    {
        public double[] Values { get; set; }
        public int Lenght { get; set; }

        public CriteriaValues(int size)
        {
            Lenght = size;
            Values = new double[Lenght];
        }

        public void Count(double[,] tab)
        {
            for (int i = 0; i < Lenght; i++)
            {
                Values[i] = Factor(tab, i) * Lenght;
            }
        }
        private double Factor(double[,] tab, int id)
        {
            return Counter(tab, id) / Denominator(tab);
        }
        private double Counter(double[,] tab, int id)
        {
            double tmp = 1;

            for (int i = 0; i < Lenght; i++)
            {
                tmp *= tab[id, i];
            }

            return Math.Pow(tmp, 1.0 / Lenght);
        }
        private double Denominator(double[,] tab)
        {
            double tmp = 0;

            for (int i = 0; i < Lenght; i++)
            {
                tmp += Counter(tab, i);
            }
            return tmp;
        }
        public double[] GetValues()
        {
            return Values;
        }
        public double GetValue(int id)
        {
            return Values[id];
        }
    }
}

