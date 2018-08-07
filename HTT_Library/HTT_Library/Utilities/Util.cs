using HTT_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Utilities
{
    public static class Util
    {
        internal static string BuildQuery(string sp, List<Parameter> list)
        {
            string query = "";
            query = "exec " + sp;
            foreach (Parameter par in list)
            {
                if (!par.name.Contains("@"))
                    par.name = "@" + par.name;

                query += " " + par.name + " =";

                if (!IsNumericType(par.value))
                {
                    query += "'" + par.value.ToString() + "', ";    
                }
                else
                {
                    query += par.value + ", ";
                }
            }

            return query.Remove(query.Length -2);
        }

        internal static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }

    
}
