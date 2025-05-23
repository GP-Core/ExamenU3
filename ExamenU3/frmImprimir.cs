using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenU3
{
    public partial class frmImprimir : Form
    {
        public frmImprimir()
        {
            InitializeComponent();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void crystalReport1_InitReport(object sender, EventArgs e)
        {
        }

        private void frmImprimir_Activated(object sender, EventArgs e)
        {
           crystalReport1.Refresh();
        }
    }
}
