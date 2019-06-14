using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PogromcaDylematów.Model
{
    class CompareCouple
    {
        public int itemOne { get; set; }
        public int itemTwo { get; set; }

        public CompareCouple(int idOne, int idTwo)
        {
            itemOne = idOne;
            itemTwo = idTwo;
        }
    }
}
