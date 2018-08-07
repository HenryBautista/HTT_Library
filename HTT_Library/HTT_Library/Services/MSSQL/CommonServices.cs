using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Services.MSSQL.Services
{
    public static class CommonServices
    {
        //EJECUTA UN QUERY
        public static DataTable ExecuteQuery(SqlCommand command,string chain)
        {
            DataTable result = new DataTable();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = chain;
            con.Open();
            command.Connection = con;
            //command.CommandText = request_procedure;
            //command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(result);
            con.Close();
            return result;
        }

        public static DataTable ExecuteQuery(SqlCommand command, SqlConnection conn, SqlTransaction trans)
        {
            DataTable result = new DataTable();
            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = chain;
            //con.Open();
            command.Connection = conn;
            command.Transaction = trans;
            //command.CommandText = request_procedure;
            //command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(result);
            //con.Close();
            return result;
        }

        public static void BulkInsert(DataTable table, string table_name, string chain) { 
        SqlConnection con = new SqlConnection();
        con.ConnectionString = chain;
        con.Open();
        SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(chain);
        sqlbulkcopy.DestinationTableName = table_name;
        sqlbulkcopy.WriteToServer(table);
        con.Close();
        }
    }
}
