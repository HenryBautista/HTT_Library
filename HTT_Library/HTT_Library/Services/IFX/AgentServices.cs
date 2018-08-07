using HTT_Library.Agents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using IBM.Data.Informix;
using HTT_Library.Utilities;
using HTT_Library.Models;
using HTTLibrary.Models;
using HTTLibrary.Services.IFX;
namespace HTT_Library.Services.IFX
{
    public static class AgentServices
    {

        internal static DataTable ListenAgent(IFXAgent Agent)
        {
            IfxCommand command = new IfxCommand();
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
                    command.Parameters.Add(new IfxParameter(par.name, par.value));
                }
                //DECLARA LOS PARAMETROS DE SALIDA, SI LOS TUVIESE
                foreach (OuterParameterIfx o_par in Agent.outer_parameters)
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
            foreach (OuterParameterIfx o_par in Agent.outer_parameters)
            {
                o_par.value = command.Parameters[o_par.name].Value;
            }
            return result;

            //DataTable result = new DataTable();
            //Agent.built_query = Agent.query;
            //IfxConnection conexionIFX = new IfxConnection();
            //IBM.Data.Informix.IfxDataAdapter datoIFX = default(IBM.Data.Informix.IfxDataAdapter);
            //conexionIFX = new IfxConnection(Agent.chain);
            //conexionIFX.Open();
            //datoIFX = new IfxDataAdapter(Agent.query, conexionIFX);
            //datoIFX.Fill(result);
            //datoIFX.Dispose();
            //conexionIFX.Close();
            //return result;
        }



    }
}
