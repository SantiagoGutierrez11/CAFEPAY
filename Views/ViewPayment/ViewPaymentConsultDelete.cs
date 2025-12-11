using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CAFEPAY.Views.ViewOrigin;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewPaymentConsultDelete : Form
    {
        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);
        private Color greenColor = Color.FromArgb(34, 139, 34);   // Verde para botones

        // Variables de datos
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;
        private List<Payment> listPayments;
        private List<PaymentDTO> listPaymentsDTO;

        // 🔥 AGREGAR: Referencia al ViewMenuPayment
        private Form viewMenuPayment;

        // Controles dinámicos
        private TextBox txtSearchDynamic;
        private Button btnConsultDynamic;
        private Button btnBackDynamic;
        private Button btnHomeDynamic;
        private DataGridView dgvCollectorsDynamic;

        // 🔥 CONSTRUCTOR MODIFICADO: Recibir ViewMenuPayment como parámetro
        public ViewPaymentConsultDelete(Form _viewMenuPayment)
        {
            this.viewMenuPayment = _viewMenuPayment;
            InitializeFormWithoutFlicker();
        }

        // 🔥 CONSTRUCTOR ALTERNATIVO para compatibilidad
        public ViewPaymentConsultDelete() : this(null)
        {
        }

        private void InitializeFormWithoutFlicker()
        {
            // 🔥 DOBLE BUFFER para eliminar parpadeo
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.DoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            // 🔥 CARGAR PRIMERO LOS DATOS (antes de mostrar nada)
            LoadDataBeforeShowing();

            // 🔥 LUEGO CREAR LA INTERFAZ CON LOS DATOS YA CARGADOS
            InitializeDynamicControls();

            this.WindowState = FormWindowState.Maximized;
        }

        private void LoadDataBeforeShowing()
        {
            try
            {
                // 🔥 CARGAR DATOS ANTES de crear la interfaz
                listCollector = AppServices.CollectorServices.query.execute();
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                // Procesar datos para mostrar estado como texto
                foreach (var collector in listDTOCollector)
                {
                    collector.statusText = collector.status == 1 ? "Activo" : "Inactivo";
                }
            }
            catch (Exception ex)
            {
                // Guardar el error para mostrarlo después
                MessageBox.Show($"Error al cargar los recolectores: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                listDTOCollector = new List<CollectorDTO>(); // Lista vacía si hay error
            }
        }

        private void InitializeDynamicControls()
        {
            // 🔥 SUSPENDER TODO el layout mientras se crea
            this.SuspendLayout();

            try
            {
                ApplyExactFigmaDesign();
                ConfigureDataGridView();

                // 🔥 YA TENEMOS LOS DATOS, CONFIGURAR EL DATAGRIDVIEW INMEDIATAMENTE
                SetupDataGridViewWithData();
            }
            finally
            {
                // 🔥 REANUDAR layout - TODO listo para mostrar
                this.ResumeLayout(true);
                this.PerformLayout();
            }
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Consultar Pagos";

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10)
            };

            // Panel del logo
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 350,
                BackColor = Color.Transparent,
                Height = 70,
                Padding = new Padding(10, 0, 0, 0)
            };

            // 🖼️ CARGAR IMAGEN DESDE CARPETA RESOURCES
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");

                if (File.Exists(imagePath))
                {
                    PictureBox logoPicture = new PictureBox();
                    logoPicture.Image = Image.FromFile(imagePath);
                    logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
                    logoPicture.Size = new Size(320, 70);
                    logoPicture.Location = new Point(5, 5);
                    logoPicture.Cursor = Cursors.Hand;

                    ToolTip toolTip = new ToolTip();
                    toolTip.SetToolTip(logoPicture, "CAFICAUCA - Cooperativa de Caficultores del Cauca");

                    logoPanel.Controls.Add(logoPicture);
                }
                else
                {
                    CreateSimulatedLogo(logoPanel);
                }
            }
            catch (Exception)
            {
                CreateSimulatedLogo(logoPanel);
            }

            // 🏠 BOTÓN HOME (esquina superior derecha)
            var homeButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(topHeaderPanel.Width - 60, 25),
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
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            };

            topHeaderPanel.Controls.Add(homeButton);
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL - "CONSULTAR PAGOS"
            var titleContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(0, 30, 0, 0)
            };

            // Rectángulo azul exterior
            var blueOuterPanel = new Panel
            {
                Size = new Size(500, 70),
                Location = new Point((this.Width - 500) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            // Rectángulo blanco interior
            var whiteInnerPanel = new Panel
            {
                Size = new Size(490, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            // Label del título
            var mainTitleLabel = new Label
            {
                Text = "CONSULTAR PAGOS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 🔍 PANEL DE BÚSQUEDA
            var searchPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Label "Consultar por cédula / id de trabajador"
            var lblSearch = new Label
            {
                Text = "Consultar por cédula / id de trabajador",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(40, 25),
                AutoSize = true
            };

            // TextBox de búsqueda CON PLACEHOLDER MANUAL
            txtSearchDynamic = new TextBox
            {
                Location = new Point(350, 20),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                Text = "Ingrese ID trabajador o cédula..."
            };

            // Configurar comportamiento de placeholder
            txtSearchDynamic.ForeColor = Color.Gray;
            txtSearchDynamic.GotFocus += (s, ev) =>
            {
                if (txtSearchDynamic.Text == "Ingrese ID trabajador o cédula...")
                {
                    txtSearchDynamic.Text = "";
                    txtSearchDynamic.ForeColor = Color.Black;
                }
            };
            txtSearchDynamic.LostFocus += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearchDynamic.Text))
                {
                    txtSearchDynamic.Text = "Ingrese ID trabajador o cédula...";
                    txtSearchDynamic.ForeColor = Color.Gray;
                }
            };
            txtSearchDynamic.TextChanged += txtSearch_TextChanged;

            searchPanel.Controls.Add(lblSearch);
            searchPanel.Controls.Add(txtSearchDynamic);

            // 📊 PANEL DE DATOS (DataGridView) - CON DATOS YA CARGADOS
            var dataPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // 🟦 PANEL CON BORDE AZUL para el DataGridView
            var dataGridContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBlueColor,
                Padding = new Padding(2),
                Margin = new Padding(0, 0, 0, 10)
            };

            // Configurar DataGridView dinámico
            dgvCollectorsDynamic = new DataGridView();
            dgvCollectorsDynamic.Dock = DockStyle.Fill;

            dataGridContainerPanel.Controls.Add(dgvCollectorsDynamic);
            dataPanel.Controls.Add(dataGridContainerPanel);

            // 🔘 PANEL DE BOTONES INFERIOR
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 25, 40, 25)
            };

            // Botón Consultar (CENTRADO)
            btnConsultDynamic = new Button();
            btnConsultDynamic.FlatStyle = FlatStyle.Flat;
            btnConsultDynamic.BackColor = darkBlueColor;
            btnConsultDynamic.ForeColor = whiteColor;
            btnConsultDynamic.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnConsultDynamic.Text = "Consultar";
            btnConsultDynamic.Size = new Size(200, 50);
            btnConsultDynamic.Location = new Point(buttonPanel.Width / 2 - 220, 35);
            btnConsultDynamic.Anchor = AnchorStyles.None;
            btnConsultDynamic.Cursor = Cursors.Hand;
            btnConsultDynamic.FlatAppearance.BorderSize = 0;
            btnConsultDynamic.Click += btnConsult_Click;
            ApplyRoundedCorners(btnConsultDynamic, 10);

            // Botón Regresar (CENTRADO) - 🔥 MODIFICADO para regresar a ViewMenuPayment
            btnBackDynamic = new Button();
            btnBackDynamic.FlatStyle = FlatStyle.Flat;
            btnBackDynamic.BackColor = darkGrayColor;
            btnBackDynamic.ForeColor = whiteColor;
            btnBackDynamic.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBackDynamic.Text = "← Regresar";
            btnBackDynamic.Size = new Size(180, 50);
            btnBackDynamic.Location = new Point(buttonPanel.Width / 2 + 20, 35);
            btnBackDynamic.Anchor = AnchorStyles.None;
            btnBackDynamic.Cursor = Cursors.Hand;
            btnBackDynamic.FlatAppearance.BorderSize = 0;
            btnBackDynamic.Click += btnBack_Click;
            ApplyRoundedCorners(btnBackDynamic, 10);

            buttonPanel.Controls.Add(btnConsultDynamic);
            buttonPanel.Controls.Add(btnBackDynamic);

            // 📋 BREADCRUMB (pie de página inferior izquierda)
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 10, 40, 10)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / pagos / consultar",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO EN ORDEN CORRECTO
            this.Controls.Add(dataPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(searchPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Manejo de redimensionamiento
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnConsultDynamic.Location = new Point(buttonPanel.Width / 2 - 220, 35);
                btnBackDynamic.Location = new Point(buttonPanel.Width / 2 + 20, 35);
            };
        }

        private void CreateSimulatedLogo(Panel logoPanel)
        {
            var simulatedLogoPanel = new Panel
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle
            };

            var logoText = new Label
            {
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nPAGOS",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(15, 8),
                AutoSize = false,
                Size = new Size(290, 55),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var redLine = new Panel
            {
                Size = new Size(4, 40),
                Location = new Point(8, 15),
                BackColor = redColor
            };

            simulatedLogoPanel.Controls.Add(logoText);
            simulatedLogoPanel.Controls.Add(redLine);
            logoPanel.Controls.Add(simulatedLogoPanel);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void ConfigureDataGridView()
        {
            // 🔥 DOBLE BUFFER para DataGridView (elimina parpadeo)
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dgvCollectorsDynamic, new object[] { true });

            dgvCollectorsDynamic.BorderStyle = BorderStyle.None;
            dgvCollectorsDynamic.BackgroundColor = whiteColor;
            dgvCollectorsDynamic.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCollectorsDynamic.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCollectorsDynamic.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCollectorsDynamic.RowHeadersVisible = false;

            dgvCollectorsDynamic.AllowUserToAddRows = false;
            dgvCollectorsDynamic.AllowUserToDeleteRows = false;
            dgvCollectorsDynamic.AllowUserToResizeRows = false;

            dgvCollectorsDynamic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCollectorsDynamic.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCollectorsDynamic.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCollectorsDynamic.EnableHeadersVisualStyles = false;
            dgvCollectorsDynamic.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCollectorsDynamic.MultiSelect = false;
            dgvCollectorsDynamic.ReadOnly = true;

            // Estilo de encabezados
            dgvCollectorsDynamic.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgvCollectorsDynamic.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgvCollectorsDynamic.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCollectorsDynamic.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectorsDynamic.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgvCollectorsDynamic.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgvCollectorsDynamic.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCollectorsDynamic.DefaultCellStyle.BackColor = whiteColor;
            dgvCollectorsDynamic.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvCollectorsDynamic.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectorsDynamic.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgvCollectorsDynamic.RowTemplate.Height = 45;
        }

        private void SetupDataGridViewWithData()
        {
            // 🔥 SUSPENDER DataGridView mientras se configura
            dgvCollectorsDynamic.SuspendLayout();

            try
            {
                // Configurar columnas manualmente
                dgvCollectorsDynamic.AutoGenerateColumns = false;
                dgvCollectorsDynamic.Columns.Clear();

                // Agregar columnas según el diseño
                AddColumn("workerCode", "ID TRABAJADOR", 150);
                AddColumn("id", "CÉDULA", 120);
                AddColumn("firstName", "NOMBRES", 180);
                AddColumn("lastName", "APELLIDOS", 180);
                AddColumn("phone", "TELÉFONO", 150);

                // Columna de estado
                var colEstado = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "statusText",
                    HeaderText = "ESTADO",
                    Width = 120,
                    Name = "colEstado"
                };
                dgvCollectorsDynamic.Columns.Add(colEstado);

                // 🔥 ASIGNAR LOS DATOS YA CARGADOS AL DATAGRIDVIEW
                var bindingList = new BindingList<CollectorDTO>(listDTOCollector);
                dgvCollectorsDynamic.DataSource = bindingList;

                // Limpiar selección inicial
                dgvCollectorsDynamic.ClearSelection();
            }
            finally
            {
                // 🔥 REANUDAR DataGridView - TODO LISTO
                dgvCollectorsDynamic.ResumeLayout();
            }
        }

        // 🔹 MÉTODO AUXILIAR PARA AGREGAR COLUMNAS
        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgvCollectorsDynamic.Columns.Add(column);
        }

        // 🔍 EVENTO DE BÚSQUEDA
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Si es el texto del placeholder, no buscar
            if (txtSearchDynamic.Text == "Ingrese ID trabajador o cédula...")
            {
                var bindingList = new BindingList<CollectorDTO>(listDTOCollector);
                dgvCollectorsDynamic.DataSource = bindingList;
                dgvCollectorsDynamic.ClearSelection();
                return;
            }

            try
            {
                string searchText = txtSearchDynamic.Text.Trim().ToLower();

                // Si el texto está vacío, mostrar todos los registros
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    var bindingList = new BindingList<CollectorDTO>(listDTOCollector);
                    dgvCollectorsDynamic.DataSource = bindingList;
                    dgvCollectorsDynamic.ClearSelection();
                    return;
                }

                // Filtrar por ID TRABAJADOR o CÉDULA o NOMBRES
                var filteredList = listDTOCollector.Where(c =>
                    (c.workerCode != null && c.workerCode.ToLower().Contains(searchText)) ||
                    (c.id != null && c.id.ToString().ToLower().Contains(searchText)) ||
                    (c.firstName != null && c.firstName.ToLower().Contains(searchText)) ||
                    (c.lastName != null && c.lastName.ToLower().Contains(searchText))
                ).ToList();

                // Actualizar el DataGridView con los resultados filtrados
                var bindingListFiltered = new BindingList<CollectorDTO>(filteredList);
                dgvCollectorsDynamic.DataSource = bindingListFiltered;
                dgvCollectorsDynamic.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        // 🔘 EVENTO BOTÓN CONSULTAR
        private void btnConsult_Click(object sender, EventArgs e)
        {
            if (dgvCollectorsDynamic.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un recolector para consultar sus pagos.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener el CollectorDTO de la fila seleccionada
                DataGridViewRow selectedRow = dgvCollectorsDynamic.SelectedRows[0];
                if (selectedRow.DataBoundItem is CollectorDTO collector)
                {
                    // Validar que el workerCode no sea nulo
                    if (string.IsNullOrEmpty(collector.workerCode))
                    {
                        MessageBox.Show("El recolector seleccionado no tiene un código de trabajador válido.",
                                      "Datos incompletos",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return;
                    }

                    // Consultar los pagos del recolector
                    listPayments = AppServices.PaymentServices.queryByWorkerCode.execute(collector.workerCode);
                    listPaymentsDTO = PaymentMaper.ToDTOList(listPayments);

                    // Validar si hay pagos
                    if (listPayments == null || listPayments.Count == 0)
                    {
                        MessageBox.Show($"No se encontraron pagos para el recolector {collector.firstName} {collector.lastName}.",
                                      "Sin resultados",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        GoToWorkerPaymentsImmediately(collector, listPaymentsDTO);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información del recolector seleccionado.",
                                  "Error de datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los pagos: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void GoToWorkerPaymentsImmediately(CollectorDTO collector, List<PaymentDTO> payments)
        {
            // 🔥 PASAR this como referencia para que pueda regresar
            ViewPaymentConsultDeleteWorkerPayments viewPaymentConsultWorkerPayments =
                new ViewPaymentConsultDeleteWorkerPayments(collector, payments);
            viewPaymentConsultWorkerPayments.Owner = this; // 🔥 IMPORTANTE: Pasar this, no this.Owner
            this.Hide();
            viewPaymentConsultWorkerPayments.Show();
        }

        private void GoToHomeImmediately()
        {
            // 🔥 CAMBIO INMEDIATO SIN ANIMACIONES - VA A ViewMain
            var viewMain = new ViewOrigin.ViewMain();
            viewMain.Show();
            this.Close();
        }

        // 🔙 EVENTO BOTÓN REGRESAR - MODIFICADO para regresar a ViewMenuPayment
        private void btnBack_Click(object sender, EventArgs e)
        {
            GoBackToViewMenuPayment();
        }

        private void GoBackToViewMenuPayment()
        {
            try
            {
                // 🔥 PRIMERO intentar usar viewMenuPayment si existe
                if (this.viewMenuPayment != null && !this.viewMenuPayment.IsDisposed)
                {
                    this.viewMenuPayment.Show();
                    this.viewMenuPayment.WindowState = FormWindowState.Maximized;
                    this.Close();
                }
                // 🔥 SEGUNDO intentar usar Owner si viewMenuPayment no está
                else if (this.Owner != null && !this.Owner.IsDisposed)
                {
                    this.Owner.Show();
                    this.Owner.WindowState = FormWindowState.Maximized;
                    this.Close();
                }
                // 🔥 TERCERO crear nuevo ViewMenuPayment
                else
                {
                    var menuPayment = new ViewMenuPayment();
                    menuPayment.WindowState = FormWindowState.Maximized;
                    menuPayment.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al regresar: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fallback: ir al menú principal
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            }
        }

        // 📋 MÉTODO PARA ELIMINAR ERROR DEL DISEÑADOR
        private void ViewPaymentConsultDelete_Load(object sender, EventArgs e)
        {
            // Método vacío para compatibilidad
        }
    }
}