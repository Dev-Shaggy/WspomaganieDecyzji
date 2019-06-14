using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    public class Alternative
    {
        public string Name { get; set; }
        public Parameters parameters { get; set; }
        
        public Alternative(string name, Parameters list)
        {
            Name = name;
            parameters = list;
        }
        public Alternative(Alternative alternative)
        {
            Name = alternative.Name;
            parameters = alternative.parameters;
        }
        public void AddParameter(string parameterValue="Brak dancyh")
        {
            parameters.AddOnceParameter(parameterValue);
        }
        public void RemoveParameter(int id)
        {
            parameters.RemoveParameter(id);
        }
    }
}
