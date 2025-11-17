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
            this.lbPrecioPorKilo = new System.Windows.Forms.Label();
            this.lbFechaDeInicio = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.textBoxIdPlot = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPlotName = new System.Windows.Forms.TextBox();
            this.textBoxPricePerKilo = new System.Windows.Forms.TextBox();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbLote
            // 
            this.lbLote.AutoSize = true;
            this.lbLote.Location = new System.Drawing.Point(61, 91);
            this.lbLote.Name = "lbLote";
            this.lbLote.Size = new System.Drawing.Size(40, 13);
            this.lbLote.TabIndex = 0;
            this.lbLote.Text = "Id Lote";
            // 
            // lbPrecioPorKilo
            // 
            this.lbPrecioPorKilo.AutoSize = true;
            this.lbPrecioPorKilo.Location = new System.Drawing.Point(61, 177);
            this.lbPrecioPorKilo.Name = "lbPrecioPorKilo";
            this.lbPrecioPorKilo.Size = new System.Drawing.Size(70, 13);
            this.lbPrecioPorKilo.TabIndex = 6;
            this.lbPrecioPorKilo.Text = "PrecioPorKilo";
            // 
            // lbFechaDeInicio
            // 
            this.lbFechaDeInicio.AutoSize = true;
            this.lbFechaDeInicio.Location = new System.Drawing.Point(61, 228);
            this.lbFechaDeInicio.Name = "lbFechaDeInicio";
            this.lbFechaDeInicio.Size = new System.Drawing.Size(79, 13);
            this.lbFechaDeInicio.TabIndex = 8;
            this.lbFechaDeInicio.Text = "Fecha de inicio";
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
            // textBoxIdPlot
            // 
            this.textBoxIdPlot.Location = new System.Drawing.Point(64, 107);
            this.textBoxIdPlot.Name = "textBoxIdPlot";
            this.textBoxIdPlot.Size = new System.Drawing.Size(100, 20);
            this.textBoxIdPlot.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Nombre de lote";
            // 
            // textBoxPlotName
            // 
            this.textBoxPlotName.Location = new System.Drawing.Point(64, 68);
            this.textBoxPlotName.Name = "textBoxPlotName";
            this.textBoxPlotName.Size = new System.Drawing.Size(100, 20);
            this.textBoxPlotName.TabIndex = 14;
            // 
            // textBoxPricePerKilo
            // 
            this.textBoxPricePerKilo.Location = new System.Drawing.Point(64, 193);
            this.textBoxPricePerKilo.Name = "textBoxPricePerKilo";
            this.textBoxPricePerKilo.Size = new System.Drawing.Size(100, 20);
            this.textBoxPricePerKilo.TabIndex = 15;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(64, 244);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(100, 20);
            this.textBoxDate.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 18);
            this.label3.TabIndex = 18;
            this.label3.Text = "Información del lote";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 18);
            this.label2.TabIndex = 19;
            this.label2.Text = "Información de la cosecha";
            // 
            // ViewHarvestRegisterConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 388);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.textBoxPricePerKilo);
            this.Controls.Add(this.textBoxPlotName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxIdPlot);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lbFechaDeInicio);
            this.Controls.Add(this.lbPrecioPorKilo);
            this.Controls.Add(this.lbLote);
            this.Name = "ViewHarvestRegisterConfirm";
            this.Text = "ViewRegisterConfirm";
            this.Load += new System.EventHandler(this.ViewRegisterConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLote;
        private System.Windows.Forms.Label lbPrecioPorKilo;
        private System.Windows.Forms.Label lbFechaDeInicio;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.TextBox textBoxIdPlot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPlotName;
        private System.Windows.Forms.TextBox textBoxPricePerKilo;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}