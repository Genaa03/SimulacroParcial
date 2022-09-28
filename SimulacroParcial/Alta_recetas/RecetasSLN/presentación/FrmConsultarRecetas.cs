using RecetasSLN.datos;
using RecetasSLN.dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecetasSLN.presentación
{
    public partial class FrmConsultarRecetas : Form
    {

        private GestorDB gestor;
        private Receta receta;


        public FrmConsultarRecetas()
        {
            InitializeComponent();
            gestor = new GestorDB();
            receta = new Receta();

        }

        private void FrmInsertarReceta_Load(object sender, EventArgs e)
        {
            cargarIngredientes();

        }

        private void cargarIngredientes()
        {
            DataTable tabla = gestor.listarIngredientes();
            cboProducto.DataSource = tabla;
            cboProducto.ValueMember = "id_ingrediente";
            cboProducto.DisplayMember = "n_ingrediente";
            cboProducto.SelectedIndex = -1;
        }
    }
}
