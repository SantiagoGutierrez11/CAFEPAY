namespace CAFEPAY.Views.ViewCollector
{
    partial class ViewCollector
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
            this.dgCollector = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelet = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgCollector)).BeginInit();
            this.SuspendLayout();
            // 
            // dgCollector
            // 
            this.dgCollector.AllowUserToAddRows = false;
            this.dgCollector.AllowUserToDeleteRows = false;
            this.dgCollector.AllowUserToResizeColumns = false;
            this.dgCollector.AllowUserToResizeRows = false;
            this.dgCollector.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCollector.Location = new System.Drawing.Point(49, 88);
            this.dgCollector.MultiSelect = false;
            this.dgCollector.Name = "dgCollector";
            this.dgCollector.ReadOnly = true;
            this.dgCollector.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCollector.Size = new System.Drawing.Size(690, 235);
            this.dgCollector.TabIndex = 0;
            this.dgCollector.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCollector_CellContentClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(49, 26);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(168, 39);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelet
            // 
            this.btnDelet.Location = new System.Drawing.Point(571, 26);
            this.btnDelet.Name = "btnDelet";
            this.btnDelet.Size = new System.Drawing.Size(168, 39);
            this.btnDelet.TabIndex = 2;
            this.btnDelet.Text = "Eliminar";
            this.btnDelet.UseVisualStyleBackColor = true;
            this.btnDelet.Click += new System.EventHandler(this.btnDelet_Click);
            // 
            // ViewCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 350);
            this.Controls.Add(this.btnDelet);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgCollector);
            this.Name = "ViewCollector";
            this.Text = "Recolector";
            this.Load += new System.EventHandler(this.ViewCollector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCollector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCollector;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelet;
    }
}