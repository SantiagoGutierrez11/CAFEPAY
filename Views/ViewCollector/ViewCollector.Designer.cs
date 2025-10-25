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
            this.btnModify = new System.Windows.Forms.Button();
            this.lbCollectors = new System.Windows.Forms.Label();
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
            this.dgCollector.Location = new System.Drawing.Point(81, 127);
            this.dgCollector.Margin = new System.Windows.Forms.Padding(4);
            this.dgCollector.MultiSelect = false;
            this.dgCollector.Name = "dgCollector";
            this.dgCollector.ReadOnly = true;
            this.dgCollector.RowHeadersWidth = 51;
            this.dgCollector.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCollector.Size = new System.Drawing.Size(861, 289);
            this.dgCollector.TabIndex = 0;
            this.dgCollector.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCollector_CellContentClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(81, 71);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(224, 48);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnModify
            // 
            this.btnModify.Location = new System.Drawing.Point(719, 71);
            this.btnModify.Margin = new System.Windows.Forms.Padding(4);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(224, 48);
            this.btnModify.TabIndex = 2;
            this.btnModify.Text = "Modificar";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // lbCollectors
            // 
            this.lbCollectors.AutoSize = true;
            this.lbCollectors.Location = new System.Drawing.Point(83, 44);
            this.lbCollectors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCollectors.Name = "lbCollectors";
            this.lbCollectors.Size = new System.Drawing.Size(0, 16);
            this.lbCollectors.TabIndex = 3;
            // 
            // ViewCollector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 431);
            this.Controls.Add(this.lbCollectors);
            this.Controls.Add(this.btnModify);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgCollector);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewCollector";
            this.Text = "Recolectores";
            this.Load += new System.EventHandler(this.ViewCollector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgCollector)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCollector;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Label lbCollectors;
    }
}