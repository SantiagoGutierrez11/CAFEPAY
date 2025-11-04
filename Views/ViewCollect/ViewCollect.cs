using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using CAFEPAY.ArqHex.Share;

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollect : Form
    {
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;

        public ViewCollect()
        {
            InitializeComponent();
        }

        private void ViewCollect_Load(object sender, EventArgs e)
        {
            loadCollectors();
        }

        // CARGAR LOS RECOLECTORES
        private void loadCollectors()
        {
            try
            {
                listCollector = AppServices.CollectorServices.query.execute();
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                dgCollectors.AutoGenerateColumns = false;
                dgCollectors.Columns.Clear();

                AddColumn("workerCode", "ID TRABAJADOR");
                AddColumn("id", "CÉDULA");
                AddColumn("firstName", "NOMBRES");
                AddColumn("lastName", "APELLIDOS");
                AddColumn("phone", "TELÉFONO");

                var statusItems = new[]
                {
                    new { Value = 1, Text = "Activo" },
                    new { Value = 2, Text = "Inactivo" }
                };

                var colStatus = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = "status",
                    HeaderText = "ESTADO",
                    DataSource = statusItems,
                    DisplayMember = "Text",
                    ValueMember = "Value",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing
                };
                dgCollectors.Columns.Add(colStatus);

                dgCollectors.DataSource = listDTOCollector;
                dgCollectors.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los recolectores: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // MÉTODO AUXILIAR PARA AGREGAR COLUMNAS AL DATAGRIDVIEW
        private void AddColumn(string dataProperty, string headerText)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText
            };
            dgCollectors.Columns.Add(column);
        }

        // Botón "Recargar"
        private void button1_Click(object sender, EventArgs e)
        {
            loadCollectors();
        }

        // Botón "Eliminar" (sin lógica aún)
        private void button2_Click(object sender, EventArgs e)
        {
            // Vacío por ahora
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Vacío
        }
    }
}
