namespace CAFEPAY.Views.ViewPayment
{
    partial class ViewPaymentConsult
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
            this.textBoxConsult = new System.Windows.Forms.TextBox();
            this.dgCollectors = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnConsult = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgCollectors)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(108, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Consultar por cedula / id de trabajador";
            // 
            // textBoxConsult
            // 
            this.textBoxConsult.Location = new System.Drawing.Point(302, 83);
            this.textBoxConsult.Name = "textBoxConsult";
            this.textBoxConsult.Size = new System.Drawing.Size(283, 20);
            this.textBoxConsult.TabIndex = 1;
            this.textBoxConsult.TextChanged += new System.EventHandler(this.textBoxConsult_TextChanged);
            // 
            // dgCollectors
            // 
            this.dgCollectors.AllowUserToAddRows = false;
            this.dgCollectors.AllowUserToDeleteRows = false;
            this.dgCollectors.AllowUserToResizeColumns = false;
            this.dgCollectors.AllowUserToResizeRows = false;
            this.dgCollectors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCollectors.Location = new System.Drawing.Point(105, 119);
            this.dgCollectors.MultiSelect = false;
            this.dgCollectors.Name = "dgCollectors";
            this.dgCollectors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCollectors.Size = new System.Drawing.Size(595, 181);
            this.dgCollectors.TabIndex = 2;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(34, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Regresar";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnConsult
            // 
            this.btnConsult.Location = new System.Drawing.Point(270, 317);
            this.btnConsult.Name = "btnConsult";
            this.btnConsult.Size = new System.Drawing.Size(254, 55);
            this.btnConsult.TabIndex = 4;
            this.btnConsult.Text = "Consultar";
            this.btnConsult.UseVisualStyleBackColor = true;
            this.btnConsult.Click += new System.EventHandler(this.btnConsult_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(297, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Consultar Pagos";
            // 
            // ViewPaymentConsult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnConsult);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgCollectors);
            this.Controls.Add(this.textBoxConsult);
            this.Controls.Add(this.label1);
            this.Name = "ViewPaymentConsult";
            this.Text = "ViewPaymentConsult";
            this.Load += new System.EventHandler(this.ViewPaymentConsult_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCollectors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxConsult;
        private System.Windows.Forms.DataGridView dgCollectors;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Label label2;
    }
}