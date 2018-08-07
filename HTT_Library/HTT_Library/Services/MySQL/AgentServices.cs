using HTT_Library.Agents;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace HTT_Library.Services.MySQL
{
    class AgentServices
    {
        internal static DataTable ListenAgent(MyAgent agent)
        {
            DataTable dt = new DataTable();
            agent.built_query = agent.query;
            using (MySqlConnection conexion = new MySqlConnection(agent.chain))
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand();
                comando.Connection = conexion;
                comando.CommandText = agent.query;
                comando.CommandTimeout = 0;
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conexion.Close();
            }
            return dt;
        }
    }
}
