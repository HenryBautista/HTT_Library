using HTT_Library.Agents;

//using System.Threading.Tasks;
using HTT_Library.Models;
using HTT_Library.Utilities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HTT_Library.Services.MSSQL.Services
{
    public static class AgentServices
    {
        internal static DataTable ListenAgent(MSAgent Agent)
        {
            SqlCommand command = new SqlCommand();
            if (Agent.parameters.Count == 0)
            {
                command.CommandText = Agent.query;
                Agent.built_query = Agent.query;
            }
            else
            {
                Agent.built_query = Util.BuildQuery(Agent.query, Agent.parameters);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = Agent.query;
                //CARGA LOS PARAMETROS
                foreach (Parameter par in Agent.parameters)
                {
                    command.Parameters.AddWithValue(par.name, par.value);
                }
                //DECLARA LOS PARAMETROS DE SALIDA, SI LOS TUVIESE
                foreach (OuterParameter o_par in Agent.outer_parameters)
                {
                    if (o_par.size == 0)
                        command.Parameters.Add(o_par.name, o_par.type);
                    else
                        command.Parameters.Add(o_par.name, o_par.type, o_par.size);

                    command.Parameters[o_par.name].Direction = ParameterDirection.Output;
                }
            }
            DataTable result;

            if (Agent.handding_transaction)
                result = CommonServices.ExecuteQuery(command, Agent.connection, Agent.transaction);
            else
                result = CommonServices.ExecuteQuery(command, Agent.chain);

            //ASIGNA LOS VALORES A LOS PARAMETROS DE SALIDA
            foreach (OuterParameter o_par in Agent.outer_parameters)
            {
                o_par.value = command.Parameters[o_par.name].Value;
            }
            return result;
        }

        /// <summary>
        /// ATIENDE AL AGENTE PARA UNA CARGA MASIVA DE DATOS
        /// </summary>
        /// <param name="Agent"></param>
        /// <returns></returns>
        internal static string ListenBulkAgent(MSAgent Agent)
        {
            SqlTransaction transac;
            SqlConnection con;
            con = new SqlConnection();
            con.ConnectionString = Agent.chain;
            con.Open();
            transac = con.BeginTransaction();
            try
            {
                SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, transac);
                sqlbulkcopy.DestinationTableName = Agent.bulk_name;
                sqlbulkcopy.WriteToServer(Agent.bulk_table);
                transac.Commit();
                return "success";
            }
            catch (Exception ex)
            {
                transac.Rollback();
                con.Close();
                return ex.Message;
            }
        }
    }
}