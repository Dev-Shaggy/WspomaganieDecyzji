using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    public class Criterias
    {
        public List<string> CriteriasNames { get; set; }

        public Criterias()
        {
            CriteriasNames = new List<string>();
        }
        public Criterias(List<string> list)
        {
            CriteriasNames = list;
        }
        public void RemoveCriteria(int id)
        {
            CriteriasNames.RemoveAt(id);
        }
        public void AddCriteria(string newCriteria)
        {
            CriteriasNames.Add(newCriteria);
        }
        public void Clear()
        {
            CriteriasNames.Clear();
        }
    }
}
