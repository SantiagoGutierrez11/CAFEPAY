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
    public partial class ViewCollectorModifyConfirm_ : Form
    {
        private CollectorDTO newCollector;
        private CollectorDTO oldCollector;
        private Form viewCollector;

        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Confirmar
        private Color whiteColor = Color.White;
        private Color successGreen = Color.FromArgb(46, 125, 50); // Verde éxito

        public ViewCollectorModifyConfirm_(CollectorDTO _newCollectorDTO, CollectorDTO _oldCollectorDTO, Form viewCollector)
        {
            this.oldCollector = _oldCollectorDTO;
            this.newCollector = _newCollectorDTO;
            this.viewCollector = viewCollector;
            InitializeComponent();
            ApplyVisualDesign();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Confirmación de Modificación";
            this.WindowState = FormWindowState.Normal;
            this.MaximizeBox = true;

            // 🖼️ LOGO CAFICAUCA (Esquina superior izquierda)
            var logoImage = new PictureBox
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            // Cargar logo desde recursos
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    logoImage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    CreateLogoPlaceholder(logoImage);
                }
            }
            catch (Exception ex)
            {
                CreateLogoPlaceholder(logoImage);
                Console.WriteLine("Error cargando logo: " + ex.Message);
            }
            this.Controls.Add(logoImage);

            // 📦 PANEL PRINCIPAL (AZUL OSCURO CON BORDE AZUL CLARO)
            var mainFormPanel = new Panel
            {
                Size = new Size(500, 730),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 30)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // ✅ ICONO DE CONFIRMACIÓN
            var confirmIcon = new Label
            {
                Text = "✓",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = successGreen,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 5)
            };
            mainFormPanel.Controls.Add(confirmIcon);

            // 🏷️ TÍTULO PRINCIPAL
            var titleLabel = new Label
            {
                Text = "MODIFICACIÓN EXITOSA",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 5)
            };
            mainFormPanel.Controls.Add(titleLabel);

            var subtitleLabel = new Label
            {
                Text = "El recolector ha sido modificado correctamente",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 200, 200),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 30)
            };
            mainFormPanel.Controls.Add(subtitleLabel);

            // 📝 CONTENEDOR DE INFORMACIÓN
            var fieldsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            // Agregar campos al diseño
            AddInfoFieldsToDesign(fieldsContainer);

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

            // 🔄 AJUSTAR AL REDIMENSIONAR
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 30);
            };
        }

        private void AddInfoFieldsToDesign(Panel container)
        {
            // Crear controles NUEVOS en lugar de usar los del diseñador
            int currentY = 150;
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            int verticalSpacing = 35;

            // 🔹 ID DE RECOLECTOR - CREAR NUEVO LABEL
            var workerCodeLabel = new Label();
            workerCodeLabel.Text = newCollector.workerCode;
            CreateStyledInfoField(container, "ID de Recolector", workerCodeLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Cédula - CREAR NUEVO LABEL
            var idLabel = new Label();
            idLabel.Text = newCollector.id.ToString();
            CreateStyledInfoField(container, "Cédula", idLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Nombres - CREAR NUEVO LABEL
            var firstNameLabel = new Label();
            firstNameLabel.Text = newCollector.firstName;
            CreateStyledInfoField(container, "Nombres", firstNameLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Apellidos - CREAR NUEVO LABEL
            var lastNameLabel = new Label();
            lastNameLabel.Text = newCollector.lastName;
            CreateStyledInfoField(container, "Apellidos", lastNameLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Teléfono - CREAR NUEVO LABEL
            var phoneLabel = new Label();
            phoneLabel.Text = newCollector.phone.ToString();
            CreateStyledInfoField(container, "Teléfono", phoneLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Estado - CREAR NUEVO LABEL
            var statusLabel = new Label();
            if (newCollector.status == 1)
            {
                statusLabel.Text = "Activo";
                statusLabel.ForeColor = Color.FromArgb(11, 110, 51); // Verde
                statusLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            }
            else
            {
                statusLabel.Text = "Inactivo";
                statusLabel.ForeColor = Color.FromArgb(183, 32, 46); // Rojo
                statusLabel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            }
            CreateStyledInfoField(container, "Estado", statusLabel, currentY, fieldWidth, labelHeight, fieldHeight);
        }

        private void CreateStyledInfoField(Panel container, string labelText, Control control, int y, int width, int labelHeight, int fieldHeight)
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

            // Control con estilo (igual que los campos de entrada pero de solo lectura)
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 8);
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = Color.FromArgb(240, 240, 240); // Fondo gris claro para solo lectura
                control.ForeColor = Color.FromArgb(80, 80, 80);    // Texto gris oscuro

                if (control is System.Windows.Forms.Label lbl)
                {
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Padding = new Padding(12, 8, 12, 8);

                    // Color especial para el estado
                    if (labelText == "Estado")
                    {
                        if (lbl.Text == "ACTIVO" || lbl.Text == "Activo")
                        {
                            lbl.ForeColor = Color.FromArgb(11, 110, 51); // Verde
                            lbl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        }
                        else if (lbl.Text == "INACTIVO" || lbl.Text == "Inactivo")
                        {
                            lbl.ForeColor = Color.FromArgb(183, 32, 46); // Rojo
                            lbl.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        }
                    }
                }

                ApplyRoundedCorners(control, 8);
                container.Controls.Add(control);
            }
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Confirmar (Verde)
            btnAccept.Text = "CONFIRMAR";
            btnAccept.BackColor = greenColor;
            btnAccept.ForeColor = whiteColor;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAccept.Size = new Size(120, 40);
            btnAccept.Cursor = Cursors.Hand;
            ApplyRoundedCorners(btnAccept, 8);

            // Botón Cancelar (Rojo)
            btnDecline.Text = "CANCELAR";
            btnDecline.BackColor = Color.FromArgb(183, 32, 46); // Rojo vino
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDecline.Size = new Size(120, 40);
            btnDecline.Cursor = Cursors.Hand;
            ApplyRoundedCorners(btnDecline, 8);

            int panelWidth = 420;
            int totalButtonsWidth = btnAccept.Width + 30 + btnDecline.Width;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            btnAccept.Location = new Point(startX, 15);
            btnDecline.Location = new Point(startX + btnAccept.Width + 30, 15);

            buttonPanel.Controls.Add(btnAccept);
            buttonPanel.Controls.Add(btnDecline);
        }

        private void CreateLogoPlaceholder(PictureBox pictureBox)
        {
            var placeholder = new Bitmap(320, 70);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.LightGray);

                using (var font = new Font("Segoe UI", 10, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    var text = "LOGO CAFICAUCA";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush,
                        (320 - textSize.Width) / 2,
                        (70 - textSize.Height) / 2);
                }
            }
            pictureBox.Image = placeholder;
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

        private void label1_Click(object sender, EventArgs e) { }

        private void ViewCollectorModifyConfirm__Load(object sender, EventArgs e) { }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.CollectorServices.update.execute(oldCollector.id, newCollector.workerCode, newCollector.id, newCollector.firstName, newCollector.lastName, newCollector.phone, newCollector.status);
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