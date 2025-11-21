using CAFEPAY.ArqHex.Share.DTO;
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
    public partial class ViewCollectorModifyId : Form
    {
        private Form viewCollector;
        private CollectorDTO newCollector;
        private CollectorDTO oldCollector;
        private Form viewMain;

        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Modificar
        private Color redColor = Color.FromArgb(183, 32, 46);      // Rojo vino para advertencia
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64); // Gris oscuro para el botón Regresar

        public ViewCollectorModifyId(CollectorDTO newCollector, CollectorDTO oldCollector, System.Windows.Forms.Form _viewCollector, Form _viewMain)
        {
            InitializeComponent();
            this.oldCollector = oldCollector;
            this.newCollector = newCollector;
            this.viewCollector = _viewCollector;
            this.viewMain = _viewMain;
            ApplyVisualDesign();
            LoadData();

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
            this.Text = "Confirmar Modificación de Cédula";

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
                Size = new Size(500, 500),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 50)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // ⚠️ TÍTULO PRINCIPAL (ADVERTENCIA)
            var titleLabel = new Label
            {
                Text = "¡SE ESTÁ MODIFICANDO LA CÉDULA!",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = redColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 10)
            };
            mainFormPanel.Controls.Add(titleLabel);

            // 📝 SUBTÍTULO/INSTRUCCIÓN
            var subtitleLabel = new Label
            {
                Text = "Confirma la cédula a modificar",
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 30)
            };
            mainFormPanel.Controls.Add(subtitleLabel);

            // 📋 CONTENEDOR DE CAMPOS
            var fieldsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = false
            };

            // Agregar campo de cédula actual
            AddCurrentIdField(fieldsContainer);

            // Agregar campo de confirmación
            AddConfirmationField(fieldsContainer);

            mainFormPanel.Controls.Add(fieldsContainer);

            // 🔘 PANEL DE BOTONES
            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 20, 0, 0)
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            this.Controls.Add(mainFormPanel);

            // 🔄 AJUSTAR AL REDIMENSIONAR
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 50);
            };
        }

        private void AddCurrentIdField(Panel container)
        {
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;

            // Label "Cédula Actual"
            var currentIdLabel = new Label
            {
                Text = "CÉDULA ACTUAL",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 180, 220),
                Location = new Point(0, 0),
                Size = new Size(fieldWidth, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(currentIdLabel);

            // Campo de cédula actual (solo lectura)
            var currentIdField = new Label
            {
                Text = oldCollector.id.ToString(),
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(0, labelHeight + 8),
                Size = new Size(fieldWidth, fieldHeight),
                BackColor = Color.FromArgb(240, 240, 240),
                TextAlign = ContentAlignment.MiddleLeft,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(12, 8, 12, 8)
            };
            ApplyRoundedCorners(currentIdField, 8);
            container.Controls.Add(currentIdField);
        }

        private void AddConfirmationField(Panel container)
        {
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            
            // Label "Nueva Cédula"
            var newIdLabel = new Label
            {
                Text = "NUEVA CÉDULA A CONFIRMAR",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, 100),
                Size = new Size(fieldWidth, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(newIdLabel);

            // Campo de texto para confirmación
            if (textBoxId != null)
            {
                textBoxId.Location = new Point(0, 100 + labelHeight + 8);
                textBoxId.Size = new Size(fieldWidth, fieldHeight);
                textBoxId.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                textBoxId.BackColor = whiteColor;
                textBoxId.ForeColor = blackColor;
                textBoxId.BorderStyle = BorderStyle.FixedSingle;
                textBoxId.Padding = new Padding(12, 8, 12, 8);
                textBoxId.TextAlign = HorizontalAlignment.Left;

                ApplyRoundedCorners(textBoxId, 8);
                container.Controls.Add(textBoxId);
            }

            // Información adicional
            var infoLabel = new Label
            {
                Text = "Ingrese la nueva cédula para confirmar la modificación",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.FromArgb(200, 200, 200),
                Location = new Point(0, 100 + labelHeight + fieldHeight + 15),
                Size = new Size(fieldWidth, 20),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(infoLabel);
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Modificar (Verde)
            btnAccept.Text = "MODIFICAR";
            btnAccept.BackColor = greenColor;
            btnAccept.ForeColor = whiteColor;
            btnAccept.FlatStyle = FlatStyle.Flat;
            btnAccept.FlatAppearance.BorderSize = 0;
            btnAccept.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnAccept.Size = new Size(120, 40);
            btnAccept.Cursor = Cursors.Hand;
            ApplyRoundedCorners(btnAccept, 8);

            // Botón Regresar (Gris oscuro)
            btnDecline.Text = "REGRESAR";
            btnDecline.BackColor = darkGrayColor;
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

        private void LoadData()
        {
            // Los datos ya se cargan en los controles dinámicos
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void ViewCollectorModifyId_Load(object sender, EventArgs e)
        {
            // Cargar datos si es necesario
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
                ViewCollectorModifyConfirm_ viewCollectorModifyConfirm_ = new ViewCollectorModifyConfirm_(newCollector, oldCollector, viewCollector, viewMain);
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

        private void textBoxId_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}