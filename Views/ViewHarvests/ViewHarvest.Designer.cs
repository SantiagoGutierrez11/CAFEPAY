using System;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewHarvest
{
    partial class ViewHarvest
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.dgHarvest = new System.Windows.Forms.DataGridView();
            this.btnAssociate = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.Cosechas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgHarvest)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(30, 67);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(180, 47);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(606, 67);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(142, 47);
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Text = "Finalizar";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // dgHarvest
            // 
            this.dgHarvest.AllowUserToAddRows = false;
            this.dgHarvest.AllowUserToDeleteRows = false;
            this.dgHarvest.AllowUserToOrderColumns = true;
            this.dgHarvest.AllowUserToResizeColumns = false;
            this.dgHarvest.AllowUserToResizeRows = false;
            this.dgHarvest.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgHarvest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgHarvest.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgHarvest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHarvest.Location = new System.Drawing.Point(30, 120);
            this.dgHarvest.MultiSelect = false;
            this.dgHarvest.Name = "dgHarvest";
            this.dgHarvest.ReadOnly = true;
            this.dgHarvest.RowHeadersWidth = 51;
            this.dgHarvest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHarvest.Size = new System.Drawing.Size(718, 181);
            this.dgHarvest.TabIndex = 2;
            this.dgHarvest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgHarvest_CellContentClick);
            // 
            // btnAssociate
            // 
            this.btnAssociate.Location = new System.Drawing.Point(323, 67);
            this.btnAssociate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAssociate.Name = "btnAssociate";
            this.btnAssociate.Size = new System.Drawing.Size(161, 48);
            this.btnAssociate.TabIndex = 3;
            this.btnAssociate.Text = "Asociar recolector";
            this.btnAssociate.UseVisualStyleBackColor = true;
            this.btnAssociate.Click += new System.EventHandler(this.btnAssociate_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(30, 315);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(161, 48);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Regresar";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Cosechas
            // 
            this.Cosechas.AutoSize = true;
            this.Cosechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cosechas.Location = new System.Drawing.Point(318, 9);
            this.Cosechas.Name = "Cosechas";
            this.Cosechas.Size = new System.Drawing.Size(116, 25);
            this.Cosechas.TabIndex = 6;
            this.Cosechas.Text = "Cosechas";
            // 
            // ViewHarvest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Cosechas);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAssociate);
            this.Controls.Add(this.dgHarvest);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnAdd);
            this.Name = "ViewHarvest";
            this.Text = "ViewHarvest";
            this.Load += new System.EventHandler(this.ViewHarvest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHarvest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void dgHarvest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.DataGridView dgHarvest;
        private Button btnAssociate;
        private Button btnBack;
        private Label Cosechas;
    }
}