namespace CAFEPAY.Views.ViewHarvests
{
    partial class ViewHarvestAssociateCollector
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNombreLote = new System.Windows.Forms.TextBox();
            this.textBoxNumeroCosecha = new System.Windows.Forms.TextBox();
            this.textBoxPrecioPorKilo = new System.Windows.Forms.TextBox();
            this.textBoxFechaInicio = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(201, 585);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(255, 57);
            this.button1.TabIndex = 0;
            this.button1.Text = "Asociar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 283);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(584, 271);
            this.dataGridView1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre lote:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Numero de cosecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 206);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de inicio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Precio por kilo:";
            // 
            // textBoxNombreLote
            // 
            this.textBoxNombreLote.Location = new System.Drawing.Point(201, 93);
            this.textBoxNombreLote.Name = "textBoxNombreLote";
            this.textBoxNombreLote.Size = new System.Drawing.Size(255, 22);
            this.textBoxNombreLote.TabIndex = 6;
            // 
            // textBoxNumeroCosecha
            // 
            this.textBoxNumeroCosecha.Location = new System.Drawing.Point(201, 137);
            this.textBoxNumeroCosecha.Name = "textBoxNumeroCosecha";
            this.textBoxNumeroCosecha.Size = new System.Drawing.Size(255, 22);
            this.textBoxNumeroCosecha.TabIndex = 7;
            // 
            // textBoxPrecioPorKilo
            // 
            this.textBoxPrecioPorKilo.Location = new System.Drawing.Point(201, 181);
            this.textBoxPrecioPorKilo.Name = "textBoxPrecioPorKilo";
            this.textBoxPrecioPorKilo.Size = new System.Drawing.Size(255, 22);
            this.textBoxPrecioPorKilo.TabIndex = 8;
            // 
            // textBoxFechaInicio
            // 
            this.textBoxFechaInicio.Location = new System.Drawing.Point(201, 225);
            this.textBoxFechaInicio.Name = "textBoxFechaInicio";
            this.textBoxFechaInicio.Size = new System.Drawing.Size(255, 22);
            this.textBoxFechaInicio.TabIndex = 9;
            // 
            // ViewHarvestAssociateCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 687);
            this.Controls.Add(this.textBoxFechaInicio);
            this.Controls.Add(this.textBoxPrecioPorKilo);
            this.Controls.Add(this.textBoxNumeroCosecha);
            this.Controls.Add(this.textBoxNombreLote);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "ViewHarvestAssociateCollector";
            this.Text = "ViewHarvestAssociateCollector";
            this.Load += new System.EventHandler(this.ViewHarvestAssociateCollector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNombreLote;
        private System.Windows.Forms.TextBox textBoxNumeroCosecha;
        private System.Windows.Forms.TextBox textBoxPrecioPorKilo;
        private System.Windows.Forms.TextBox textBoxFechaInicio;
    }
}