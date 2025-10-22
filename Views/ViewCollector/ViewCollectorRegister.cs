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

            cmbStatus.Enabled = false;
        }

        //Componentes de eventos
        #region
        private void ViewCollectorModify_Load(object sender, EventArgs e)
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

        private void btnAccept_Click_1(object sender, EventArgs e)
        {
            var _workerCode = txtBoxWorkerCode.Text?.Trim();
            var _id = txtBoxId.Text?.Trim();
            var _firstName = txtBoxFirstName.Text?.Trim();
            var _lastName = txtBoxLastName.Text?.Trim();
            var _phone = txtBoxPhone.Text?.Trim();
            var _status = (int)cmbStatus.SelectedValue;

            // 1) Validaciones mínimas de UI
            if (string.IsNullOrWhiteSpace(_workerCode)) { MessageBox.Show("Worker Code es requerido."); txtBoxWorkerCode.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_id)) { MessageBox.Show("Cédula/ID es requerida."); txtBoxId.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_firstName)) { MessageBox.Show("Nombres es requerido."); txtBoxFirstName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_lastName)) { MessageBox.Show("Apellidos es requerido."); txtBoxLastName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_phone)) { MessageBox.Show("Teléfono es requerido."); txtBoxPhone.Focus(); return; }

            //NUEVAS VALIDACIONES PARA WORKER CODE
            if (_workerCode.Length != 6)
            {
                MessageBox.Show("El Worker Code debe tener exactamente 6 caracteres (ej: W00001)");
                txtBoxWorkerCode.Focus();
                return;
            }

            if (!_workerCode.ToUpper().StartsWith("W"))
            {
                MessageBox.Show("El Worker Code debe empezar con 'W'");
                txtBoxWorkerCode.Focus();
                return;
            }

            //NUEVAS VALIDACIONES PARA ID (CÉDULA)
            if (!long.TryParse(_id, out long idValue))
            {
                MessageBox.Show("La cédula debe contener solo números.");
                txtBoxId.Focus();
                return;
            }

            if (_id.Length < 8 || _id.Length > 10)
            {
                MessageBox.Show("La cédula debe tener entre 8 y 10 dígitos.");
                txtBoxId.Focus();
                return;
            }

            if (_id.StartsWith("0"))
            {
                MessageBox.Show("La cédula no puede empezar con 0.");
                txtBoxId.Focus();
                return;
            }

            //VALIDACIONES PARA TELÉFONO
            if (_phone.Length != 10)
            {
                MessageBox.Show("El teléfono debe tener exactamente 10 dígitos.");
                txtBoxPhone.Focus();
                return;
            }

            if (!_phone.All(char.IsDigit))
            {
                MessageBox.Show("El teléfono solo puede contener números.");
                txtBoxPhone.Focus();
                return;
            }

            //VALIDACIONES PARA NOMBRES/APELLIDOS
            if (_firstName.Length < 3 || _firstName.Length > 30)
            {
                MessageBox.Show("El nombre debe tener entre 3 y 30 caracteres.");
                txtBoxFirstName.Focus();
                return;
            }

            if (_lastName.Length < 3 || _lastName.Length > 30)
            {
                MessageBox.Show("El apellido debe tener entre 3 y 30 caracteres.");
                txtBoxLastName.Focus();
                return;
            }

            try
            {
                // Solo si pasa TODAS las validaciones, proceder
                AppServices.Collector.save.execute(_workerCode, idValue, _firstName, _lastName, _phone, _status);

                var collectorDTO = new CollectorDTO
                {
                    workerCode = _workerCode,
                    id = idValue,
                    firstName = _firstName,
                    lastName = _lastName,
                    phone = _phone,
                    status = _status
                };

                ViewCollectorRegisterConfirm viewCollectorRegisterConfirm = new ViewCollectorRegisterConfirm(collectorDTO);
                viewCollectorRegisterConfirm.Owner = this.Owner;
                this.Close();
                viewCollectorRegisterConfirm.Show();
            }
            catch (InvalidOperationException ex)
            {
                // Viene del repositorio cuando ORA-00001 (duplicado PK/UNIQUE)
                MessageBox.Show(ex.Message, "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus();
            }
            catch (ArgumentException ex)
            {
                // Errores de validación del dominio
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OracleException ex) when (ex.Number == 1400)
            {
                MessageBox.Show("Hay campos obligatorios vacíos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex) when (ex.Number == 12899)
            {
                MessageBox.Show("Algún campo supera el tamaño permitido por la columna.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex)
            {
                MessageBox.Show($"Error de base de datos ORA-{ex.Number}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el collector: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDecline_Click_1(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void cmbStatus_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
