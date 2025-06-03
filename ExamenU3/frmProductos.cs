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
    public partial class frmProductos : Form
    {
        Datos datos = new Datos();
        WebSocket ws;
        public frmProductos()
        {
            InitializeComponent();

            WebSocketClient.Inicializar("ws://192.168.100.55:8080/notify"); // IP del servidor WebSocket

            WebSocketClient.ws.OnMessage += (sender, e) =>
            {
                if (e.Data == "REFRESH")
                {
                    this.Invoke(new Action(() =>
                    {
                        cargarTabla();
                        MessageBox.Show("Los productos han sido actualizados por otro usuario.", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }));
                }
            };
        }
        private void cargarTabla()
        {
            DataSet ds = new DataSet();
            ds = datos.consulta("select * from Productos");
            if (ds != null)
            {
                dgvProductos.DataSource = ds.Tables[0];
                dgvProductos.Columns[0].HeaderText = "ID Producto";
                dgvProductos.Columns[1].HeaderText = "Nombre";
                dgvProductos.Columns[2].HeaderText = "Precio";
                dgvProductos.Columns[3].HeaderText = "Descripción";
                dgvProductos.Columns[4].HeaderText = "Inventario";
            }
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregar agregar = new frmAgregar();
            agregar.Show();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            cargarTabla();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dgvProductos.CurrentRow.Index;
            int id = Int32.Parse(dgvProductos.Rows[i].Cells[0].Value.ToString());
            string nombre = dgvProductos.Rows[i].Cells[1].Value.ToString();
            double precio = Double.Parse(dgvProductos.Rows[i].Cells[2].Value.ToString());
            string descripcion = dgvProductos.Rows[i].Cells[3].Value.ToString();
            int inventario = Int32.Parse(dgvProductos.Rows[i].Cells[4].Value.ToString());
            frmAgregar agregar = new frmAgregar(id, nombre, precio.ToString(), descripcion, inventario);
            agregar.Show();
        }

        private void frmProductos_Activated(object sender, EventArgs e)
        {
            cargarTabla();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            frmImprimir imprimir = new frmImprimir();
            imprimir.Show();
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = dgvProductos.CurrentRow.Index;
            DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar el producto?" + dgvProductos.Rows[i].Cells[1].Value, "Eliminar Producto", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string sql = "Delete from Productos where IdProducto=" + dgvProductos.Rows[i].Cells[0].Value;
                bool v = datos.ejecutarComando(sql);
                if (v)
                {
                    MessageBox.Show("Producto Eliminado");
                    cargarTabla();
                }
                else
                {
                    MessageBox.Show("Error al eliminar producto");
                }
            }
            else if (result == DialogResult.No)
            {
                MessageBox.Show("Eliminación cancelada");
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = datos.consulta("select * from Productos where Nombre like '%" + txtBusqueda.Text + "%'");
            if (ds != null)
            {
                dgvProductos.DataSource = ds.Tables[0];
                dgvProductos.Columns[0].HeaderText = "ID Producto";
                dgvProductos.Columns[1].HeaderText = "Nombre";
                dgvProductos.Columns[2].HeaderText = "Precio";
                dgvProductos.Columns[3].HeaderText = "Descripción";
                dgvProductos.Columns[4].HeaderText = "Inventario";
            }
        }

        private void frmProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            WebSocketClient.Cerrar();
        }
    }
}
