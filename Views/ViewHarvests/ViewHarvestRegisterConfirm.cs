using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvestRegisterConfirm : Form
    {
        private readonly HarvestDTO harvestDTO;
        private readonly string plotInfomation;
        ViewHarvest viewHarvest;
        public ViewHarvestRegisterConfirm(HarvestDTO _harvestDTO, string _plotInfomation, ViewHarvest _viewHarvest)
        {
            harvestDTO = _harvestDTO;
            this.plotInfomation = _plotInfomation;
            InitializeComponent();
            loadComponets();
            viewHarvest = _viewHarvest;
        }
        void loadComponets()
        {
            textBoxIdPlot.Text = harvestDTO.idPlot.ToString();
            textBoxPlotName.Text = harvestDTO.plotName;
            textBoxDate.Text = harvestDTO.startDate.ToString("yyyy-MM-dd");
            textBoxPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2") + " $";
        }
        private void ViewRegisterConfirm_Load(object sender, EventArgs e)
        {
            viewHarvest.loadHarvests();
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                long idHarvest = AppServices.HarvestServices.save.execute(harvestDTO.idPlot, harvestDTO.startDate, harvestDTO.pricePerKilo);
                MessageBox.Show($"Cosecha numero: {idHarvest}  registrada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                viewHarvest.loadHarvests();
                viewHarvest.Show();

                this.Owner.Close();
                this.Close();
            }
            catch (HarvestActiveExistsException ex)
            {
                MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException argumentException)
            {
                MessageBox.Show(argumentException.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                string cleanedMessage = CleanOracleErrorMessage(ex.Message);
                MessageBox.Show($"Error al registrar la cosecha: {cleanedMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);



            }
        }
        // Método para limpiar mensajes de error de Oracle
        private string CleanOracleErrorMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return message;

            // Remover código ORA-XXXXX: del inicio
            string cleaned = Regex.Replace(
                message,
                @"^ORA-\d+:\s*",
                "",
                RegexOptions.IgnoreCase
            );

            // Remover saltos de línea y texto adicional después del primer salto
            int newLineIndex = cleaned.IndexOf('\n');
            if (newLineIndex > 0)
            {
                cleaned = cleaned.Substring(0, newLineIndex);
            }

            return cleaned.Trim();

        }
    }
}
