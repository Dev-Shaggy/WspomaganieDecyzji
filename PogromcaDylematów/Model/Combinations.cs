using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class Combinations
    {
        public List<CompareCouple> CombinationList { get; set; }

        public Combinations()
        {
            CombinationList = new List<CompareCouple>();
        }
        public void SetCouples(int count)
        {
            CombinationList.Clear();
            int k = 0, l = 1;

            for (int i = 0; i < count * (count - 1) / 2; i++)
            {
               
                if (l == count)
                {
                    k++;
                    l = k + 1;
                }
                CombinationList.Add(new CompareCouple(k, l));
                l++;
                Console.WriteLine();
            }
        }
    }
}
