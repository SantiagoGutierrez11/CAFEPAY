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
            ((System.ComponentModel.ISupportInitialize)(this.dgHarvest)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(36, 42);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(240, 58);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(804, 42);
            this.btnFinish.Margin = new System.Windows.Forms.Padding(4);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(189, 58);
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
            this.dgHarvest.Location = new System.Drawing.Point(36, 107);
            this.dgHarvest.Margin = new System.Windows.Forms.Padding(4);
            this.dgHarvest.MultiSelect = false;
            this.dgHarvest.Name = "dgHarvest";
            this.dgHarvest.ReadOnly = true;
            this.dgHarvest.RowHeadersWidth = 51;
            this.dgHarvest.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgHarvest.Size = new System.Drawing.Size(957, 223);
            this.dgHarvest.TabIndex = 2;
            this.dgHarvest.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgHarvest_CellContentClick);
            // 
            // btnAssociate
            // 
            this.btnAssociate.Location = new System.Drawing.Point(426, 42);
            this.btnAssociate.Name = "btnAssociate";
            this.btnAssociate.Size = new System.Drawing.Size(215, 59);
            this.btnAssociate.TabIndex = 3;
            this.btnAssociate.Text = "Asociar recolector";
            this.btnAssociate.UseVisualStyleBackColor = true;
            this.btnAssociate.Click += new System.EventHandler(this.btnAssociate_Click);

            // 
            // ViewHarvest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.btnAssociate);
            this.Controls.Add(this.dgHarvest);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnAdd);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewHarvest";
            this.Text = "ViewHarvest";
            this.Load += new System.EventHandler(this.ViewHarvest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgHarvest)).EndInit();
            this.ResumeLayout(false);

        }

        private void dgHarvest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.DataGridView dgHarvest;
        private Button btnAssociate;
    }
}