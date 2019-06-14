using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    public class Parameters
    {
        public List<string> parameters { get; set; }

        public Parameters()
        {
            parameters = new List<string>();
        }
        public Parameters(Parameters source)
        {
            parameters = source.parameters;
        }
        public void AddParameters(List<string> list)
        {
            parameters = list;
        }
        public void AddOnceParameter(string parameter)
        {
            parameters.Add(parameter);
        }
        public void RemoveParameter(int id)
        {
            parameters.RemoveAt(id);
        }
    }
}
