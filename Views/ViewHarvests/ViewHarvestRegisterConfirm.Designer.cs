namespace CAFEPAY.Views.ViewHarvest
{
    partial class ViewHarvestRegisterConfirm
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
            this.lbIdPlot = new System.Windows.Forms.Label();
            this.lbPrecioPorKilo = new System.Windows.Forms.Label();
            this.lbPricePerKilo = new System.Windows.Forms.Label();
            this.lbFechaDeInicio = new System.Windows.Forms.Label();
            this.lbStartDate = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbLote
            // 
            this.lbLote.AutoSize = true;
            this.lbLote.Location = new System.Drawing.Point(61, 81);
            this.lbLote.Name = "lbLote";
            this.lbLote.Size = new System.Drawing.Size(28, 13);
            this.lbLote.TabIndex = 0;
            this.lbLote.Text = "Lote";
            // 
            // lbIdPlot
            // 
            this.lbIdPlot.AutoSize = true;
            this.lbIdPlot.Location = new System.Drawing.Point(61, 103);
            this.lbIdPlot.Name = "lbIdPlot";
            this.lbIdPlot.Size = new System.Drawing.Size(73, 13);
            this.lbIdPlot.TabIndex = 1;
            this.lbIdPlot.Text = "----------------------";
            // 
            // lbPrecioPorKilo
            // 
            this.lbPrecioPorKilo.AutoSize = true;
            this.lbPrecioPorKilo.Location = new System.Drawing.Point(61, 134);
            this.lbPrecioPorKilo.Name = "lbPrecioPorKilo";
            this.lbPrecioPorKilo.Size = new System.Drawing.Size(70, 13);
            this.lbPrecioPorKilo.TabIndex = 6;
            this.lbPrecioPorKilo.Text = "PrecioPorKilo";
            // 
            // lbPricePerKilo
            // 
            this.lbPricePerKilo.AutoSize = true;
            this.lbPricePerKilo.Location = new System.Drawing.Point(64, 160);
            this.lbPricePerKilo.Name = "lbPricePerKilo";
            this.lbPricePerKilo.Size = new System.Drawing.Size(76, 13);
            this.lbPricePerKilo.TabIndex = 7;
            this.lbPricePerKilo.Text = "-----------------------";
            // 
            // lbFechaDeInicio
            // 
            this.lbFechaDeInicio.AutoSize = true;
            this.lbFechaDeInicio.Location = new System.Drawing.Point(64, 189);
            this.lbFechaDeInicio.Name = "lbFechaDeInicio";
            this.lbFechaDeInicio.Size = new System.Drawing.Size(79, 13);
            this.lbFechaDeInicio.TabIndex = 8;
            this.lbFechaDeInicio.Text = "Fecha de inicio";
            // 
            // lbStartDate
            // 
            this.lbStartDate.AutoSize = true;
            this.lbStartDate.Location = new System.Drawing.Point(67, 215);
            this.lbStartDate.Name = "lbStartDate";
            this.lbStartDate.Size = new System.Drawing.Size(73, 13);
            this.lbStartDate.TabIndex = 9;
            this.lbStartDate.Text = "----------------------";
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(26, 307);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(117, 48);
            this.btnConfirm.TabIndex = 10;
            this.btnConfirm.Text = "Confirmar";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(197, 307);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(117, 48);
            this.btnDecline.TabIndex = 11;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // ViewHarvestRegisterConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 388);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbStartDate);
            this.Controls.Add(this.lbFechaDeInicio);
            this.Controls.Add(this.lbPricePerKilo);
            this.Controls.Add(this.lbPrecioPorKilo);
            this.Controls.Add(this.lbIdPlot);
            this.Controls.Add(this.lbLote);
            this.Name = "ViewHarvestRegisterConfirm";
            this.Text = "ViewRegisterConfirm";
            this.Load += new System.EventHandler(this.ViewRegisterConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLote;
        private System.Windows.Forms.Label lbIdPlot;
        private System.Windows.Forms.Label lbPrecioPorKilo;
        private System.Windows.Forms.Label lbPricePerKilo;
        private System.Windows.Forms.Label lbFechaDeInicio;
        private System.Windows.Forms.Label lbStartDate;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnDecline;
    }
}