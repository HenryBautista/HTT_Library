using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Models
{
    public class Parameter
    {
        public string name { get; set; }
        public object value { get; set; }

        public Parameter(string Name, object Value)
        {
            name = Name;
            value = Value;
        }
    }
}
