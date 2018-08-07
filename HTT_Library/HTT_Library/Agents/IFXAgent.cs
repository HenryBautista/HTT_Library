using HTT_Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using HTT_Library.Services.IFX;
using IBM.Data.Informix;
using HTTLibrary.Models;
namespace HTT_Library.Agents
{
    public class IFXAgent : DBAgent
    {
        public List<OuterParameterIfx> outer_parameters { get; set; }  //LISTA DE PARAMETROS DE SALIDA
        public bool handding_transaction { get; set; }
        public IfxConnection connection {get; set;}
        public IfxTransaction transaction {get; set;}


        public IFXAgent():base()
        {
            handding_transaction = false;
            outer_parameters = new List<OuterParameterIfx>();
        }

        public IFXAgent(string Chain, string Query): base(Chain, Query)
        {
            handding_transaction = false;
            outer_parameters = new List<OuterParameterIfx>();
        }

        public void BeginTransaction()
        {
            connection = new IfxConnection(chain);
            connection.Open();
            transaction = connection.BeginTransaction();
            handding_transaction = true;
        }

        /// <summary>
        /// Ejecuta la consulta en la base de datos Informix
        /// </summary>
        /// <returns>"success" = consulta completada, en otro caso se envia en mensaje de error</returns>
        public string Execute()
        {
            try
            {  
                table = AgentServices.ListenAgent(this);
                json = JsonConvert.SerializeObject(table, Formatting.Indented, json_settings);
                return "success";
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return ex.Message;
            }
        }


        public T Execute<T>(string command = "")
        {
            if (typeof(T) == typeof(DataTable))
            {
                table = AgentServices.ListenAgent(this);
                return (T)Convert.ChangeType(table, typeof(T));
            }
            if (typeof(T) == typeof(string))
            {
                json = JsonConvert.SerializeObject(AgentServices.ListenAgent(this));
                return (T)Convert.ChangeType(json, typeof(T));
            }

            return (T)Convert.ChangeType(null, typeof(T));
        }


        /// <summary>
        /// agrega un nuevo parametro de salidad a la lista de parametros de salida
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        public void AddOuterParameter(string Name, IfxType Type, int Size = 0)
        {
            OuterParameterIfx op = new OuterParameterIfx(Name, Type, Size);
            outer_parameters.Add(op);
        }


        /// <summary>
        /// Agrega un parametro a la lista de parametros
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void AddParameter(string Name, object Value)
        {
            base.AddParameter(Name, Value);
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        public void Commit()
        {
            transaction.Commit();
        }

    }
}
