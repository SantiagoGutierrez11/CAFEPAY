using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Harvests.Infrastructure;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
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
    public partial class ViewHarvestFinishConfirm : Form
    {
        private HarvestDTO harvestDTO;
        private PlotDTO plotOfHarvest;
        public ViewHarvestFinishConfirm(PlotDTO _plotDTO, HarvestDTO _harvestDTO)
        {
            plotOfHarvest = _plotDTO;
            harvestDTO = _harvestDTO;
            InitializeComponent();
            loadCompoents();

        }
        public void loadCompoents()
        {
            textBoxIdPlot.Text = plotOfHarvest.idPlot.ToString();
            textBoxPlotName.Text = plotOfHarvest.name;
            textBoxIdHarvest.Text = harvestDTO.id.ToString();
            textBoxStartDate.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            textBoxEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textBoxPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2");

        }
        private void ViewHarvestFinishConfirm_Load(object sender, EventArgs e)
        {

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            if (this.Owner is ViewHarvest parent)
            {
                parent.loadHarvests();
                this.Owner?.Show();
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.HarvestServices.update.execute(harvestDTO.id, harvestDTO.idPlot, harvestDTO.startDate, DateTime.Today, harvestDTO.pricePerKilo, 2);
                MessageBox.Show($"Se ha finalizado la cosecha correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(this.Owner is ViewHarvest parent)
                {
                    parent.loadHarvests();
                    this.Owner.Show();
                    this.Close();
                }
            }
            catch (HarvestHasPendingCollectsException ex)
            {
                MessageBox.Show(
                    "No se puede finalizar la cosecha porque tiene recolecciones pendientes de pago.\n\n" +
                    "Por favor, complete o elimine todas las recolecciones antes de finalizar.",
                    "No se puede finalizar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (InvalidHarvestDurationException ex)
            {
                MessageBox.Show(
                    "La fecha de finalización no es válida.\n\n" +
                    "Debe ser posterior a la fecha de inicio de la cosecha.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (HarvestNotFoundException ex)
            {
                MessageBox.Show(
                    "No se encontró la cosecha seleccionada.\n\n" +
                    "Es posible que ya haya sido eliminada.",
                    "Cosecha no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HarvestOperationException ex)
            {
                // Remover el código de error ORA-XXXXX del mensaje
                string mensaje = ex.Message;
                if (mensaje.Contains("ORA-"))
                {
                    int index = mensaje.IndexOf("ORA-");
                    int endIndex = mensaje.IndexOf(':', index);
                    if (endIndex > index)
                    {
                        mensaje = mensaje.Substring(endIndex + 1).Trim();
                    }
                }

                MessageBox.Show(
                    $"Error al finalizar la cosecha:\n\n{mensaje}",
                    "Error de operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado al finalizar la cosecha.\n\n" +
                    "Por favor, intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
