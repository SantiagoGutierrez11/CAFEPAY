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

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollectRegisterConfirm : Form
    {
        private HarvestDTO harvestRegister;
        private CollectorDTO collectorRegister;
        private CollectDTO collectRegister;
        private Form viewCollect;
        public ViewCollectRegisterConfirm(CollectorDTO _collectorRegister, HarvestDTO _harvestRegister, CollectDTO _collectRegister, Form _viewCollect)
        {
            InitializeComponent();
            this.collectorRegister = _collectorRegister;
            this.harvestRegister = _harvestRegister;
            this.collectRegister = _collectRegister;
            loadData();
            this.viewCollect = _viewCollect;
        }

        public void loadData()
        {
            textBoxIdHarvest.Text = harvestRegister.id.ToString();
            textBoxPlotName.Text = harvestRegister.plotName;
            textBoxIdPlot.Text = harvestRegister.idPlot.ToString();
            textBoxWorkerName.Text = collectorRegister.firstName + " " + collectorRegister.lastName;
            textBoxWorkerCode.Text = collectorRegister.workerCode;
            textBoxIdWorker.Text = collectorRegister.id.ToString();
            textBoxDate.Text = collectRegister.collectDate.ToString("yyyy-MM-dd");
            textBoxKilos.Text = collectRegister.collectedKilos.ToString();
            textBoxAmountToPay.Text = (collectRegister.collectedKilos * harvestRegister.pricePerKilo).ToString("C2") + " $";
            textBoxStatus.Text = collectRegister.statusText;
        }

        private void ViewCollectRegisterConfirm_Load(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.CollectServices.save.execute(
                    null,
                    collectorRegister.workerCode,
                    collectRegister.collectDate,
                    collectRegister.collectedKilos,
                    harvestRegister.id,
                    1,
                    null,
                    collectRegister.plotId,
                    1);

                MessageBox.Show(
                    "Recolección registrada exitosamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Owner.Close();

                if (viewCollect is ViewCollect parent)
                {
                    parent.Show();
                    parent.loadLastDataGridView();
                }
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                // Capturar errores de negocio específicos del repositorio
                string errorMessage = ex.Message;

                // Personalizar mensaje según el tipo de error
                if (errorMessage.Contains("ya ha registrado una recolecta") ||
                    errorMessage.Contains("Error 20072"))
                {
                    MessageBox.Show(
                        $"El recolector '{collectorRegister.workerCode}' ya tiene una recolección registrada " +
                        $"para la cosecha #{harvestRegister.id} en el lote #{collectRegister.plotId}.\n\n" +
                        $"Por favor, verifique los datos " + "\n\n" + "Un recolector solo puede registrar una recolecta por día para cada cosecha",
                        "Registro Duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else if (errorMessage.Contains("Ya existe un registro ZERO"))
                {
                    MessageBox.Show(
                        "Ya existe un registro ZERO para esta combinación de recolector y cosecha.",
                        "Registro Duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"Error al registrar la recolección:\n\n{errorMessage}",
                        "Error de Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error no esperado
                MessageBox.Show(
                    $"Error inesperado al registrar la recolección:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
