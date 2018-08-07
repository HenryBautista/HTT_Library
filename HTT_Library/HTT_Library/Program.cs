using HTT_Library.Agents;
using HTT_Library.Models;
using IBM.Data.Informix;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace HTT_Library
{
    class Program
    {
        static void Main(string[] args)
        {
            //string ifx = @"Server=ecodes;Host=192.168.200.143;Service=9090;Protocol=onsoctcp;database=sfi021_ebca;User ID=nlsis010; Password=Sfi-2013;Persist Security Info=True;";

            //IFXAgent agent_ifx = new IFXAgent(ifx, "informix.sp_cmovil_cargos");
            //agent_ifx.BeginTransaction();
            //try
            //{
            //    agent_ifx.AddParameter("i_tipo", 2);
            //    agent_ifx.AddParameter("i_parametro", "2036681");
            //    //agent_ifx.AddOuterParameter("o_npre", IfxType.Integer);
            //    agent_ifx.Execute<DataTable>();
            //    agent_ifx.Commit();
            //}
            //catch (Exception ex)
            //{
            //    agent_ifx.RollBack();
            //}
            //finally 
            //{
            //    agent_ifx.CloseConnection();
            //}

            


              //string ifx = "Server=dbs01;Host=192.168.200.142;Service=1527;Protocol=onsoctcp;database=sfi021;User ID=ecoprod1; Password=ecoprod1db;Persist Security Info=True;";
            string chain = @"Server=171.3.0.38\data2;Initial Catalog=usuarios; Persist Security Info=True;uid=sa;pwd=Ecofuturo12;Connect Timeout=200; pooling='true'; Max Pool Size=200;";
            MSAgent agent_normal = new MSAgent(chain, "usuarios..sp_usuario_web_service");
            agent_normal.AddParameter("i_accion1", "S1");
            agent_normal.Execute();
            Console.WriteLine(agent_normal.json);
            //Console.ReadKey();


           
            ////agent.AddParameter("i_accion", "S1");
            //SqlTransaction trans; 

            //SqlConnection con = new SqlConnection(chain);
            
            //con.Open();
            //trans = con.BeginTransaction();
            //MSAgent agent = new MSAgent("usuarios..sp_usuario_web_service", con, trans);
            //try
            //{
            //    //string table = 
            //    agent.AddParameter("i_accion", "S1");
            //    DataTable tab1 = agent.Execute<DataTable>();
                
            //    agent.parameters.Clear();
                
            //    agent.AddParameter("i_accion", "S2");
            //    DataTable tab2 = agent.Execute<DataTable>();
            //    Console.WriteLine(" ");
            //}
            //catch (Exception ex)
            //{
            //    trans.Rollback();
            //}
            //finally{
            //    con.Close();
            //}

            //MSAgent tagent = new MSAgent(chain, "usuarios..sp_usuario_web_service");
            //tagent.BeginTransaction();
            //try
            //{
            //    tagent.AddParameter("i_accion", "S1");
            //    DataTable tab1 = tagent.Execute<DataTable>();

            //    tagent.parameters.Clear();

            //    tagent.AddParameter("i_accion", "S2");
            //    DataTable tab2 = tagent.Execute<DataTable>();

            //    tagent.Commit();
            //}
            //catch (Exception)
            //{
            //    tagent.RollBack();
            //}
            //finally
            //{
            //    tagent.CloseConnection();
            //}

            //if (agent.Execute() == "success")
            //{
            //    return agent.json;
            //}
            //else
            //    return agent.error;
            
            //try
            //{
            //    grid.DataSource = agent.Execute<DataTable>("json");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            

            //string filename = string.Format(@"{0}\{1}",

            //                    System.IO.Path.GetTempPath(),

            //                    "testhtm.htm");

            //File.WriteAllText(filename, "<h1>Hello HTT</h1>");

            //Process.Start(filename);



            //MSAgent agent = new MSAgent(chain,"select * from usuarios..t_usuario");
            //try
            //{

            //    string json = agent.Execute<string>("json");
            //    Console.ReadKey();
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //    Console.ReadKey();
            //}


            
            //DataTable mytable = new DataTable();

          
            //MSAgent agent = new MSAgent(chain, "usuarios..sp_usuario_web_service");
            //agent.AddParameter("i_accion", "P1");
            //agent.AddParameter("i_nombre", "co");
            //agent.AddOuterParameter("o_salida", SqlDbType.Int);
            //agent.AddOuterParameter("o_salidachar", SqlDbType.VarChar, 50);
            //agent.Execute();

            //Response.Write(agent.json);

            //grid.DataSource = agent.table;



            //int id = int.Parse(agent.GetOuterParameter("o_salida").value);

            //string query = agent.built_query;


            //agent.GetTable();
            //Console.WriteLine(agent.GetJson());    

            //MSAgent agent = new MSAgent(chain, "select * from usuarios..t_usuario");
            //agent.Execute();
            //agent.GetTable();

            //IFXAgent ifxagent = new IFXAgent(ifx, "select * from sisev");
            ////ifxagent.Execute();
            ////Console.Write(ifxagent.json);
            ////Console.ReadKey();

            ////MSAgent agent = new MSAgent();

            ////MSAgent agent = new MSAgent(chain, "select * from usuarios..t_usuario");
            ////agent.Execute();
            ////Console.WriteLine(agent.json);
            ////Console.ReadKey();
            ////MyAgent myagent = new MyAgent("Data Source=localhost;port=3306;Initial Catalog=postulaciones;User Id=root;password=","select * from t_agencia");
            ////myagent.Execute();

            //MSAgent agent = new MSAgent(chain, "usuarios..sp_usuario_web_service");
            //agent.AddParameter("i_accion", "P1");
            //agent.AddParameter("i_nombre", "co");
            //agent.AddParameter("i_numero", 1);
            ////agent.AddParameter("i_fecha", DateTime.Now);
            //agent.AddOuterParameter("o_salida", SqlDbType.Int);
            //agent.AddOuterParameter("o_salidachar", SqlDbType.VarChar, 50);
            //agent.Execute();


            //string chain = @"Server=171.3.0.38\data2;Initial Catalog=usuarios; Persist Security Info=True;uid=sa;pwd=Ecofuturo12;Connect Timeout=200; pooling='true'; Max Pool Size=200;";

            //MSAgent agent = new MSAgent();
            //agent.chain = chain;
            //agent.bulk_table = mytable;
            //agent.bulk_name = "usuarios.dbo.t_usuario";
            //string result = agent.ExecuteBulk();


            //Console.WriteLine(agent.GetOuterParameter("o_salidachar").GetName());
            //Console.WriteLine(agent.GetOuterParameter("o_salidachar").GetValue());
            //Console.WriteLine();

            //Console.WriteLine(agent.json);
            //Console.ReadKey();
                                                                                                 
            //Console.WriteLine(agent.json);
            //Console.ReadKey();
        }
    }
}
