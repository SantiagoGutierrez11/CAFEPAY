using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollector : Form
    {
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;
        public ViewCollector()
        {
            InitializeComponent();
            loadCollectors();
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {

        }
        public void loadCollectors()
        {
            listCollector = AppServices.Collector.query.execute();
            listDTOCollector = CollectorMaper.ToDTOList(listCollector);
            dgCollector.AutoGenerateColumns = false;
            dgCollector.Columns.Clear();
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "workerCode",
                HeaderText = "Id de Trabajador"
            });
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "id",
                HeaderText = "Cedula"
            });
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "firstName",
                HeaderText = "Nombres"
            });
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "lastName",
                HeaderText = "Apellidos"
            });
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "phone",
                HeaderText = "Telefono"
            });
            // Mapeo status numérico -> texto visible
            var statusItems = new[]
            {
                 new { Value = 1, Text = "Activo" },
                new { Value = 2, Text = "Inactivo" }
             };

            var colStatus = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "status",      // sigue bindeando al int
                HeaderText = "Estado",
                DataSource = statusItems,
                DisplayMember = "Text",
                ValueMember = "Value",
                DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing, // que se vea como texto
                FlatStyle = FlatStyle.Flat
            };
            dgCollector.Columns.Add(colStatus);

            dgCollector.DataSource = listDTOCollector;
            dgCollector.DataSource = listDTOCollector;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewCollectorRegister viewCollectorRegister = new ViewCollectorRegister();
            viewCollectorRegister.Owner = this;
            viewCollectorRegister.Show();
            this.Hide();
        }

        private void dgCollector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewCollector_Load(object sender, EventArgs e)
        {

        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int rowSelected = dgCollector.CurrentCell.RowIndex;
            ViewCollectorModify viewCollectorModify = new ViewCollectorModify(listDTOCollector[rowSelected], this);
            viewCollectorModify.Owner = this;
            viewCollectorModify.Show();
            this.Hide();
        }
    }
}
