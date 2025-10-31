using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvest : Form
    {
        private string _lastSortProp;
        private bool _sortAsc = true;
        List<Harvest> listHarvest = new List<Harvest>();
        List<HarvestDTO> listHarvestDTO = new List<HarvestDTO>();

        public ViewHarvest()
        {
            InitializeComponent();
            loadHarvests();
            dgHarvest.ColumnHeaderMouseClick += dgHarvest_ColumnHeaderMouseClick;

        }

        private void ViewHarvest_Load(object sender, EventArgs e)
        {

        }
        private void dgHarvest_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dgHarvest.Columns[e.ColumnIndex];
            var prop = col.DataPropertyName;                 // ej: "startDate", "endDate", etc.
            if (string.IsNullOrWhiteSpace(prop)) return;

            _sortAsc = (_lastSortProp == prop) ? !_sortAsc : true;
            _lastSortProp = prop;

            Func<HarvestDTO, object> key = x => x?.GetType().GetProperty(prop)?.GetValue(x, null);
            var sorted = _sortAsc
                ? listHarvestDTO.OrderBy(key).ToList()
                : listHarvestDTO.OrderByDescending(key).ToList();

            dgHarvest.DataSource = null;                     // refresco simple
            dgHarvest.DataSource = sorted;
        }
        public void loadHarvests()
        {
            try
            {
                listHarvest = AppServices.HarvestServices.query.execute();
                listHarvestDTO = HarvestMaper.ToDTOList(listHarvest)
                .OrderByDescending(h => h.status == 1 && h.endDate == null).ToList(); // activas primero
   
                dgHarvest.AutoGenerateColumns = false;
                dgHarvest.Columns.Clear();
                AddColumn("idPlot", "Parcela Id", 150);
                AddColumn("id", "Cosecha Id", 150);
                AddColumn("startDate", "Fecha Inicio", 150);
                AddColumn("endDate", "Fecha Fin", 150);
                AddColumn("pricePerKilo", "Precio por Kilo", 150);
                AddColumn("statusText", "Estado", 150);
                dgHarvest.DataSource = listHarvestDTO;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading harvests: " + ex.Message);
                return;
            }
        }
        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgHarvest.Columns.Add(column);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewHarvestRegister viewHarvestRegister = new ViewHarvestRegister();
            viewHarvestRegister.Owner = this;
            viewHarvestRegister.Show();
            this.Hide();
        }
    }
}