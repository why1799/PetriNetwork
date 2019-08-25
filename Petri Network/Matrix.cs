using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri_Network
{
    public class Matrix
    {
        public List<List<Tuple<string, string>>> Value { get; set; }

        public Matrix()
        {
            Value = new List<List<Tuple<string, string>>>();
        }
    }
}
