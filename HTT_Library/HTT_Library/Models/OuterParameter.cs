using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Models
{
    public class OuterParameter
    {
        public string name { get; set; }
        public SqlDbType type { get; set; }
        public object value { get; set; }
        public int size { get; set ;}

        public OuterParameter(string Name, SqlDbType Type, int Size)
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
