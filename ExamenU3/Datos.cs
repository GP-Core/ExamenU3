using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebSocketSharp;
using System.Net.WebSockets;

namespace ExamenU3
{
    internal class Datos
    {
        String cadenaConexion = ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;
        SqlConnection conexion;

        // Cliente WebSocket para notificar cambios
        private WebSocket ws;

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

        private SqlConnection abrirConexion()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir conexion: " + ex.Message);
                return null;
            }
        }

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

        public bool ejecutarComando(string cmdText)
        {
            try
            {
                SqlCommand comando = new SqlCommand(cmdText, abrirConexion());
                comando.ExecuteNonQuery();

                // Notificar al servidor que la BD cambió
                if (ws != null && ws.IsAlive)
                {
                    ws.Send("REFRESH");
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
