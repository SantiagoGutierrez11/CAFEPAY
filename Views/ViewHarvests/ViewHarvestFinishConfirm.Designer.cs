namespace CAFEPAY.Views.ViewHarvest
{
    partial class ViewHarvestFinishConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLote = new System.Windows.Forms.Label();
            this.lbNombreDeLote = new System.Windows.Forms.Label();
            this.lbNumeroDeCosecha = new System.Windows.Forms.Label();
            this.lbPrecioPorKilo = new System.Windows.Forms.Label();
            this.lbFechaInicio = new System.Windows.Forms.Label();
            this.lbFechaDeCierre = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.textBoxIdPlot = new System.Windows.Forms.TextBox();
            this.textBoxPlotName = new System.Windows.Forms.TextBox();
            this.textBoxIdHarvest = new System.Windows.Forms.TextBox();
            this.textBoxPricePerKilo = new System.Windows.Forms.TextBox();
            this.textBoxStartDate = new System.Windows.Forms.TextBox();
            this.textBoxEndDate = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbLote
            // 
            this.lbLote.AutoSize = true;
            this.lbLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLote.Location = new System.Drawing.Point(53, 19);
            this.lbLote.Name = "lbLote";
            this.lbLote.Size = new System.Drawing.Size(32, 13);
            this.lbLote.TabIndex = 1;
            this.lbLote.Text = "Lote";
            // 
            // lbNombreDeLote
            // 
            this.lbNombreDeLote.AutoSize = true;
            this.lbNombreDeLote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreDeLote.Location = new System.Drawing.Point(53, 70);
            this.lbNombreDeLote.Name = "lbNombreDeLote";
            this.lbNombreDeLote.Size = new System.Drawing.Size(93, 13);
            this.lbNombreDeLote.TabIndex = 3;
            this.lbNombreDeLote.Text = "Nombre de lote";
            // 
            // lbNumeroDeCosecha
            // 
            this.lbNumeroDeCosecha.AutoSize = true;
            this.lbNumeroDeCosecha.Location = new System.Drawing.Point(53, 123);
            this.lbNumeroDeCosecha.Name = "lbNumeroDeCosecha";
            this.lbNumeroDeCosecha.Size = new System.Drawing.Size(103, 13);
            this.lbNumeroDeCosecha.TabIndex = 5;
            this.lbNumeroDeCosecha.Text = "Numero de cosecha";
            // 
            // lbPrecioPorKilo
            // 
            this.lbPrecioPorKilo.AutoSize = true;
            this.lbPrecioPorKilo.Location = new System.Drawing.Point(53, 178);
            this.lbPrecioPorKilo.Name = "lbPrecioPorKilo";
            this.lbPrecioPorKilo.Size = new System.Drawing.Size(74, 13);
            this.lbPrecioPorKilo.TabIndex = 7;
            this.lbPrecioPorKilo.Text = "Precio por kilo";
            // 
            // lbFechaInicio
            // 
            this.lbFechaInicio.AutoSize = true;
            this.lbFechaInicio.Location = new System.Drawing.Point(53, 230);
            this.lbFechaInicio.Name = "lbFechaInicio";
            this.lbFechaInicio.Size = new System.Drawing.Size(65, 13);
            this.lbFechaInicio.TabIndex = 9;
            this.lbFechaInicio.Text = "Fecha Inicio";
            // 
            // lbFechaDeCierre
            // 
            this.lbFechaDeCierre.AutoSize = true;
            this.lbFechaDeCierre.Location = new System.Drawing.Point(53, 284);
            this.lbFechaDeCierre.Name = "lbFechaDeCierre";
            this.lbFechaDeCierre.Size = new System.Drawing.Size(81, 13);
            this.lbFechaDeCierre.TabIndex = 11;
            this.lbFechaDeCierre.Text = "Fecha de cierre";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(52, 358);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 14;
            this.btnConfirm.Text = "Confirmar";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(215, 358);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 15;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // textBoxIdPlot
            // 
            this.textBoxIdPlot.Location = new System.Drawing.Point(56, 41);
            this.textBoxIdPlot.Name = "textBoxIdPlot";
            this.textBoxIdPlot.ReadOnly = true;
            this.textBoxIdPlot.Size = new System.Drawing.Size(201, 20);
            this.textBoxIdPlot.TabIndex = 16;
            // 
            // textBoxPlotName
            // 
            this.textBoxPlotName.Location = new System.Drawing.Point(56, 86);
            this.textBoxPlotName.Name = "textBoxPlotName";
            this.textBoxPlotName.ReadOnly = true;
            this.textBoxPlotName.Size = new System.Drawing.Size(201, 20);
            this.textBoxPlotName.TabIndex = 17;
            // 
            // textBoxIdHarvest
            // 
            this.textBoxIdHarvest.Location = new System.Drawing.Point(54, 142);
            this.textBoxIdHarvest.Name = "textBoxIdHarvest";
            this.textBoxIdHarvest.ReadOnly = true;
            this.textBoxIdHarvest.Size = new System.Drawing.Size(201, 20);
            this.textBoxIdHarvest.TabIndex = 18;
            // 
            // textBoxPricePerKilo
            // 
            this.textBoxPricePerKilo.Location = new System.Drawing.Point(56, 194);
            this.textBoxPricePerKilo.Name = "textBoxPricePerKilo";
            this.textBoxPricePerKilo.ReadOnly = true;
            this.textBoxPricePerKilo.Size = new System.Drawing.Size(201, 20);
            this.textBoxPricePerKilo.TabIndex = 19;
            // 
            // textBoxStartDate
            // 
            this.textBoxStartDate.Location = new System.Drawing.Point(56, 246);
            this.textBoxStartDate.Name = "textBoxStartDate";
            this.textBoxStartDate.ReadOnly = true;
            this.textBoxStartDate.Size = new System.Drawing.Size(201, 20);
            this.textBoxStartDate.TabIndex = 20;
            // 
            // textBoxEndDate
            // 
            this.textBoxEndDate.Location = new System.Drawing.Point(56, 300);
            this.textBoxEndDate.Name = "textBoxEndDate";
            this.textBoxEndDate.ReadOnly = true;
            this.textBoxEndDate.Size = new System.Drawing.Size(201, 20);
            this.textBoxEndDate.TabIndex = 21;
            // 
            // ViewHarvestFinishConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 418);
            this.Controls.Add(this.textBoxEndDate);
            this.Controls.Add(this.textBoxStartDate);
            this.Controls.Add(this.textBoxPricePerKilo);
            this.Controls.Add(this.textBoxIdHarvest);
            this.Controls.Add(this.textBoxPlotName);
            this.Controls.Add(this.textBoxIdPlot);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbFechaDeCierre);
            this.Controls.Add(this.lbFechaInicio);
            this.Controls.Add(this.lbPrecioPorKilo);
            this.Controls.Add(this.lbNumeroDeCosecha);
            this.Controls.Add(this.lbNombreDeLote);
            this.Controls.Add(this.lbLote);
            this.Name = "ViewHarvestFinishConfirm";
            this.Text = "ViewHarvestFinishConfirm";
            this.Load += new System.EventHandler(this.ViewHarvestFinishConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLote;
        private System.Windows.Forms.Label lbNombreDeLote;
        private System.Windows.Forms.Label lbNumeroDeCosecha;
        private System.Windows.Forms.Label lbPrecioPorKilo;
        private System.Windows.Forms.Label lbFechaInicio;
        private System.Windows.Forms.Label lbFechaDeCierre;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.TextBox textBoxIdPlot;
        private System.Windows.Forms.TextBox textBoxPlotName;
        private System.Windows.Forms.TextBox textBoxIdHarvest;
        private System.Windows.Forms.TextBox textBoxPricePerKilo;
        private System.Windows.Forms.TextBox textBoxStartDate;
        private System.Windows.Forms.TextBox textBoxEndDate;
    }
}