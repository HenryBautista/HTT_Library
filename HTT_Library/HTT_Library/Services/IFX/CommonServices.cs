using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace HTTLibrary.Services.IFX
{
    public static class CommonServices
    {

        //EJECUTA UN QUERY
        public static DataTable ExecuteQuery(IfxCommand command, string chain)
        {
            DataTable result = new DataTable();
            IfxConnection conexionIFX = new IfxConnection();
            IBM.Data.Informix.IfxDataAdapter datoIFX = default(IBM.Data.Informix.IfxDataAdapter);
            conexionIFX = new IfxConnection(chain);
            conexionIFX.Open();
            command.Connection = conexionIFX;
            datoIFX = new IfxDataAdapter();
            datoIFX.SelectCommand = command;
            datoIFX.Fill(result);
            datoIFX.Dispose();
            conexionIFX.Close();
            return result;
        }

        public static DataTable ExecuteQuery(IfxCommand command, IfxConnection conn, IfxTransaction trans)
        {
            DataTable result = new DataTable();
            command.Connection = conn;
            command.Transaction = trans;
            IBM.Data.Informix.IfxDataAdapter datoIFX = default(IBM.Data.Informix.IfxDataAdapter);
            datoIFX = new IfxDataAdapter();
            datoIFX.SelectCommand = command;
            datoIFX.Fill(result);
            datoIFX.Dispose();
            return result;
        }

    }
}
