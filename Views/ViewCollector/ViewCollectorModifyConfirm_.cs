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

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorModifyConfirm_ : Form
    {
        private CollectorDTO newCollector;
        private CollectorDTO oldCollector;
        private Form viewCollector;

        public ViewCollectorModifyConfirm_(CollectorDTO _newCollectorDTO, CollectorDTO _oldCollectorDTO, Form viewCollector)
        {
            this.oldCollector = _oldCollectorDTO;
            this.newCollector = _newCollectorDTO;
            this.viewCollector = viewCollector;
            InitializeComponent();
            loadCollector();
            this.viewCollector = viewCollector;
        }
        private void loadCollector()
        {
            lbWorkerCode.Text = newCollector.workerCode;
            lbId.Text = newCollector.id.ToString();
            lbFirstName.Text = newCollector.firstName;
            lbLastName.Text = newCollector.lastName;
            lbPhone.Text = newCollector.phone.ToString();
            if (newCollector.status == 1)
            {
                lbStatus.Text = "Activo";

            }
            else
            {
                lbStatus.Text = "Inactivo";
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ViewCollectorModifyConfirm__Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.Collector.update.execute(oldCollector.id, newCollector.workerCode, newCollector.id, newCollector.firstName, newCollector.lastName, newCollector.phone, newCollector.status);
                if (viewCollector is ViewCollector parent)
                        {
                            parent.loadCollectors();
                            MessageBox.Show("Collector modificado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    viewCollector.Show();
                            this.Close();
                        }
            }
            catch (InvalidOperationException ex)
            {
                // Viene del repositorio cuando ORA-00001 (duplicado PK/UNIQUE)
                MessageBox.Show(ex.Message, "Clave Id posee una existencia en la base de datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }
    }
}
