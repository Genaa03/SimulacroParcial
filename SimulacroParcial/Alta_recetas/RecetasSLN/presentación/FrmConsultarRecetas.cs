using RecetasSLN.datos.Implementaciones;
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

        private GestorDAO gestor;
        private Receta receta;

        public FrmConsultarRecetas()
        {
            InitializeComponent();
            gestor = new GestorDAO();
            receta = new Receta();

        }

        private void FrmInsertarReceta_Load(object sender, EventArgs e)
        {
            cargarIngredientes();
            proximaReceta();
            cantidadIngredientes();

        }

        private void cargarIngredientes()
        {
            cboIngredientes.DataSource = gestor.cargarIngredientes();
            cboIngredientes.ValueMember = "id_ingrediente";
            cboIngredientes.DisplayMember = "n_ingrediente";
            cboIngredientes.SelectedIndex = -1;
        }

        private void proximaReceta()
        {
            lblNro.Text = "Receta #: " + gestor.siguienteReceta().ToString();
        }

        private bool validar()
        {
            if(String.IsNullOrEmpty(txtNombre.Text) || String.IsNullOrEmpty(txtCheff.Text) || cboTipo.SelectedIndex == -1 
                || nudCantidad.Value < 1)
            {
                MessageBox.Show("Algun campo se encuentra vacio o la cantidad tiene que ser mayor a 0.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            if (dgvDetalles.Rows.Count < 3)
            {
                MessageBox.Show("Ha olvidado ingredientes?\nLa receta debe tener 3 o mas ingredientes.", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        private void cantidadIngredientes()
        {
            lblTotalIngredientes.Text = "Total de ingredientes: " + dgvDetalles.Rows.Count;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("¿Seguro que desea cancelar la receta?", "ALERTA",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                limpiar();
            }
        }

        private void limpiar()
        {
            txtCheff.Text = "Ingrese el nombre del cheff";
            txtNombre.Text = "Ingrese nombre de su receta";
            cboIngredientes.SelectedIndex = -1;
            cboTipo.SelectedIndex = -1;
            nudCantidad.Value = 1;
            dgvDetalles.Rows.Clear();
            cantidadIngredientes();
            proximaReceta();
            receta.DetalleRecetas.Clear();

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboIngredientes.SelectedIndex != -1)
            {
                if (!existe(cboIngredientes.Text))
                {
                    DetalleReceta det = new DetalleReceta();
                    det.oIngrediente = new Ingrediente((int)cboIngredientes.SelectedValue, cboIngredientes.Text);
                    det.Cantidad = (int)nudCantidad.Value;
                    receta.AgregarDetalle(det);
                    dgvDetalles.Rows.Add(new object[] { det.oIngrediente.IngredienteID, det.oIngrediente.Nombre, det.Cantidad });
                    cantidadIngredientes();
                    nudCantidad.Value = 1;
                }
            }
        }

        private bool existe(string ingrediente)
        {
            bool existe = false;
            foreach(DataGridViewRow fila in dgvDetalles.Rows)
            {
                if(ingrediente == fila.Cells["ingrediente"].Value.ToString())
                {
                    existe = true;
                    break;
                }
            }
            return existe;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                receta.RecetaNro = gestor.siguienteReceta();
                receta.Nombre = txtNombre.Text;
                receta.Cheff = txtCheff.Text;
                receta.TipoReceta = Convert.ToInt32(cboTipo.SelectedIndex + 1);
                if (gestor.insertarMaestroDetalle(receta))
                {
                    MessageBox.Show("Receta insertada con éxito", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                }
                else
                {
                    MessageBox.Show("No se pudo insertar la receta.", "ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3) 
            {
                DetalleReceta d = null;
                foreach(DetalleReceta detalle in receta.DetalleRecetas)
                {
                    if (dgvDetalles.Rows[e.RowIndex].Cells[1].Value.ToString() == detalle.oIngrediente.Nombre)
                    {
                        d = detalle;
                    }
                }
                dgvDetalles.Rows.RemoveAt(e.RowIndex);
                receta.DetalleRecetas.Remove(d);

            }
        }
    }
}
