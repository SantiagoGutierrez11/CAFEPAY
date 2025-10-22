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

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorModifyId : Form
    {
        private Form viewCollector;
        private CollectorDTO newCollector;
        private CollectorDTO oldCollector;
        public ViewCollectorModifyId(CollectorDTO newCollector, CollectorDTO oldCollector, System.Windows.Forms.Form _viewCollector)
        {
            InitializeComponent();
            this.oldCollector = oldCollector;
            this.newCollector = newCollector;
            this.viewCollector = _viewCollector;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void ViewCollectorModifyId_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var _id = textBoxId.Text?.Trim();

            //VALIDACIONES ANTES de usar long.Parse
            if (string.IsNullOrWhiteSpace(_id))
            {
                MessageBox.Show("Por favor ingrese la cedula de confirmación.");
                textBoxId.Focus();
                return;
            }

            if (!long.TryParse(_id, out long enteredId))
            {
                MessageBox.Show("La cedula debe contener solo números.");
                textBoxId.Focus();
                return;
            }

            //VALIDAR LONGITUD (8-10 dígitos)
            if (_id.Length < 8 || _id.Length > 10)
            {
                MessageBox.Show("La cedula debe tener entre 8 y 10 dígitos.");
                textBoxId.Focus();
                return;
            }

            //VALIDAR QUE NO EMPIECE CON 0
            if (_id.StartsWith("0"))
            {
                MessageBox.Show("La cedula no puede empezar con 0.");
                textBoxId.Focus();
                return;
            }

            //VALIDAR QUE NO TODOS LOS DÍGITOS IGUALES
            if (_id.All(c => c == _id[0]))
            {
                MessageBox.Show("La cedula no puede tener todos los dígitos iguales.");
                textBoxId.Focus();
                return;
            }

            //SOLO SI PASA TODAS LAS VALIDACIONES, comparar
            if (enteredId == newCollector.id)
            {
                ViewCollectorModifyConfirm_ viewCollectorModifyConfirm_ = new ViewCollectorModifyConfirm_(newCollector, oldCollector, viewCollector);
                viewCollectorModifyConfirm_.Owner = this.Owner;
                viewCollectorModifyConfirm_.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("La cedula ingresada no coincide con la nueva cedula del recolector a modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxId.Focus();
            }
        }
    }
}