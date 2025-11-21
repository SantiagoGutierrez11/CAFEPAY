namespace CAFEPAY.Views.ViewHarvest
{
    partial class ViewHarvestRegister
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTmStartDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxPricePerKilo = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.cmbIdPlot = new System.Windows.Forms.ComboBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(93, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Lote";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fecha de inicio";
            // 
            // dtTmStartDate
            // 
            this.dtTmStartDate.Location = new System.Drawing.Point(96, 118);
            this.dtTmStartDate.MaxDate = new System.DateTime(2025, 10, 30, 0, 0, 0, 0);
            this.dtTmStartDate.MinDate = new System.DateTime(2025, 10, 30, 0, 0, 0, 0);
            this.dtTmStartDate.Name = "dtTmStartDate";
            this.dtTmStartDate.Size = new System.Drawing.Size(196, 20);
            this.dtTmStartDate.TabIndex = 3;
            this.dtTmStartDate.Value = new System.DateTime(2025, 10, 30, 0, 0, 0, 0);
            this.dtTmStartDate.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(93, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Precio Kilogramo";
            // 
            // textBoxPricePerKilo
            // 
            this.textBoxPricePerKilo.Location = new System.Drawing.Point(96, 176);
            this.textBoxPricePerKilo.Name = "textBoxPricePerKilo";
            this.textBoxPricePerKilo.Size = new System.Drawing.Size(196, 20);
            this.textBoxPricePerKilo.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(96, 223);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(89, 24);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(207, 224);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(85, 24);
            this.btnDecline.TabIndex = 7;
            this.btnDecline.Text = "Cancelar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // cmbIdPlot
            // 
            this.cmbIdPlot.FormattingEnabled = true;
            this.cmbIdPlot.Location = new System.Drawing.Point(96, 63);
            this.cmbIdPlot.Name = "cmbIdPlot";
            this.cmbIdPlot.Size = new System.Drawing.Size(196, 21);
            this.cmbIdPlot.TabIndex = 8;
            this.cmbIdPlot.SelectedIndexChanged += new System.EventHandler(this.cmbIdPlot_SelectedIndexChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 9;
            this.btnBack.Text = "Regresar";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ViewHarvestRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 279);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.cmbIdPlot);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textBoxPricePerKilo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtTmStartDate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ViewHarvestRegister";
            this.Text = "Registrar Cosecha";
            this.Load += new System.EventHandler(this.ViewHarvestRegister_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtTmStartDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPricePerKilo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.ComboBox cmbIdPlot;
        private System.Windows.Forms.Button btnBack;
    }
}