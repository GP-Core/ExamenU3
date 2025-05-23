using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenU3
{
    internal class Datos
    {
        String cadenaConexion = "Data Source = GP;" + "integrated security=true; initial catalog=ExamenU3Tienda; encrypt=false";
        SqlConnection conexion;

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
        public bool ejecutarComando(string cmdText)
        {
            try
            {
                SqlCommand comando = new SqlCommand(cmdText, abrirConexion());
                comando.ExecuteNonQuery();
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
