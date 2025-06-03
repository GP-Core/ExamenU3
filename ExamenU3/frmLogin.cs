using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenU3
{
    public partial class frmLogin : Form
    {
        public static string usuarioLogueado = "";

        public frmLogin()
        {
            InitializeComponent();
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            if (ValidarUsuario(usuario, contraseña))
            {
                usuarioLogueado = usuario;

                this.Hide();
                frmProductos productos = new frmProductos(usuario); // pasa el nombre
                productos.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarUsuario(string usuario, string contraseña)
        {
            string cadena = System.Configuration.ConfigurationManager.ConnectionStrings["MiConexionSQL"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(cadena))
            {
                conexion.Open();
                string sql = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = @user AND Contraseña = @pass";
                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.AddWithValue("@user", usuario);
                cmd.Parameters.AddWithValue("@pass", contraseña);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
