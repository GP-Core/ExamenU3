using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WebSocketSharp;

namespace ExamenU3
{
    internal class Datos
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;
        SqlConnection conexion;
        WebSocket ws;

        private SqlConnection abrirConexion()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open(); // abrir conexion a bd
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Eror al abrir conexion: " + ex.Message);
                return null;
            }
        }

        public bool prueba()
        {
            try
            {
                abrirConexion();
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Eror al abrir conexion: " + ex.Message);
                return false;
            }

        }
        //el dataset ayuda a taer informacion de la 
        public DataSet consulta(string consulta)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(consulta, abrirConexion());
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        //public bool ejecutarComando(string cmdText)
        //{
        //    try
        //    {
        //        SqlCommand comando = new SqlCommand(cmdText, abrirConexion());
        //        comando.ExecuteNonQuery();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}
        public Datos()
        {
            try
            {
                ws = new WebSocket("ws://192.168.100.55:8080/notify"); // Cambia por la IP del servidor
                ws.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo conectar al WebSocket para notificaciones: " + ex.Message);
            }
        }

        //public bool ejecutarComando(string cmdText)
        //{
        //    try
        //    {
        //        SqlCommand comando = new SqlCommand(cmdText, abrirConexion());
        //        comando.ExecuteNonQuery();

        //        // Notificar al servidor que la BD cambió
        //        if (ws.IsAlive)
        //        {
        //            ws.Send("REFRESH");
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //}

        public bool ejecutarComando(string cmdText, string accion = "REFRESH", string nombreProducto = "")
        {
            try
            {
                SqlCommand comando = new SqlCommand(cmdText, abrirConexion());
                comando.ExecuteNonQuery();

                if (!string.IsNullOrEmpty(accion) && !string.IsNullOrEmpty(nombreProducto))
                {
                    // Envía la acción + producto + nombre usuario (que debes obtener desde WebSocketClient)
                    WebSocketClient.EnviarMensaje($"{accion}:{nombreProducto}");
                }
                else
                {
                    WebSocketClient.EnviarMensaje("REFRESH");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


    }
}
