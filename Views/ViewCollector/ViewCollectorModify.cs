using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.Views.ViewCollect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorModify : Form
    {
        private System.Windows.Forms.Form viewCollector;
        private CollectorDTO oldCollectorDTO;
        public ViewCollectorModify(CollectorDTO _oldCollectorDTO, Form _viewCollector)
        {
            this.viewCollector = _viewCollector;
            this.oldCollectorDTO = _oldCollectorDTO;
            InitializeComponent();
            loadCollector();
            loadComboBox();
        }
        private void loadCollector()
        {
            textBoxFirstName.Text = oldCollectorDTO.firstName;
            textBoxLastName.Text = oldCollectorDTO.lastName;
            textBoxId.Text = oldCollectorDTO.id.ToString();
            textBoxPhone.Text = oldCollectorDTO.phone.ToString();
            cmbStatus.SelectedValue = oldCollectorDTO.status;
            lbWorkerCode.Text = oldCollectorDTO.workerCode;
        }
        private class StatusItem
        {
            public int value { get; set; }
            public string text { get; set; }
        }
   
        private void loadComboBox()
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

        private void ViewCollectorModify__Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtBoxWorkerId_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var _workerCode = oldCollectorDTO.workerCode;
            var _id = textBoxId.Text?.Trim();
            var _firstName = textBoxFirstName.Text?.Trim();
            var _lastName = textBoxLastName.Text?.Trim();
            var _phone = textBoxPhone.Text?.Trim();
            var _status = (int)cmbStatus.SelectedValue; // Estado activo por defecto

            // 1) Validaciones mínimas de UI
           
            if (string.IsNullOrWhiteSpace(_id)) { MessageBox.Show("Cédula/ID es requerida."); textBoxId.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_firstName)) { MessageBox.Show("Nombres es requerido."); textBoxFirstName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_lastName)) { MessageBox.Show("Apellidos es requerido."); textBoxLastName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_phone)) { MessageBox.Show("Teléfono es requerido."); textBoxPhone.Focus(); return; }

                var newCollectorDTO = new CollectorDTO
                {
                    workerCode = _workerCode,
                    id = long.Parse(_id),
                    firstName = _firstName,
                    lastName = _lastName,
                    phone = long.Parse(_phone),
                    status = _status
                };
        
            if (newCollectorDTO.id != oldCollectorDTO.id)
            {
                var ownerViewCollector = this.Owner;
                ViewCollectorModifyId viewCollectorModifyId = new ViewCollectorModifyId(newCollectorDTO,oldCollectorDTO, viewCollector);
                viewCollectorModifyId.Owner = this;
                this.Hide();
                viewCollectorModifyId.Show();
            }
            else
            {
                ViewCollectorModifyConfirm_ viewCollectorModifyConfirm = new ViewCollectorModifyConfirm_(newCollectorDTO,oldCollectorDTO, viewCollector);
                viewCollectorModifyConfirm.Owner = this;
                viewCollectorModifyConfirm.Show();
                this.Hide();
              
            }
        }

        private void textBoxFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();

        }
    }
}
