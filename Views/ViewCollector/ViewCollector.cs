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
        List<Collector> listCollector;
        List<CollectorDTO> listDTOCollector;
        public ViewCollector()
        {
            InitializeComponent();
            loadCustomers();
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {

        }
        public void loadCustomers()
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
            dgCollector.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "status",
                HeaderText = "Estado"
            });
            dgCollector.DataSource = listDTOCollector;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewCollectorRegister viewCollectorDetail = new ViewCollectorRegister();
            viewCollectorDetail.Owner = this;
            viewCollectorDetail.Show();
            this.Hide();
        }

        private void dgCollector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewCollector_Load(object sender, EventArgs e)
        {

        }
    }
}
