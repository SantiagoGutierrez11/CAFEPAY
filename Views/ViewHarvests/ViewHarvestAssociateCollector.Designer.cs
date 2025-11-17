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
            this.btnAssociate = new System.Windows.Forms.Button();
            this.dgCollectors = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPlotName = new System.Windows.Forms.TextBox();
            this.textBoxIdHarvest = new System.Windows.Forms.TextBox();
            this.textBoxPricePerKilo = new System.Windows.Forms.TextBox();
            this.textBoxStartDate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.textBoxIdPlot = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCollectors)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAssociate
            // 
            this.btnAssociate.Location = new System.Drawing.Point(292, 457);
            this.btnAssociate.Margin = new System.Windows.Forms.Padding(2);
            this.btnAssociate.Name = "btnAssociate";
            this.btnAssociate.Size = new System.Drawing.Size(162, 46);
            this.btnAssociate.TabIndex = 0;
            this.btnAssociate.Text = "Asociar";
            this.btnAssociate.UseVisualStyleBackColor = true;
            this.btnAssociate.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgCollectors
            // 
            this.dgCollectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCollectors.Location = new System.Drawing.Point(20, 218);
            this.dgCollectors.Margin = new System.Windows.Forms.Padding(2);
            this.dgCollectors.Name = "dgCollectors";
            this.dgCollectors.RowHeadersWidth = 51;
            this.dgCollectors.RowTemplate.Height = 24;
            this.dgCollectors.Size = new System.Drawing.Size(682, 220);
            this.dgCollectors.TabIndex = 1;
            this.dgCollectors.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 109);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nombre lote:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 109);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Numero de cosecha:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(584, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de inicio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 109);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Precio por kilo:";
            // 
            // textBoxPlotName
            // 
            this.textBoxPlotName.Location = new System.Drawing.Point(20, 124);
            this.textBoxPlotName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPlotName.Name = "textBoxPlotName";
            this.textBoxPlotName.Size = new System.Drawing.Size(102, 20);
            this.textBoxPlotName.TabIndex = 6;
            this.textBoxPlotName.TextChanged += new System.EventHandler(this.textBoxNombreLote_TextChanged);
            // 
            // textBoxIdHarvest
            // 
            this.textBoxIdHarvest.Location = new System.Drawing.Point(441, 124);
            this.textBoxIdHarvest.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIdHarvest.Name = "textBoxIdHarvest";
            this.textBoxIdHarvest.Size = new System.Drawing.Size(120, 20);
            this.textBoxIdHarvest.TabIndex = 7;
            this.textBoxIdHarvest.TextChanged += new System.EventHandler(this.textBoxNumeroCosecha_TextChanged);
            // 
            // textBoxPricePerKilo
            // 
            this.textBoxPricePerKilo.Location = new System.Drawing.Point(586, 124);
            this.textBoxPricePerKilo.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPricePerKilo.Name = "textBoxPricePerKilo";
            this.textBoxPricePerKilo.Size = new System.Drawing.Size(120, 20);
            this.textBoxPricePerKilo.TabIndex = 8;
            // 
            // textBoxStartDate
            // 
            this.textBoxStartDate.Location = new System.Drawing.Point(276, 124);
            this.textBoxStartDate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStartDate.Name = "textBoxStartDate";
            this.textBoxStartDate.Size = new System.Drawing.Size(120, 20);
            this.textBoxStartDate.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(288, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Cosecha activa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(266, 26);
            this.label6.TabIndex = 11;
            this.label6.Text = "Seleccione recolectores";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(20, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 12;
            this.btnBack.Text = "Regresar";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // textBoxIdPlot
            // 
            this.textBoxIdPlot.Location = new System.Drawing.Point(146, 124);
            this.textBoxIdPlot.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxIdPlot.Name = "textBoxIdPlot";
            this.textBoxIdPlot.Size = new System.Drawing.Size(102, 20);
            this.textBoxIdPlot.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(145, 109);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Id lote:";
            // 
            // ViewHarvestAssociateCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 529);
            this.Controls.Add(this.textBoxIdPlot);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxStartDate);
            this.Controls.Add(this.textBoxPricePerKilo);
            this.Controls.Add(this.textBoxIdHarvest);
            this.Controls.Add(this.textBoxPlotName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgCollectors);
            this.Controls.Add(this.btnAssociate);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ViewHarvestAssociateCollector";
            this.Text = "ViewHarvestAssociateCollector";
            this.Load += new System.EventHandler(this.ViewHarvestAssociateCollector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCollectors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAssociate;
        private System.Windows.Forms.DataGridView dgCollectors;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPlotName;
        private System.Windows.Forms.TextBox textBoxIdHarvest;
        private System.Windows.Forms.TextBox textBoxPricePerKilo;
        private System.Windows.Forms.TextBox textBoxStartDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox textBoxIdPlot;
        private System.Windows.Forms.Label label7;
    }
}