namespace CAFEPAY.Views.ViewCollector
{
    partial class ViewCollectorRegister
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
            this.txtBoxWorkerCode = new System.Windows.Forms.TextBox();
            this.txtBoxId = new System.Windows.Forms.TextBox();
            this.txtBoxFirstName = new System.Windows.Forms.TextBox();
            this.txtBoxLastName = new System.Windows.Forms.TextBox();
            this.lbWorkerCode = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lbCollectorId = new System.Windows.Forms.Label();
            this.txtBoxPhone = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBoxWorkerCode
            // 
            this.txtBoxWorkerCode.Location = new System.Drawing.Point(195, 54);
            this.txtBoxWorkerCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxWorkerCode.Name = "txtBoxWorkerCode";
            this.txtBoxWorkerCode.Size = new System.Drawing.Size(240, 22);
            this.txtBoxWorkerCode.TabIndex = 13;
            // 
            // txtBoxId
            // 
            this.txtBoxId.Location = new System.Drawing.Point(193, 95);
            this.txtBoxId.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(241, 22);
            this.txtBoxId.TabIndex = 14;
            // 
            // txtBoxFirstName
            // 
            this.txtBoxFirstName.Location = new System.Drawing.Point(192, 137);
            this.txtBoxFirstName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxFirstName.Name = "txtBoxFirstName";
            this.txtBoxFirstName.Size = new System.Drawing.Size(244, 22);
            this.txtBoxFirstName.TabIndex = 15;
            // 
            // txtBoxLastName
            // 
            this.txtBoxLastName.Location = new System.Drawing.Point(193, 180);
            this.txtBoxLastName.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxLastName.Name = "txtBoxLastName";
            this.txtBoxLastName.Size = new System.Drawing.Size(241, 22);
            this.txtBoxLastName.TabIndex = 17;
            // 
            // lbWorkerCode
            // 
            this.lbWorkerCode.AutoSize = true;
            this.lbWorkerCode.Location = new System.Drawing.Point(71, 57);
            this.lbWorkerCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbWorkerCode.Name = "lbWorkerCode";
            this.lbWorkerCode.Size = new System.Drawing.Size(0, 16);
            this.lbWorkerCode.TabIndex = 25;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(192, 260);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(244, 24);
            this.cmbStatus.TabIndex = 21;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged_1);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(337, 316);
            this.btnDecline.Margin = new System.Windows.Forms.Padding(4);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(100, 28);
            this.btnDecline.TabIndex = 23;
            this.btnDecline.Text = "Cancelar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click_1);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(37, 316);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(4);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(100, 28);
            this.btnAccept.TabIndex = 22;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click_1);
            // 
            // lbCollectorId
            // 
            this.lbCollectorId.AutoSize = true;
            this.lbCollectorId.Location = new System.Drawing.Point(112, 98);
            this.lbCollectorId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCollectorId.Name = "lbCollectorId";
            this.lbCollectorId.Size = new System.Drawing.Size(0, 16);
            this.lbCollectorId.TabIndex = 16;
            this.lbCollectorId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxPhone
            // 
            this.txtBoxPhone.Location = new System.Drawing.Point(193, 215);
            this.txtBoxPhone.Margin = new System.Windows.Forms.Padding(4);
            this.txtBoxPhone.Name = "txtBoxPhone";
            this.txtBoxPhone.Size = new System.Drawing.Size(243, 22);
            this.txtBoxPhone.TabIndex = 20;
            // 
            // ViewCollectorRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 396);
            this.Controls.Add(this.txtBoxWorkerCode);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.txtBoxFirstName);
            this.Controls.Add(this.txtBoxLastName);
            this.Controls.Add(this.lbWorkerCode);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbCollectorId);
            this.Controls.Add(this.txtBoxPhone);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ViewCollectorRegister";
            this.Text = "Registrar Recolector";
            this.Load += new System.EventHandler(this.ViewCollectorModify_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxWorkerCode;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.TextBox txtBoxFirstName;
        private System.Windows.Forms.TextBox txtBoxLastName;
        private System.Windows.Forms.Label lbWorkerCode;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lbCollectorId;
        private System.Windows.Forms.TextBox txtBoxPhone;
    }
}