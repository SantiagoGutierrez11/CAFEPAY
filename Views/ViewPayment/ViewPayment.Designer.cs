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
            this.dgvPayment = new System.Windows.Forms.DataGridView();
            this.btnCalculateTotalPayment = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cm = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbHarvest = new System.Windows.Forms.Label();
            this.lbCollector = new System.Windows.Forms.Label();
<<<<<<< HEAD
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).BeginInit();
=======
            this.cmbCollectors = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTotalAmounToPaid = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCollects)).BeginInit();
>>>>>>> Santiago
            this.SuspendLayout();
            // 
            // dgvPayment
            // 
            this.dgvPayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayment.Location = new System.Drawing.Point(78, 118);
            this.dgvPayment.Name = "dgvPayment";
            this.dgvPayment.Size = new System.Drawing.Size(603, 290);
            this.dgvPayment.TabIndex = 0;
            // 
            // btnCalculateTotalPayment
            // 
            this.btnCalculateTotalPayment.Location = new System.Drawing.Point(78, 76);
            this.btnCalculateTotalPayment.Name = "btnCalculateTotalPayment";
            this.btnCalculateTotalPayment.Size = new System.Drawing.Size(149, 36);
            this.btnCalculateTotalPayment.TabIndex = 1;
            this.btnCalculateTotalPayment.Text = "Cacular pago total";
            this.btnCalculateTotalPayment.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(233, 76);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Calcular Pago Seleccionado";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cm
            // 
            this.cm.FormattingEnabled = true;
            this.cm.Location = new System.Drawing.Point(425, 86);
            this.cm.Name = "cm";
            this.cm.Size = new System.Drawing.Size(121, 21);
            this.cm.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(563, 87);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(117, 20);
            this.textBox1.TabIndex = 4;
            // 
            // lbHarvest
            // 
            this.lbHarvest.AutoSize = true;
            this.lbHarvest.Location = new System.Drawing.Point(422, 70);
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
<<<<<<< HEAD
=======
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Monto total a pagar";
            // 
            // textBoxTotalAmounToPaid
            // 
            this.textBoxTotalAmounToPaid.Location = new System.Drawing.Point(202, 434);
            this.textBoxTotalAmounToPaid.Name = "textBoxTotalAmounToPaid";
            this.textBoxTotalAmounToPaid.Size = new System.Drawing.Size(261, 20);
            this.textBoxTotalAmounToPaid.TabIndex = 10;
            // 
>>>>>>> Santiago
            // ViewPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(800, 450);
=======
            this.ClientSize = new System.Drawing.Size(803, 496);
            this.Controls.Add(this.textBoxTotalAmounToPaid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbCollectors);
>>>>>>> Santiago
            this.Controls.Add(this.lbCollector);
            this.Controls.Add(this.lbHarvest);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cm);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCalculateTotalPayment);
            this.Controls.Add(this.dgvPayment);
            this.Name = "ViewPayment";
            this.Text = "ViewPayment";
            this.Load += new System.EventHandler(this.ViewPayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPayment;
        private System.Windows.Forms.Button btnCalculateTotalPayment;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cm;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbHarvest;
        private System.Windows.Forms.Label lbCollector;
<<<<<<< HEAD
=======
        private System.Windows.Forms.ComboBox cmbCollectors;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTotalAmounToPaid;
>>>>>>> Santiago
    }
}