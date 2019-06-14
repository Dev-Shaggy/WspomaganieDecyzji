using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class RatingAlternatives
    {
        public double[] Values { get; set; }
        public int cAlternatives;
        private int cCriterias;

        public RatingAlternatives(int alternatives, int criterias)
        {
            cAlternatives = alternatives;
            Values = new double[cAlternatives];
            cCriterias = criterias;
        }
        public void Count(double[] vCriterias, List<CriteriaValues> vAlternatives)
        {
            for (int i = 0; i < cAlternatives; i++)
            {
                Values[i] = 0;
            }


            for (int i = 0; i < cAlternatives; i++)
            {
                for (int j = 0; j < cCriterias; j++)
                {
                    Values[i] += vCriterias[j] * vAlternatives[j].Values[i];
                }
            }
        }
    }
}
