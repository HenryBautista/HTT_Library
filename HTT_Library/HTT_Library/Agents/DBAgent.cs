using HTT_Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Agents
{
    public class DBAgent
    {
        public string chain { get; set; }                           //CADENA DE CONEXION
        public string query { get; set; }                           //CONSULTA
        public List<Parameter> parameters { get; set; }             //PARAMETROS

        public string error { get; set; }                           //ERROR DESPLEGADO
        public string built_query { get; set; }                     //CONSULTA CONSTRUIDA
        public JsonSerializerSettings json_settings { get; set; }   //CONFIGURACION PARA EL JSON

        public string json { get; set; }                            //JSON RESULTADO
        public DataTable table { get; set; }                        //TABLA RESULTADO

    
        public DBAgent()
        {
            parameters = new List<Parameter>();
            json_settings = new JsonSerializerSettings();
            json_settings.DateFormatString = "dd/MM/yyyy";
        }

        /// <summary>
        /// Inicializa al agente
        /// </summary>
        /// <param name="Chain">Cadena de coexion</param>
        /// <param name="Query">Texto consulta</param>
        public DBAgent(string Chain, string Query)
        {
            chain = Chain;
            query = Query;
            parameters = new List<Parameter>();
            json_settings = new JsonSerializerSettings();
            json_settings.DateFormatString = "dd/MM/yyyy";
        }

        public DBAgent(string Query)
        {
            query = Query;
            parameters = new List<Parameter>();
            json_settings = new JsonSerializerSettings();
            json_settings.DateFormatString = "dd/MM/yyyy";
        }

        /// <summary>
        /// Agrega un parametro a la lista de parametros
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        protected void AddParameter(string Name, object Value)
        {

            Parameter param = new Parameter(Name, Value);
            parameters.Add(param);
        }

        public string GetBuiltQuery() {
            return built_query;
        }

        public string GetJson() {
            return json;
        }

        public DataTable GetTable() {
            return table;
        }
    }
}
