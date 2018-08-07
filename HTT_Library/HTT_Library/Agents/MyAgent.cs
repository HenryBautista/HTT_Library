using HTT_Library.Models;
using HTT_Library.Services.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Agents
{
    public class MyAgent: DBAgent
    {
       
        public MyAgent() : base()
        {
            
        }

        public MyAgent(string Chain, string Query) : base(Chain, Query)
        {

        }

        /// <summary>
        /// Ejecuta la consulta de tipo Mysql
        /// </summary>
        /// <returns></returns>
        public string Execute()
        {
            try
            {
                base.table = AgentServices.ListenAgent(this);
                base.json = JsonConvert.SerializeObject(table, Formatting.Indented, json_settings);
                return "success";
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return ex.Message;
            }
        }

       
        //public void AddParameter(string Name, object Value)
        //{
        //    base.AddParameter(Name, Value);
        //}

    }
}
