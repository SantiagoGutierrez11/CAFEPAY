using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorRegister : Form
    {
        public ViewCollectorRegister()
        {
            InitializeComponent();
            LoadComboStatus();
        }
        private class StatusItem
        {
            public int value { get; set; }
            public string text { get; set; }
        }
        private void LoadComboStatus()
        {
            var items = new List<StatusItem>
            {
                new StatusItem { value = 1, text = "Activo"   },
                new StatusItem { value = 2, text = "Inactivo" }
            };

            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.DisplayMember = "text";
            cmbStatus.ValueMember = "value";
            cmbStatus.DataSource = items;
            // Selección por defecto (opcional)
            cmbStatus.SelectedValue = 1; // Activo
        }

        //Componentes de eventos
        #region
        private void ViewCollectorDetail_Load(object sender, EventArgs e)
        {

        }

        private void txtBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void grupBoxCollectorRegister_Enter(object sender, EventArgs e)
        {

        }


        private void lbCollectorId_Click(object sender, EventArgs e)
        {

        }

        private void lbCollecorName_Click(object sender, EventArgs e)
        {

        }

        private void lbCollectorPhone_Click(object sender, EventArgs e)
        {

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            Owner?.Show();
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var _workerCode = txtBoxWorkerCode.Text?.Trim();
            var _id = txtBoxId.Text?.Trim();
            var _firstName = txtBoxFirstName.Text?.Trim();
            var _lastName = txtBoxLastName.Text?.Trim();
            var _phone = txtBoxPhone.Text?.Trim();
            var _status = (int)cmbStatus.SelectedValue; // Estado activo por defecto
            
            // 1) Validaciones mínimas de UI
            if (string.IsNullOrWhiteSpace(_workerCode)) { MessageBox.Show("Worker Code es requerido."); txtBoxWorkerCode.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_id)) { MessageBox.Show("Cédula/ID es requerida."); txtBoxId.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_firstName)) { MessageBox.Show("Nombres es requerido."); txtBoxFirstName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_lastName)) { MessageBox.Show("Apellidos es requerido."); txtBoxLastName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_phone)) { MessageBox.Show("Teléfono es requerido."); txtBoxPhone.Focus(); return; }
      
            try
            {
                // Llamada a tu caso de uso (INSERT)
                AppServices.Collector.save.execute(_workerCode, _id, _firstName, _lastName, _phone, _status);
                var collectorDTO = new CollectorDTO
                {
                    workerCode = _workerCode,
                    id = _id,
                    firstName = _firstName,
                    lastName = _lastName,
                    phone = _phone,
                    status = cmbStatus.Text
                }
                ;
                ViewCollectorRegisterConfirm viewCollectorDetailConfirm = new ViewCollectorRegisterConfirm(collectorDTO);
                viewCollectorDetailConfirm.Owner = this.Owner;
                this.Close();
                viewCollectorDetailConfirm.Show();
            }
            catch (InvalidOperationException ex)
            {
                // Viene del repositorio cuando ORA-00001 (duplicado PK/UNIQUE)
                MessageBox.Show(ex.Message, "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus(); // o txtBoxId.Focus() según el caso
            }
            catch (OracleException ex) when (ex.Number == 1400) // ORA-01400: cannot insert NULL
            {
                MessageBox.Show("Hay campos obligatorios vacíos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex) when (ex.Number == 12899) // ORA-12899: value too large for column
            {
                MessageBox.Show("Algún campo supera el tamaño permitido por la columna.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex)
            {
                // Otros errores de Oracle
                MessageBox.Show($"Error de base de datos ORA-{ex.Number}: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Errores inesperados
                MessageBox.Show("Error al guardar el collector: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBoxLastName_TextChanged_1(object sender, EventArgs e)
        {

        }
        private void txtBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxLastName_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxWorkerCode_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
