using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTLibrary.Models
{
    public class OuterParameterIfx
    {
        public string name { get; set; }
        public IfxType type { get; set; }
        public object value { get; set; }
        public int size { get; set ;}

        public OuterParameterIfx(string Name, IfxType Type, int Size)
	    {
            name = Name;
            type = Type;
            value = new object();
            size = Size;
	    }

        public object GetValue() {
            return value;
        }
        public string GetName() {
            return name;
        }
    }
}
