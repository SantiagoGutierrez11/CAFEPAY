namespace CAFEPAY.Views.ViewPayment
{
    partial class ViewPayment
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
            this.dgvCollects = new System.Windows.Forms.DataGridView();
            this.btnCalculateTotalPayment = new System.Windows.Forms.Button();
            this.btnPaymentPartial = new System.Windows.Forms.Button();
            this.cmbHarvests = new System.Windows.Forms.ComboBox();
            this.lbHarvest = new System.Windows.Forms.Label();
            this.lbCollector = new System.Windows.Forms.Label();
            this.cmbCollectors = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollects)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCollects
            // 
            this.dgvCollects.AllowUserToAddRows = false;
            this.dgvCollects.AllowUserToDeleteRows = false;
            this.dgvCollects.AllowUserToOrderColumns = true;
            this.dgvCollects.AllowUserToResizeColumns = false;
            this.dgvCollects.AllowUserToResizeRows = false;
            this.dgvCollects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCollects.Location = new System.Drawing.Point(27, 118);
            this.dgvCollects.Name = "dgvCollects";
            this.dgvCollects.ReadOnly = true;
            this.dgvCollects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCollects.Size = new System.Drawing.Size(740, 290);
            this.dgvCollects.TabIndex = 0;
            // 
            // btnCalculateTotalPayment
            // 
            this.btnCalculateTotalPayment.Location = new System.Drawing.Point(34, 76);
            this.btnCalculateTotalPayment.Name = "btnCalculateTotalPayment";
            this.btnCalculateTotalPayment.Size = new System.Drawing.Size(146, 36);
            this.btnCalculateTotalPayment.TabIndex = 1;
            this.btnCalculateTotalPayment.Text = "Cacular pago total";
            this.btnCalculateTotalPayment.UseVisualStyleBackColor = true;
            this.btnCalculateTotalPayment.Click += new System.EventHandler(this.btnCalculateTotalPayment_Click);
            // 
            // btnPaymentPartial
            // 
            this.btnPaymentPartial.Location = new System.Drawing.Point(189, 76);
            this.btnPaymentPartial.Name = "btnPaymentPartial";
            this.btnPaymentPartial.Size = new System.Drawing.Size(146, 36);
            this.btnPaymentPartial.TabIndex = 2;
            this.btnPaymentPartial.Text = "Calcular Pago Seleccionado";
            this.btnPaymentPartial.UseVisualStyleBackColor = true;
            this.btnPaymentPartial.Click += new System.EventHandler(this.btnPaymentPartial_Click);
            // 
            // cmbHarvests
            // 
            this.cmbHarvests.FormattingEnabled = true;
            this.cmbHarvests.Location = new System.Drawing.Point(394, 86);
            this.cmbHarvests.Name = "cmbHarvests";
            this.cmbHarvests.Size = new System.Drawing.Size(148, 21);
            this.cmbHarvests.TabIndex = 3;
            this.cmbHarvests.SelectedIndexChanged += new System.EventHandler(this.cmbHarvests_SelectedIndexChanged);
            // 
            // lbHarvest
            // 
            this.lbHarvest.AutoSize = true;
            this.lbHarvest.Location = new System.Drawing.Point(391, 70);
            this.lbHarvest.Name = "lbHarvest";
            this.lbHarvest.Size = new System.Drawing.Size(49, 13);
            this.lbHarvest.TabIndex = 5;
            this.lbHarvest.Text = "Cosecha";
            // 
            // lbCollector
            // 
            this.lbCollector.AutoSize = true;
            this.lbCollector.Location = new System.Drawing.Point(560, 71);
            this.lbCollector.Name = "lbCollector";
            this.lbCollector.Size = new System.Drawing.Size(59, 13);
            this.lbCollector.TabIndex = 6;
            this.lbCollector.Text = "Recolector";
            // 
            // cmbCollectors
            // 
            this.cmbCollectors.FormattingEnabled = true;
            this.cmbCollectors.Location = new System.Drawing.Point(563, 87);
            this.cmbCollectors.Name = "cmbCollectors";
            this.cmbCollectors.Size = new System.Drawing.Size(164, 21);
            this.cmbCollectors.TabIndex = 7;
            this.cmbCollectors.SelectedIndexChanged += new System.EventHandler(this.cmbCollectors_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(34, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(146, 36);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Pagina Pricipal";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ViewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbCollectors);
            this.Controls.Add(this.lbCollector);
            this.Controls.Add(this.lbHarvest);
            this.Controls.Add(this.cmbHarvests);
            this.Controls.Add(this.btnPaymentPartial);
            this.Controls.Add(this.btnCalculateTotalPayment);
            this.Controls.Add(this.dgvCollects);
            this.Name = "ViewPayment";
            this.Text = "ViewPayment";
            this.Load += new System.EventHandler(this.ViewPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollects)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCollects;
        private System.Windows.Forms.Button btnCalculateTotalPayment;
        private System.Windows.Forms.Button btnPaymentPartial;
        private System.Windows.Forms.ComboBox cmbHarvests;
        private System.Windows.Forms.Label lbHarvest;
        private System.Windows.Forms.Label lbCollector;
        private System.Windows.Forms.ComboBox cmbCollectors;
        private System.Windows.Forms.Button btnBack;
    }
}