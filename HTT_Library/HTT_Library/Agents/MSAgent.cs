using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using HTT_Library.Models;
using System.Data;
using HTT_Library.Services.MSSQL.Services;
using Newtonsoft.Json;
using System.Data.SqlClient;
namespace HTT_Library.Agents
{
    public class MSAgent :DBAgent
    {

        public DataTable bulk_table { get; set; }           //TABLA PARA CARGA MASIVA
        public string bulk_name { get; set; }               //OBJETO TABLA EJEM: Captaciones.dbo.t_remesa
       
        public List<OuterParameter> outer_parameters { get; set; }  //LISTA DE PARAMETROS DE SALIDA

        public SqlConnection connection { get; set; }
        public SqlTransaction transaction { get; set; }
        public bool handding_transaction { get; set; }

        public MSAgent():base()
        {
            handding_transaction = false;
            outer_parameters = new List<OuterParameter>();
        }

        public MSAgent(string Chain, string Query):base(Chain, Query)
        {
            handding_transaction = false;
            outer_parameters = new List<OuterParameter>();
        }

        public MSAgent(string Query, SqlConnection Connection, SqlTransaction Transaction):base(Query)
        {
            connection = Connection;
            transaction = Transaction;
            handding_transaction = true;
            outer_parameters = new List<OuterParameter>();
        }


        public void BeginTransaction() 
        {
            connection = new SqlConnection(chain);
            connection.Open();
            transaction = connection.BeginTransaction();
            handding_transaction = true;
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        public void RollBack() 
        {
            transaction.Rollback();
        }

        public void Commit() {
            transaction.Commit();
        }
        /// <summary>
        /// Ejecuta la consulta de tipo MSSQL
        /// </summary>
        /// <returns>reportan "success" ejecucion completada, en otro caso devuelve el error</returns>
        public string Execute() 
        {
            try
            {
                table = AgentServices.ListenAgent(this); 
                json = JsonConvert.SerializeObject(table,Formatting.Indented,json_settings);
                return "success";
            }
            catch (Exception ex)
            {
                if (handding_transaction)
                {
                    transaction.Rollback(); 
                    connection.Close();
                }
                error = ex.Message;
                return ex.Message;
            }
        }

        /// <summary>
        /// Ejecuta la consulta segun el comando enviado y retorna directamente el objeto requerido, se recomienda utilizar 'try' con esta metodo
        /// Ademas los atributos algunos atributos del agente no estan disponibles con este metodo:
        /// .error, .table, .json 
        /// </summary>
        /// <param name="command">comandos:table, json </param>
        /// <returns></returns>
        public T Execute<T>(string command = "") {
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
            
            return (T) Convert.ChangeType(null, typeof(T));
        }

        /// <summary>
        /// EJECUTA CARGA DE DATOS MASIVA 
        /// </summary>
        /// <returns></returns>
        public string ExecuteBulk() 
        {
            string response = AgentServices.ListenBulkAgent(this);

            if (response !="success")
            {
                error = response;
            }
            return response;            
        }


        /// <summary>
        /// Agrega un parametro a la lista de parametros
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        public void AddParameter(string Name, object Value) {

            base.AddParameter(Name, Value);
        }


        /// <summary>
        /// agrega un nuevo parametro de salidad a la lista de parametros de salida
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Type"></param>
        public void AddOuterParameter(string Name, SqlDbType Type, int Size = 0)
        {
            OuterParameter op = new OuterParameter(Name, Type, Size);
            outer_parameters.Add(op);
        }

        public OuterParameter GetOuterParameter(string Name) {
            foreach (OuterParameter item in outer_parameters)
            {
                if (item.name == Name)
                {
                    return item;
                }
            }
            return new OuterParameter("Not found",SqlDbType.VarChar,0);
        }

        /// <summary>
        /// RESETEA TODOS LOS ATRIBUTOS DEL AGENTE
        /// </summary>
        public void Clear()
        {
            chain = "";
            query = "";
            parameters = new List<Parameter>();
            outer_parameters = new List<OuterParameter>();
            json = "";
            table = new DataTable();
            bulk_table = new DataTable();
            bulk_name = "";
            built_query = "";
            error = "";
        }



       
    }
}
