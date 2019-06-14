using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class Validate
    {
        private double CI;
        private double Lambda;
        private double CR;
        private double RI;
        private double ErrorLevel;

        public Validate(int n, double[] cValues, double[,] weights, double errorlevel)
        {
            ErrorLevel = errorlevel;
            RI = 0.14 * n + 0.41;
            if (n == 3) RI = 0.52;
            if (n < 3) RI = 0;

            LambdaMax(n, cValues, weights);
            CI = (Lambda - n) / (n - 1);
            CR = Math.Abs(CI / RI);
        }
        private void LambdaMax(int n, double[] cValues, double[,] weights)
        {
            Lambda = 0;
            for (int i = 0; i < n; i++)
            {
                double tmp = 0;
                for (int j = 0; j < n; j++)
                {
                    tmp += weights[j, i];
                }
                Lambda += tmp * cValues[i];
            }
            Lambda /= n;
        }
        public bool Cohesion()
        {
            if (CI >= ErrorLevel || CR >= ErrorLevel) return false;
            else return true;
        }
    }
}
