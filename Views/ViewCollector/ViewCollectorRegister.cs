using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorRegister : Form
    {
        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Registrar
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64); // Gris oscuro para el botón home

        public ViewCollectorRegister()
        {
            InitializeComponent();
            ApplyVisualDesign();
            LoadComboStatus();
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Registrar Nuevo Recolector";

            // 🖼️ LOGO CAFICAUCA (Esquina superior izquierda)
            var logoImage = new PictureBox
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            // Intentar cargar el logo
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    logoImage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    // Si no encuentra la imagen, crear placeholder
                    CreateLogoPlaceholder(logoImage);
                }
            }
            catch (Exception ex)
            {
                CreateLogoPlaceholder(logoImage);
                Console.WriteLine("Error cargando logo: " + ex.Message);
            }
            this.Controls.Add(logoImage);

            // 🏠 ICONO DE CASA (Home) - Usando Button con icono
            var homeButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(this.ClientSize.Width - 50, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = darkGrayColor,
                ForeColor = whiteColor,
                Text = "🏠",
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand
            };

            homeButton.FlatAppearance.BorderSize = 0;
           
            // Hacer botón home con esquinas redondeadas
            GraphicsPath homePath = new GraphicsPath();
            homePath.AddRectangle(new Rectangle(0, 0, 40, 40));
            homeButton.Region = new Region(homePath);

            homeButton.Click += (s, e) => {
                MessageBox.Show("Volviendo al menú principal...", "Información",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };
            this.Controls.Add(homeButton);

            // 📦 PANEL PRINCIPAL (AZUL OSCURO CON BORDE AZUL CLARO)
            var mainFormPanel = new Panel
            {
                Size = new Size(500, 650),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 50)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // 🏷️ TÍTULO PRINCIPAL
            var titleLabel = new Label
            {
                Text = "REGISTRAR RECOLECTOR",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 30)
            };
            mainFormPanel.Controls.Add(titleLabel);

            // 📝 CONTENEDOR DE CAMPOS
            var fieldsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            // Configurar estilo de los controles existentes
            ConfigureExistingControlsStyle();

            // Agregar campos al diseño
            AddFieldsToDesign(fieldsContainer);

            mainFormPanel.Controls.Add(fieldsContainer);

            // 🔘 PANEL DE BOTONES
            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 15, 0, 0)
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            this.Controls.Add(mainFormPanel);

            // 📍 BREADCRUMB (inicio / recolector / registrar)
            var breadcrumbLabel = new Label
            {
                Text = "inicio / recolector / registrar",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = blackColor,
                Location = new Point(40, mainFormPanel.Bottom + 20),
                AutoSize = true
            };
            this.Controls.Add(breadcrumbLabel);
            
            // 🔄 AJUSTAR AL REDIMENSIONAR
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 50);
                breadcrumbLabel.Location = new Point(40, mainFormPanel.Bottom + 20);
            };
        }

        private void CreateLogoPlaceholder(PictureBox pictureBox)
        {
            // Crear un placeholder para el logo
            var placeholder = new Bitmap(180, 120);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.LightGray);
                
                using (var font = new Font("Segoe UI", 8, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    var text = "LOGO\nCAFICAUCA";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush, 
                        (120 - textSize.Width) / 2, 
                        (80 - textSize.Height) / 2);
                }
            }
            pictureBox.Image = placeholder;
        }

        private void AddFieldsToDesign(Panel container)
        {
            int currentY = 80;
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            int verticalSpacing = 25;

            // 🔹 ID DE RECOLECTOR
            CreateStyledField(container, "ID de Recolector", txtBoxWorkerCode, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Cédula
            CreateStyledField(container, "Cédula", txtBoxId, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Nombres
            CreateStyledField(container, "Nombres", txtBoxFirstName, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Apellidos
            CreateStyledField(container, "Apellidos", txtBoxLastName, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Teléfono
            CreateStyledField(container, "Teléfono", txtBoxPhone, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Estado
            CreateStyledComboBoxField(container, "Estado", cmbStatus, currentY, fieldWidth, labelHeight, fieldHeight);

            // Forzar scroll si es necesario
            container.AutoScrollMinSize = new Size(fieldWidth, currentY + fieldHeight + 50);
        }

        private void CreateStyledField(Panel container, string labelText, Control control, int y, int width, int labelHeight, int fieldHeight)
        {
            // Label en blanco
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, y),
                Size = new Size(width, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(label);

            // Control con estilo
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 8);
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = whiteColor;
                control.ForeColor = blackColor;

                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Padding = new Padding(12, 8, 12, 8);
                }

                ApplyRoundedCorners(control, 8);
                container.Controls.Add(control);
            }
        }

        private void CreateStyledComboBoxField(Panel container, string labelText, ComboBox comboBox, int y, int width, int labelHeight, int fieldHeight)
        {
            // Label en blanco
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, y),
                Size = new Size(width, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(label);

            // ComboBox con estilo
            if (comboBox != null)
            {
                comboBox.Location = new Point(0, y + labelHeight + 8);
                comboBox.Size = new Size(width, fieldHeight);
                comboBox.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                comboBox.BackColor = whiteColor;
                comboBox.ForeColor = blackColor;
                comboBox.FlatStyle = FlatStyle.Flat;
                ApplyRoundedCorners(comboBox, 8);
                container.Controls.Add(comboBox);
            }
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Registrar (Verde)
            btnAccept.Text = "Registrar";
            btnAccept.BackColor = greenColor;
            btnAccept.ForeColor = whiteColor;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAccept.Size = new Size(120, 40);
            ApplyRoundedCorners(btnAccept, 8);

            // Botón Cancelar (Rojo)
            btnDecline.Text = "Cancelar";
            btnDecline.BackColor = Color.FromArgb(183, 32, 46); // Rojo vino
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDecline.Size = new Size(120, 40);
            ApplyRoundedCorners(btnDecline, 8);

            int panelWidth = 420;
            int totalButtonsWidth = btnAccept.Width + 30 + btnDecline.Width;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            btnAccept.Location = new Point(startX, 15);
            btnDecline.Location = new Point(startX + btnAccept.Width + 30, 15);

            buttonPanel.Controls.Add(btnAccept);
            buttonPanel.Controls.Add(btnDecline);
        }

        private void ConfigureExistingControlsStyle()
        {
            var textBoxes = new[] { txtBoxWorkerCode, txtBoxId, txtBoxFirstName, txtBoxLastName, txtBoxPhone };
            foreach (var textBox in textBoxes)
            {
                if (textBox != null)
                {
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = whiteColor;
                    textBox.ForeColor = blackColor;
                    textBox.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                }
            }

            if (cmbStatus != null)
            {
                cmbStatus.FlatStyle = FlatStyle.Flat;
                cmbStatus.BackColor = whiteColor;
                cmbStatus.ForeColor = blackColor;
                cmbStatus.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            }
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control.Width > 0 && control.Height > 0)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }

        private void ApplyLightBlueBorder(Control control, int borderWidth)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(lightBlueColor, borderWidth))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
                }
            };
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
            cmbStatus.SelectedValue = 1;
            cmbStatus.Enabled = false;
        }

        // Resto de tus métodos existentes se mantienen igual...
        #region Event Handlers
        private void ViewCollectorModify_Load(object sender, EventArgs e) { }
        private void txtBoxLastName_TextChanged(object sender, EventArgs e) { }
        private void grupBoxCollectorRegister_Enter(object sender, EventArgs e) { }
        private void lbCollectorId_Click(object sender, EventArgs e) { }
        private void lbCollecorName_Click(object sender, EventArgs e) { }
        private void lbCollectorPhone_Click(object sender, EventArgs e) { }
        private void txtBoxLastName_TextChanged_1(object sender, EventArgs e) { }
        private void txtBoxPhone_TextChanged(object sender, EventArgs e) { }
        private void txtBoxLastName_TextChanged_2(object sender, EventArgs e) { }
        private void textBoxId_TextChanged(object sender, EventArgs e) { }
        private void textBoxFirstName_TextChanged(object sender, EventArgs e) { }
        private void textBoxWorkerCode_TextChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged_1(object sender, EventArgs e) { }
        #endregion

        private void btnAccept_Click_1(object sender, EventArgs e)
        {
            var _workerCode = txtBoxWorkerCode.Text?.Trim();
            var _id = txtBoxId.Text?.Trim();
            var _firstName = txtBoxFirstName.Text?.Trim();
            var _lastName = txtBoxLastName.Text?.Trim();
            var _phone = txtBoxPhone.Text?.Trim();
            var _status = (int)cmbStatus.SelectedValue;

            // 1) Validaciones mínimas de UI
            if (string.IsNullOrWhiteSpace(_workerCode))
            {
                MessageBox.Show("Worker Code es requerido.\n\nEjemplo: W00001", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_id))
            {
                MessageBox.Show("Cédula/ID es requerida.\n\nDebe contener entre 8 y 10 dígitos.", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxId.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_firstName))
            {
                MessageBox.Show("Nombres es requerido.\n\nEjemplo: Juan Carlos", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxFirstName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_lastName))
            {
                MessageBox.Show("Apellidos es requerido.\n\nEjemplo: Pérez García", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxLastName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_phone))
            {
                MessageBox.Show("Teléfono es requerido.\n\nDebe tener 10 dígitos.\nEjemplo: 3123456789", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxPhone.Focus(); return;
            }

            //NUEVAS VALIDACIONES PARA WORKER CODE
            if (_workerCode.Length != 6)
            {
                MessageBox.Show("El Worker Code debe tener exactamente 6 caracteres.\n\nEjemplo: W00001", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus();
                return;
            }

            if (!_workerCode.ToUpper().StartsWith("W"))
            {
                MessageBox.Show("El Worker Code debe empezar con 'W'.\n\nEjemplo: W00001", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus();
                return;
            }

            //NUEVAS VALIDACIONES PARA ID (CÉDULA)
            if (!long.TryParse(_id, out long idValue))
            {
                MessageBox.Show("La cédula debe contener solo números.\n\nEjemplo: 1234567890", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxId.Focus();
                return;
            }

            if (_id.Length < 8 || _id.Length > 10)
            {
                MessageBox.Show("La cédula debe tener entre 8 y 10 dígitos.\n\nEjemplos:\n• 12345678 (8 dígitos)\n• 1234567890 (10 dígitos)", "Longitud incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxId.Focus();
                return;
            }

            if (_id.StartsWith("0"))
            {
                MessageBox.Show("La cédula no puede empezar con 0.\n\nEjemplo válido: 123456789\nEjemplo inválido: 012345678", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxId.Focus();
                return;
            }

            //VALIDACIONES PARA TELÉFONO
            if (_phone.Length != 10)
            {
                MessageBox.Show("El teléfono debe tener exactamente 10 dígitos.\n\nEjemplo: 3123456789", "Longitud incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxPhone.Focus();
                return;
            }

            if (!_phone.All(char.IsDigit))
            {
                MessageBox.Show("El teléfono solo puede contener números.\n\nEjemplo válido: 3123456789\nEjemplo inválido: 312-456-789", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxPhone.Focus();
                return;
            }

            //VALIDACIONES PARA NOMBRES/APELLIDOS
            if (_firstName.Length < 3 || _firstName.Length > 30)
            {
                MessageBox.Show("El nombre debe tener entre 3 y 30 caracteres.\n\nEjemplos válidos:\n• Ana\n• Juan Carlos\n• María Fernanda", "Longitud incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxFirstName.Focus();
                return;
            }

            if (_lastName.Length < 3 || _lastName.Length > 30)
            {
                MessageBox.Show("El apellido debe tener entre 3 y 30 caracteres.\n\nEjemplos válidos:\n• López\n• Pérez García\n• Rodríguez Hernández", "Longitud incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(ex.Message, "Registro duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBoxWorkerCode.Focus();
            }
            catch (ArgumentException ex)
            {
                // Errores de validación del dominio
                MessageBox.Show(ex.Message, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OracleException ex) when (ex.Number == 1400)
            {
                MessageBox.Show("Hay campos obligatorios vacíos en la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex) when (ex.Number == 12899)
            {
                MessageBox.Show("Algún campo supera el tamaño permitido por la columna en la base de datos.", "Error de base de datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OracleException ex)
            {
                MessageBox.Show($"Error de base de datos:\nORA-{ex.Number}: {ex.Message}", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al guardar el recolector:\n" + ex.Message, "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecline_Click_1(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }
    }
}