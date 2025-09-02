using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY
{
    public partial class Form1 : Form
    {
        private List<Harvest> cosechas = new List<Harvest>();
        private int currentId = 1;

        public Form1()
        {
            InitializeComponent();
            // Configuración inicial
            txtId.Text = currentId.ToString();
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
        }

        // EVENTO para btnAgregar (doble click en el botón en el diseñador)
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtPrice.Text) ||
                    string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Por favor complete todos los campos", "Advertencia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Crear nueva cosecha
                Harvest nuevaCosecha = new Harvest
                {
                    attIdHarvest = currentId,
                    attStarDate = dtpStartDate.Value,
                    attEndDate = dtpEndDate.Value,
                    price_per_kilo = int.Parse(txtPrice.Text),
                    attLocation = txtLocation.Text,
                    attCollections = new List<Collects>()
                };

                cosechas.Add(nuevaCosecha);
                currentId++;

                // Actualizar interfaz
                txtId.Text = currentId.ToString();
                ActualizarDataGridView();

                MessageBox.Show("Cosecha agregada exitosamente!", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (FormatException)
            {
                MessageBox.Show("Por favor ingrese un precio válido", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // EVENTO para btnLimpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // EVENTO para btnVerColectas
        private void btnVerColectas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de ver colectas en desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // EVENTO para btnCerrar
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // EVENTO para btnEditar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de editar en desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // EVENTO para btnEliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de eliminar en desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // EVENTO para btnExportar
        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de exportar en desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LimpiarCampos()
        {
            dtpStartDate.Value = DateTime.Today;
            dtpEndDate.Value = DateTime.Today;
            txtPrice.Text = "";
            txtLocation.Text = "";
            txtPrice.Focus(); // Poner foco en el primer campo editable
        }

        private void ActualizarDataGridView()
        {
            dgvCosechas.DataSource = null;
            dgvCosechas.DataSource = cosechas;

            // Opcional: Formatear columnas
            if (dgvCosechas.Columns.Count > 0)
            {
                dgvCosechas.Columns["attIdHarvest"].HeaderText = "ID";
                dgvCosechas.Columns["attStarDate"].HeaderText = "Fecha Inicio";
                dgvCosechas.Columns["attEndDate"].HeaderText = "Fecha Fin";
                dgvCosechas.Columns["price_per_kilo"].HeaderText = "Precio/Kg";
                dgvCosechas.Columns["attLocation"].HeaderText = "Ubicación";
            }
        }
    }

    // CLASES (fuera de la clase Form1)
    public class Harvest
    {
        public int attIdHarvest { get; set; }
        public DateTime attStarDate { get; set; }
        public DateTime attEndDate { get; set; }
        public int price_per_kilo { get; set; }
        public string attLocation { get; set; }
        public List<Collects> attCollections { get; set; }

        public Harvest()
        {
            attCollections = new List<Collects>();
        }
    }

    public class Collects
    {
        public int CollectId { get; set; }
        public decimal Weight { get; set; }
        public DateTime CollectDate { get; set; }
        public string CollectorName { get; set; }
    }
}