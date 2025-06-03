using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace ExamenU3
{
    public partial class frmAgregar : Form
    {
        Datos datos = new Datos();
        int id = 0;
        bool bandera = false;
        public frmAgregar()
        {
            InitializeComponent();
        }
        public frmAgregar(int id, string nombre, string precio, string descripcion, int inventario)
        {
            InitializeComponent();
            this.id = id;
            txtId.Text = id.ToString();
            txtNombre.Text = nombre;
            txtPrecio.Text = precio;
            rtbDesc.Text = descripcion;
            txtInventario.Text = inventario.ToString();
            bandera = true;
            this.Text = "Editar Producto";  
            lblTitulo.Text = "Editar Producto"; // Cambia el texto del label
            btnAgregar.Text = "Editar Producto"; // Cambia el texto del botón
        }

        //private void notificarCambio()
        //{
        //    try
        //    {
        //        using (WebSocket ws = new WebSocket("ws://192.168.100.55:8080/notify")) // IP del servidor
        //        {
        //            ws.Connect();
        //            ws.Send("actualizar"); // Mensaje simple que los clientes interpretarán
        //            ws.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al enviar notificación WebSocket: " + ex.Message);
        //    }
        //}


        private void agregarProd()
        {
            string sql = "Insert into Productos (Nombre,Precio,Descripcion,Stock) Values ('" + txtNombre.Text + "'," +
                ""+txtPrecio.Text+",'"+rtbDesc.Text+"',"+txtInventario.Text+")";
            bool v = datos.ejecutarComando(sql);
            if (v)
            {
                MessageBox.Show("Producto Agregado");
                WebSocketClient.EnviarMensaje("REFRESH");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al agregar producto");
            }
        }
        private void editarProd()
        {
            string sql = "Update Productos set Nombre='" + txtNombre.Text + "',Precio=" + txtPrecio.Text+
                ",Descripcion='" + rtbDesc.Text + "',Stock=" + txtInventario.Text + " where IdProducto=" + id;
            bool v = datos.ejecutarComando(sql);
            if (v)
            {
                MessageBox.Show("Producto Editado");
                WebSocketClient.EnviarMensaje("REFRESH");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al editar producto");
            }

        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (bandera)
            {
                editarProd();
            }
            else
            {
                agregarProd();
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
