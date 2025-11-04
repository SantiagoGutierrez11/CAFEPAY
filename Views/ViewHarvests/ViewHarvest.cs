using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using CAFEPAY.Views.ViewCollector;
using CAFEPAY.Views.ViewHarvests;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvest : Form
    {
        private string _lastSortProp;
        private bool _sortAsc = true;
        List<Harvest> listHarvest = new List<Harvest>();
        List<HarvestDTO> listHarvestDTO = new List<HarvestDTO>();

        public object PlotMaper { get; private set; }

        public ViewHarvest()
        {
            InitializeComponent();
            loadHarvests();
            dgHarvest.ColumnHeaderMouseClick += dgHarvest_ColumnHeaderMouseClick;

        }

        private void ViewHarvest_Load(object sender, EventArgs e)
        {

        }
        private void dgHarvest_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dgHarvest.Columns[e.ColumnIndex];
            var prop = col.DataPropertyName;                 // ej: "startDate", "endDate", etc.
            if (string.IsNullOrWhiteSpace(prop)) return;

            _sortAsc = (_lastSortProp == prop) ? !_sortAsc : true;
            _lastSortProp = prop;

            Func<HarvestDTO, object> key = x => x?.GetType().GetProperty(prop)?.GetValue(x, null);
            var sorted = _sortAsc
                ? listHarvestDTO.OrderBy(key).ToList()
                : listHarvestDTO.OrderByDescending(key).ToList();

            dgHarvest.DataSource = null;                     // refresco simple
            dgHarvest.DataSource = sorted;
        }
        public void loadHarvests()
        {
            try
            {
                // 1) Traer cosechas
                listHarvest = AppServices.HarvestServices.query.execute();
                listHarvestDTO = HarvestMaper.ToDTOList(listHarvest);

                // 2) Traer lotes y mapear a diccionario id->nombre
                var plots = AppServices.PlotServices.query.execute();                   // usa tu repo
                var plotsDTO = PlotMapper.ToDTOList(plots);
                var plotNameById = plotsDTO.ToDictionary(p => p.idPlot, p => p.name);

                // 3) Completar nombre de lote en cada DTO
                foreach (var h in listHarvestDTO)
                    h.plotName = plotNameById.TryGetValue(h.idPlot, out var name) ? name : "(desconocido)";

                // 4) Orden: activas primero (opcional)
                listHarvestDTO = listHarvestDTO
                    .OrderByDescending(h => h.status == 1 && h.endDate == null)
                    .ThenByDescending(h => h.startDate)
                    .ToList();

                // 5) Bind al grid + nueva columna
                dgHarvest.AutoGenerateColumns = false;
                dgHarvest.Columns.Clear();
                AddColumn("idPlot", "Parcela Id", 110);
                AddColumn("plotName", "Nombre de lote", 180);
                AddColumn("id", "Cosecha Id", 110);
                AddColumn("startDate", "Fecha Inicio", 120);
                AddColumn("endDate", "Fecha Fin", 120);
                AddColumn("pricePerKilo", "Precio por Kilo", 130);
                AddColumn("statusText", "Estado", 100);

                dgHarvest.DataSource = listHarvestDTO;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading harvests: " + ex.Message);
            }
        }

        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgHarvest.Columns.Add(column);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewHarvestRegister viewHarvestRegister = new ViewHarvestRegister();
            viewHarvestRegister.Owner = this;
            viewHarvestRegister.Show();
            this.Hide();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            {
                // VALIDACIONES COMPLETAS DEL CÓDIGO ORIGINAL
                if (dgHarvest.CurrentCell == null)
                {
                    MessageBox.Show("Por favor, seleccione un recolector para modificar.",
                                  "Selección requerida",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                int rowSelected = dgHarvest.CurrentCell.RowIndex;

                if (rowSelected < 0 || rowSelected >= listHarvestDTO.Count)
                {
                    MessageBox.Show("La selección no es válida.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }

                var selectedHarvest = listHarvestDTO[rowSelected];
                if (selectedHarvest == null)
                {
                    MessageBox.Show("El recolector seleccionado no es válido.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }
                if (selectedHarvest.status == 2 || selectedHarvest.endDate != null)
                {
                    MessageBox.Show("La cosecha seleccionada ya está finalizada.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }
                if ( DateTime.Today == selectedHarvest.startDate)
                {
                    MessageBox.Show("La cosecha no puede finalizarse en la misma fecha de inicio.",
                                  "Error",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                    return;
                }
                Plot plot = AppServices.PlotServices.queryById.execute(selectedHarvest.idPlot);
                PlotDTO plotDTO = new PlotDTO
                {
                    idPlot = plot.idPlot.idPlotValue,
                    idOwner = plot.idOwner.idPlotOwnerValue,
                    name = plot.name.plotNameValue,
                    status = plot.status.statusValue,
                    statusText = plot.status.statusValue == 1 ? "ACTIVO" :
                                 plot.status.statusValue == 2 ? "INACTIVO" :
                                 "DESCONOCIDO"
                };
                ViewHarvestFinishConfirm viewHarvestFinishConfirm = new ViewHarvestFinishConfirm(plotDTO, selectedHarvest);
                viewHarvestFinishConfirm.Owner = this;
                viewHarvestFinishConfirm.Show();
                this.Hide();
            }
        }

        private void btnAssociate_Click(object sender, EventArgs e)
        {
            // Validar selección en el DataGridView
            if (dgHarvest.CurrentCell == null)
            {
                MessageBox.Show("Por favor, seleccione una cosecha para asociar un recolector.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            int rowSelected = dgHarvest.CurrentCell.RowIndex;

            if (rowSelected < 0 || rowSelected >= listHarvestDTO.Count)
            {
                MessageBox.Show("La selección no es válida.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            // Obtener la cosecha seleccionada
            var selectedHarvest = listHarvestDTO[rowSelected];
            if (selectedHarvest == null)
            {
                MessageBox.Show("La cosecha seleccionada no es válida.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            // Validar que la cosecha esté activa
            if (selectedHarvest.status != 1 || selectedHarvest.endDate != null)
            {
                MessageBox.Show("Solo se pueden asociar recolectores a cosechas activas.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            // Obtener información del lote asociado
            Plot plot = AppServices.PlotServices.queryById.execute(selectedHarvest.idPlot);
            PlotDTO plotDTO = new PlotDTO
            {
                idPlot = plot.idPlot.idPlotValue,
                idOwner = plot.idOwner.idPlotOwnerValue,
                name = plot.name.plotNameValue,
                status = plot.status.statusValue,
                statusText = plot.status.statusValue == 1 ? "ACTIVO" :
                             plot.status.statusValue == 2 ? "INACTIVO" :
                             "DESCONOCIDO"
            };

            // Abrir la nueva vista para asociar recolectores
            ViewHarvestAssociateCollector viewAssociate = new ViewHarvestAssociateCollector(plotDTO, selectedHarvest);
            viewAssociate.Owner = this;
            viewAssociate.Show();
            this.Hide();
        }

    }
}