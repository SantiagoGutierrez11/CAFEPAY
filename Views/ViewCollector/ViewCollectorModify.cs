using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.Views.ViewCollect;
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorModify : Form
    {
        private System.Windows.Forms.Form viewCollector;
        private CollectorDTO oldCollectorDTO;

        // Colores exactos del diseño (igual que ViewCollectorRegister)
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Modificar
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64); // Gris oscuro para el botón home
        private Color errorColor = Color.LightPink; // Color para campos con error

        // Variable para el botón home
        private Button homeButton;
        private bool isClosing = false;

        public ViewCollectorModify(CollectorDTO _oldCollectorDTO, Form _viewCollector)
        {
            this.viewCollector = _viewCollector;
            this.oldCollectorDTO = _oldCollectorDTO;
            InitializeComponent();
            ApplyVisualDesign();
            loadCollector();
            loadComboBox();
            ConfigureRealTimeValidation();
            ConfigureMaxLengthRestrictions(); // ← NUEVO: Configurar restricciones de longitud

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        // ← NUEVO MÉTODO: Configurar restricciones de longitud máxima
        private void ConfigureMaxLengthRestrictions()
        {
            // Worker Code: Longitud exacta 6 caracteres (solo lectura, pero por consistencia)
            textBoxWorkerCode.MaxLength = 6;

            // Cédula: Máximo 10 dígitos
            textBoxId.MaxLength = 10;

            // Nombres: Máximo 30 caracteres
            textBoxFirstName.MaxLength = 30;

            // Apellidos: Máximo 30 caracteres
            textBoxLastName.MaxLength = 30;

            // Celular: Longitud exacta 10 dígitos
            textBoxPhone.MaxLength = 10;
        }

        // ← MÉTODO ACTUALIZADO: Configurar validación en tiempo real con BLOQUEO
        private void ConfigureRealTimeValidation()
        {
            // Configurar eventos Validating para BLOQUEAR cambio de campo
            textBoxId.Validating += ValidarCedula;
            textBoxFirstName.Validating += ValidarNombres;
            textBoxLastName.Validating += ValidarApellidos;
            textBoxPhone.Validating += ValidarCelular;

            // Configurar eventos Enter para resetear color
            textBoxId.Enter += (s, e) => ResetearColorCampo(textBoxId);
            textBoxFirstName.Enter += (s, e) => ResetearColorCampo(textBoxFirstName);
            textBoxLastName.Enter += (s, e) => ResetearColorCampo(textBoxLastName);
            textBoxPhone.Enter += (s, e) => ResetearColorCampo(textBoxPhone);

            // ← IMPORTANTE: Deshabilitar validación automática para botones
            btnAccept.CausesValidation = false;
            btnDecline.CausesValidation = false;
            if (homeButton != null)
                homeButton.CausesValidation = false;
        }

        // ← NUEVO MÉTODO: Resetear color del campo
        private void ResetearColorCampo(Control control)
        {
            control.BackColor = whiteColor;
        }

        // ← MÉTODO ACTUALIZADO: Validar Cédula con BLOQUEO
        private void ValidarCedula(object sender, CancelEventArgs e)
        {
            // Si estamos cerrando, no validar
            if (isClosing)
            {
                e.Cancel = false;
                return;
            }

            var _id = textBoxId.Text?.Trim();
            var errores = new List<string>();

            // ← VALIDAR CAMPO VACÍO EN TIEMPO REAL
            if (string.IsNullOrWhiteSpace(_id))
            {
                errores.Add("• La cédula es requerida");
            }
            else
            {
                // Validar que sean solo números
                if (!long.TryParse(_id, out long idValue))
                {
                    errores.Add("• Debe contener solo números");
                }
                else
                {
                    string idString = idValue.ToString();

                    // Validar longitud entre 8 y 10 dígitos
                    if (idString.Length < 8 || idString.Length > 10)
                    {
                        errores.Add("• Debe tener entre 8 y 10 dígitos");
                    }

                    // Validar que no empiece con 0
                    if (idString.StartsWith("0"))
                    {
                        errores.Add("• No puede empezar con 0");
                    }

                    // Validar que no sean todos los dígitos iguales
                    if (AreAllDigitsSame(idString))
                    {
                        errores.Add("• No puede tener todos los dígitos iguales");
                    }

                    // Validar que no sea negativo
                    if (idValue < 0)
                    {
                        errores.Add("• No puede ser negativa");
                    }
                }
            }

            if (errores.Any())
            {
                // ← PINTAR EL CAMPO DE ERROR INMEDIATAMENTE
                textBoxId.BackColor = errorColor;

                MessageBox.Show($"La cédula tiene errores:\n\n{string.Join("\n", errores)}",
                    "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ← BLOQUEAR CAMBIO DE CAMPO
                e.Cancel = true;
                textBoxId.SelectAll();
            }
            else
            {
                textBoxId.BackColor = whiteColor;
            }
        }

        // ← MÉTODO ACTUALIZADO: Validar Nombres con BLOQUEO
        private void ValidarNombres(object sender, CancelEventArgs e)
        {
            // Si estamos cerrando, no validar
            if (isClosing)
            {
                e.Cancel = false;
                return;
            }

            var _firstName = textBoxFirstName.Text?.Trim();
            var errores = new List<string>();

            // ← VALIDAR CAMPO VACÍO EN TIEMPO REAL
            if (string.IsNullOrWhiteSpace(_firstName))
            {
                errores.Add("• El nombre es requerido");
            }
            else
            {
                // Longitud mínima y máxima
                if (_firstName.Length < 3)
                {
                    errores.Add("• Debe tener al menos 3 caracteres");
                }

                if (_firstName.Length > 30)
                {
                    errores.Add("• No puede tener más de 30 caracteres");
                }

                // Solo letras, espacios y algunos caracteres especiales comunes en nombres
                if (!Regex.IsMatch(_firstName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\'-]+$"))
                {
                    errores.Add("• Solo puede contener letras, espacios, apóstrofes y guiones");
                }

                // No puede empezar o terminar con espacios, puntos, guiones
                if (_firstName.StartsWith(" ") || _firstName.EndsWith(" ") ||
                    _firstName.StartsWith(".") || _firstName.EndsWith(".") ||
                    _firstName.StartsWith("-") || _firstName.EndsWith("-") ||
                    _firstName.StartsWith("'") || _firstName.EndsWith("'"))
                {
                    errores.Add("• No puede empezar o terminar con espacios, puntos, guiones o apóstrofes");
                }

                // No puede tener espacios múltiples consecutivos
                if (Regex.IsMatch(_firstName, @"\s{2,}"))
                {
                    errores.Add("• No puede tener espacios múltiples consecutivos");
                }

                // Validar que tenga al menos una letra
                if (!Regex.IsMatch(_firstName, @"[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]"))
                {
                    errores.Add("• Debe contener al menos una letra");
                }
            }

            if (errores.Any())
            {
                // ← PINTAR EL CAMPO DE ERROR INMEDIATAMENTE
                textBoxFirstName.BackColor = errorColor;

                MessageBox.Show($"El nombre tiene errores:\n\n{string.Join("\n", errores)}",
                    "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ← BLOQUEAR CAMBIO DE CAMPO
                e.Cancel = true;
                textBoxFirstName.SelectAll();
            }
            else
            {
                textBoxFirstName.BackColor = whiteColor;
            }
        }

        // ← MÉTODO ACTUALIZADO: Validar Apellidos con BLOQUEO
        private void ValidarApellidos(object sender, CancelEventArgs e)
        {
            // Si estamos cerrando, no validar
            if (isClosing)
            {
                e.Cancel = false;
                return;
            }

            var _lastName = textBoxLastName.Text?.Trim();
            var errores = new List<string>();

            // ← VALIDAR CAMPO VACÍO EN TIEMPO REAL
            if (string.IsNullOrWhiteSpace(_lastName))
            {
                errores.Add("• El apellido es requerido");
            }
            else
            {
                // Longitud mínima y máxima
                if (_lastName.Length < 3)
                {
                    errores.Add("• Debe tener al menos 3 caracteres");
                }

                if (_lastName.Length > 30)
                {
                    errores.Add("• No puede tener más de 30 caracteres");
                }

                // Solo letras, espacios y algunos caracteres especiales comunes en apellidos
                if (!Regex.IsMatch(_lastName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\'-]+$"))
                {
                    errores.Add("• Solo puede contener letras, espacios, apóstrofes y guiones");
                }

                // No puede empezar o terminar con espacios, puntos, guiones
                if (_lastName.StartsWith(" ") || _lastName.EndsWith(" ") ||
                    _lastName.StartsWith(".") || _lastName.EndsWith(".") ||
                    _lastName.StartsWith("-") || _lastName.EndsWith("-") ||
                    _lastName.StartsWith("'") || _lastName.EndsWith("'"))
                {
                    errores.Add("• No puede empezar o terminar con espacios, puntos, guiones o apóstrofes");
                }

                // No puede tener espacios múltiples consecutivos
                if (Regex.IsMatch(_lastName, @"\s{2,}"))
                {
                    errores.Add("• No puede tener espacios múltiples consecutivos");
                }

                // Validar que tenga al menos una letra
                if (!Regex.IsMatch(_lastName, @"[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]"))
                {
                    errores.Add("• Debe contener al menos una letra");
                }
            }

            if (errores.Any())
            {
                // ← PINTAR EL CAMPO DE ERROR INMEDIATAMENTE
                textBoxLastName.BackColor = errorColor;

                MessageBox.Show($"El apellido tiene errores:\n\n{string.Join("\n", errores)}",
                    "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ← BLOQUEAR CAMBIO DE CAMPO
                e.Cancel = true;
                textBoxLastName.SelectAll();
            }
            else
            {
                textBoxLastName.BackColor = whiteColor;
            }
        }

        // ← MÉTODO CORREGIDO: Validar Celular con BLOQUEO - SEGUNDO DÍGITO 0, 1 o 2
        private void ValidarCelular(object sender, CancelEventArgs e)
        {
            // Si estamos cerrando, no validar
            if (isClosing)
            {
                e.Cancel = false;
                return;
            }

            var _phone = textBoxPhone.Text?.Trim();
            var errores = new List<string>();

            // ← VALIDAR CAMPO VACÍO EN TIEMPO REAL
            if (string.IsNullOrWhiteSpace(_phone))
            {
                errores.Add("• El celular es requerido");
            }
            else
            {
                // Validar longitud exacta de 10 dígitos
                if (_phone.Length != 10)
                {
                    errores.Add("• Debe tener exactamente 10 dígitos");
                }

                // Validar que solo contenga dígitos
                if (!_phone.All(char.IsDigit))
                {
                    errores.Add("• Solo puede contener dígitos");
                }

                // Validar que no empiece con 0
                if (_phone.StartsWith("0"))
                {
                    errores.Add("• No puede empezar con 0");
                }

                // Validar que no sean todos los números iguales
                if (AreAllDigitsSame(_phone))
                {
                    errores.Add("• No puede tener todos los dígitos iguales");
                }

                // Validar que empiece con 3
                if (!_phone.StartsWith("3"))
                {
                    errores.Add("• Debe empezar con 3");
                }

                // ← CORRECCIÓN: Segundo dígito debe ser 0, 1 o 2 (NO 3, 4, 5)
                if (_phone.Length >= 2)
                {
                    char secondDigit = _phone[1];
                    if (secondDigit != '0' && secondDigit != '1' && secondDigit != '2')
                    {
                        errores.Add("• Segundo dígito debe ser 0, 1 o 2");
                    }
                }
            }

            if (errores.Any())
            {
                // ← PINTAR EL CAMPO DE ERROR INMEDIATAMENTE
                textBoxPhone.BackColor = errorColor;

                MessageBox.Show($"El celular tiene errores:\n\n{string.Join("\n", errores)}",
                    "Errores de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // ← BLOQUEAR CAMBIO DE CAMPO
                e.Cancel = true;
                textBoxPhone.SelectAll();
            }
            else
            {
                textBoxPhone.BackColor = whiteColor;
            }
        }

        // ← MÉTODO AUXILIAR: Verificar si todos los dígitos son iguales
        private bool AreAllDigitsSame(string input)
        {
            return input.Length > 0 && input.All(c => c == input[0]);
        }

        // ← NUEVO MÉTODO: Manejar el cierre del formulario
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                isClosing = true;
            }
            base.OnFormClosing(e);
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Modificar Recolector";

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
            homeButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(this.ClientSize.Width - 50, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = darkGrayColor,
                ForeColor = whiteColor,
                Text = "🏠",
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand,
                CausesValidation = false // ← IMPORTANTE: No validar al hacer clic
            };

            homeButton.FlatAppearance.BorderSize = 0;

            // Hacer botón home con esquinas redondeadas
            GraphicsPath homePath = new GraphicsPath();
            homePath.AddRectangle(new Rectangle(0, 0, 40, 40));
            homeButton.Region = new Region(homePath);

            homeButton.Click += (s, e) => {
                // Volver al menú principal
                var viewMain = new ViewMain.ViewMain();
                viewMain.Show();
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
                Text = "MODIFICAR RECOLECTOR",
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

            // 📍 BREADCRUMB (inicio / recolector / modificar)
            var breadcrumbLabel = new Label
            {
                Text = "inicio / recolector / modificar",
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
                homeButton.Location = new Point(this.ClientSize.Width - 50, 10);
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

            // 🔹 ID DE RECOLECTOR (SOLO LECTURA)
            CreateStyledReadOnlyField(container, "ID de Recolector", textBoxWorkerCode, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Cédula
            CreateStyledFieldWithExample(container, "Cédula", "Ej: 1058548042", textBoxId, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Nombres
            CreateStyledFieldWithExample(container, "Nombres", "Ej: Andrés Felipe", textBoxFirstName, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Apellidos
            CreateStyledFieldWithExample(container, "Apellidos", "Ej: Obando Quintero", textBoxLastName, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Celular ← CAMBIO: De "Teléfono" a "Celular"
            CreateStyledFieldWithExample(container, "Celular", "Ej: 3125170927", textBoxPhone, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Estado
            CreateStyledComboBoxField(container, "Estado", cmbStatus, currentY, fieldWidth, labelHeight, fieldHeight);

            // Forzar scroll si es necesario
            container.AutoScrollMinSize = new Size(fieldWidth, currentY + fieldHeight + 50);
        }

        // ← MÉTODO ACTUALIZADO: Crear campo con título y ejemplo separados (igual que Register)
        private void CreateStyledFieldWithExample(Panel container, string labelText, string exampleText, Control control, int y, int width, int labelHeight, int fieldHeight)
        {
            // Panel para contener título y ejemplo
            var headerPanel = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, labelHeight),
                BackColor = Color.Transparent
            };

            // Label principal (título)
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, 0),
                Size = new Size(width / 2, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Label de ejemplo (estilo diferente)
            var exampleLabel = new Label
            {
                Text = exampleText,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.LightGray,
                Location = new Point(width / 2, 0),
                Size = new Size(width / 2, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight
            };

            headerPanel.Controls.Add(label);
            headerPanel.Controls.Add(exampleLabel);
            container.Controls.Add(headerPanel);

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

        private void CreateStyledReadOnlyField(Panel container, string labelText, Control control, int y, int width, int labelHeight, int fieldHeight)
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

            // Control con estilo de solo lectura
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 8);
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = Color.FromArgb(240, 240, 240); // Fondo gris claro
                control.ForeColor = Color.FromArgb(100, 100, 100); // Texto gris
                control.Enabled = true; // Permitir focus pero no edición

                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Padding = new Padding(12, 8, 12, 8);
                    textBox.ReadOnly = true;
                    textBox.Cursor = Cursors.Default; // Cursor normal, no de texto
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
            // Botón Modificar (Verde)
            btnAccept.Text = "Modificar";
            btnAccept.BackColor = greenColor;
            btnAccept.ForeColor = whiteColor;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAccept.Size = new Size(120, 40);
            btnAccept.CausesValidation = false; // ← IMPORTANTE: No validar al hacer clic
            ApplyRoundedCorners(btnAccept, 8);

            // Botón Cancelar (Rojo)
            btnDecline.Text = "Cancelar";
            btnDecline.BackColor = Color.FromArgb(183, 32, 46); // Rojo vino
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDecline.Size = new Size(120, 40);
            btnDecline.CausesValidation = false; // ← IMPORTANTE: No validar al hacer clic
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
            var textBoxes = new[] { textBoxId, textBoxFirstName, textBoxLastName, textBoxPhone, textBoxWorkerCode };
            foreach (var textBox in textBoxes)
            {
                if (textBox != null)
                {
                    textBox.BorderStyle = BorderStyle.None;
                    textBox.BackColor = whiteColor;
                    textBox.ForeColor = blackColor;
                    textBox.Font = new Font("Segoe UI", 11, FontStyle.Regular);

                    // Estilo especial para el campo de solo lectura
                    if (textBox == textBoxWorkerCode)
                    {
                        textBox.BackColor = Color.FromArgb(240, 240, 240);
                        textBox.ForeColor = Color.FromArgb(100, 100, 100);
                        textBox.ReadOnly = true;
                        textBox.Cursor = Cursors.Default;
                        textBox.TextAlign = HorizontalAlignment.Left;

                        textBox.GotFocus += (s, e) => {
                            // Enviar el focus a otro control inmediatamente
                            this.SelectNextControl(textBox, true, true, true, true);
                        };
                    }
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

        private void loadCollector()
        {
            textBoxFirstName.Text = oldCollectorDTO.firstName;
            textBoxLastName.Text = oldCollectorDTO.lastName;
            textBoxId.Text = oldCollectorDTO.id.ToString();
            textBoxPhone.Text = oldCollectorDTO.phone;
            cmbStatus.SelectedValue = oldCollectorDTO.status;
            textBoxWorkerCode.Text = oldCollectorDTO.workerCode;
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
            cmbStatus.SelectedValue = oldCollectorDTO.status; // Mantener el estado actual
        }

        //Componentes de eventos
        #region
        private void ViewCollectorModify__Load(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txtBoxWorkerId_TextChanged(object sender, EventArgs e) { }
        private void textBoxFirstName_TextChanged(object sender, EventArgs e) { }
        #endregion

        private void btnAccept_Click(object sender, EventArgs e)
        {
            var _workerCode = oldCollectorDTO.workerCode;
            var _id = textBoxId.Text?.Trim();
            var _firstName = textBoxFirstName.Text?.Trim();
            var _lastName = textBoxLastName.Text?.Trim();
            var _phone = textBoxPhone.Text?.Trim();
            var _status = (int)cmbStatus.SelectedValue;

            // ← SOLO validar campos vacíos - TODO lo demás ya se validó en tiempo real
            if (string.IsNullOrWhiteSpace(_id))
            {
                MessageBox.Show("Cédula es requerida", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_firstName))
            {
                MessageBox.Show("Nombres es requerido", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_lastName))
            {
                MessageBox.Show("Apellidos es requerido", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_phone))
            {
                MessageBox.Show("Celular es requerido", "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPhone.Focus(); return;
            }

            // Convertir ID a long (ya validado en tiempo real)
            if (!long.TryParse(_id, out long idValue))
            {
                MessageBox.Show("La cédula debe contener solo números", "Formato incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus();
                return;
            }

            try
            {
                var newCollectorDTO = new CollectorDTO
                {
                    workerCode = _workerCode,
                    id = idValue,
                    firstName = _firstName,
                    lastName = _lastName,
                    phone = _phone,
                    status = _status
                };

                if (newCollectorDTO.id != oldCollectorDTO.id)
                {
                    var ownerViewCollector = this.Owner;
                    ViewCollectorModifyId viewCollectorModifyId = new ViewCollectorModifyId(newCollectorDTO, oldCollectorDTO, viewCollector);
                    viewCollectorModifyId.Owner = this;
                    this.Hide();
                    viewCollectorModifyId.Show();
                }
                else
                {
                    ViewCollectorModifyConfirm_ viewCollectorModifyConfirm = new ViewCollectorModifyConfirm_(newCollectorDTO, oldCollectorDTO, viewCollector);
                    viewCollectorModifyConfirm.Owner = this;
                    viewCollectorModifyConfirm.Show();
                    this.Hide();
                }
            }
            catch (InvalidOperationException ex)
            {
                // Viene del repositorio cuando ORA-00001 (duplicado PK/UNIQUE)
                MessageBox.Show(ex.Message, "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxId.Focus();
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
                MessageBox.Show("Error al actualizar el collector: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            isClosing = true;
            // ← PRESERVAR EL ESTADO ORIGINAL DEL FORMULARIO PADRE
            if (this.Owner != null)
            {
                // Guardar el estado actual antes de hacer cualquier cambio
                var originalWindowState = this.Owner.WindowState;

                this.Owner.Show();
                this.Owner.BringToFront();

                // Restaurar el estado original en lugar de forzar "Normal"
                this.Owner.WindowState = originalWindowState;

                this.Owner.Refresh();
            }
            this.Close();
        }

        private void lbWorkerCode_Click(object sender, EventArgs e)
        {

        }
    }
}