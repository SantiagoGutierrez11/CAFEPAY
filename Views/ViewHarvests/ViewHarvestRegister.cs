using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CAFEPAY.ArqHex.Share.AppServices;

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvestRegister : Form
    {
        List<Plot> plots = new List<Plot>();
        List<PlotDTO> plotsDTO = new List<PlotDTO>();
        public ViewHarvestRegister()
        {
            InitializeComponent();
            loadSettings();
            loadComboBoxPlot();
        }
        private void loadSettings()
        {
            var today = DateTime.Today;
            dtTmStartDate.MaxDate = today;

            dtTmStartDate.MinDate = today;
            
            dtTmStartDate.Value = today;
        }
        public void loadComboBoxPlot()
        {
            plots = AppServices.PlotServices.query.execute();
            plotsDTO = PlotMapper.ToDTOList(plots);

            var active = plotsDTO
                .Where(p => p.status == 1 || string.Equals(p.statusText, "ACTIVO", StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.idPlot)
                .Select(p => new KeyValuePair<long, string>(p.idPlot, $"{p.idPlot} - {p.name}"))
                .ToList();

            cmbIdPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIdPlot.DataSource = active;
            cmbIdPlot.DisplayMember = "Value";
            cmbIdPlot.ValueMember = "Key";
        }

 
        private void ViewHarvestRegister_Load(object sender, EventArgs e) { }
        

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var _idPlot = (long)cmbIdPlot.SelectedValue;
            var _pricePerKilo = textBoxPricePerKilo.Text.Trim();
            var _startDate = dtTmStartDate.Value;

            if (cmbIdPlot.SelectedValue == null)
            {
                MessageBox.Show("El campo 'Lote' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbIdPlot.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_pricePerKilo))
            {
                MessageBox.Show("El campo 'Precio por Kilo' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus(); return;
            }

     
            var saveHarvest = new HarvestDTO
            {
                id = null,
                idPlot = _idPlot,
                startDate = _startDate,
                pricePerKilo = decimal.Parse(_pricePerKilo),
                status = 1,
                statusText = "ACTIVO",
                plotName = AppServices.PlotServices.queryById.execute(_idPlot).name.plotNameValue
               
            };

            ViewHarvestRegisterConfirm viewHarvestRegisterConfirm = new ViewHarvestRegisterConfirm( saveHarvest, cmbIdPlot.Text, (ViewHarvest)this.Owner);
            viewHarvestRegisterConfirm.Owner = this;
            viewHarvestRegisterConfirm.Show();
            this.Hide();

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner.Show(); 
            this.Close();
        }

        private void textBoxIdPlot_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbIdPlot_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
