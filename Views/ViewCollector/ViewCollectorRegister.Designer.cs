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
            this.lbLastName = new System.Windows.Forms.Label();
            this.lbWorkerCode = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.lbCollecorName = new System.Windows.Forms.Label();
            this.lbCollectorPhone = new System.Windows.Forms.Label();
            this.lbCollectorId = new System.Windows.Forms.Label();
            this.txtBoxPhone = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBoxWorkerCode
            // 
            this.txtBoxWorkerCode.Location = new System.Drawing.Point(146, 44);
            this.txtBoxWorkerCode.Name = "txtBoxWorkerCode";
            this.txtBoxWorkerCode.Size = new System.Drawing.Size(181, 20);
            this.txtBoxWorkerCode.TabIndex = 13;
            // 
            // txtBoxId
            // 
            this.txtBoxId.Location = new System.Drawing.Point(145, 77);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(182, 20);
            this.txtBoxId.TabIndex = 14;
            // 
            // txtBoxFirstName
            // 
            this.txtBoxFirstName.Location = new System.Drawing.Point(144, 111);
            this.txtBoxFirstName.Name = "txtBoxFirstName";
            this.txtBoxFirstName.Size = new System.Drawing.Size(184, 20);
            this.txtBoxFirstName.TabIndex = 15;
            // 
            // txtBoxLastName
            // 
            this.txtBoxLastName.Location = new System.Drawing.Point(145, 146);
            this.txtBoxLastName.Name = "txtBoxLastName";
            this.txtBoxLastName.Size = new System.Drawing.Size(182, 20);
            this.txtBoxLastName.TabIndex = 17;
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(75, 149);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(49, 13);
            this.lbLastName.TabIndex = 26;
            this.lbLastName.Text = "Apellidos";
            // 
            // lbWorkerCode
            // 
            this.lbWorkerCode.AutoSize = true;
            this.lbWorkerCode.Location = new System.Drawing.Point(53, 46);
            this.lbWorkerCode.Name = "lbWorkerCode";
            this.lbWorkerCode.Size = new System.Drawing.Size(71, 13);
            this.lbWorkerCode.TabIndex = 25;
            this.lbWorkerCode.Text = "Id Recolector";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(84, 214);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(40, 13);
            this.lbStatus.TabIndex = 24;
            this.lbStatus.Text = "Estado";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(144, 211);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(184, 21);
            this.cmbStatus.TabIndex = 21;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged_1);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(253, 257);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 23;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click_1);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(28, 257);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 22;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click_1);
            // 
            // lbCollecorName
            // 
            this.lbCollecorName.AutoSize = true;
            this.lbCollecorName.Location = new System.Drawing.Point(75, 114);
            this.lbCollecorName.Name = "lbCollecorName";
            this.lbCollecorName.Size = new System.Drawing.Size(49, 13);
            this.lbCollecorName.TabIndex = 19;
            this.lbCollecorName.Text = "Nombres";
            // 
            // lbCollectorPhone
            // 
            this.lbCollectorPhone.AutoSize = true;
            this.lbCollectorPhone.Location = new System.Drawing.Point(75, 182);
            this.lbCollectorPhone.Name = "lbCollectorPhone";
            this.lbCollectorPhone.Size = new System.Drawing.Size(49, 13);
            this.lbCollectorPhone.TabIndex = 18;
            this.lbCollectorPhone.Text = "Telefono";
            // 
            // lbCollectorId
            // 
            this.lbCollectorId.AutoSize = true;
            this.lbCollectorId.Location = new System.Drawing.Point(84, 80);
            this.lbCollectorId.Name = "lbCollectorId";
            this.lbCollectorId.Size = new System.Drawing.Size(40, 13);
            this.lbCollectorId.TabIndex = 16;
            this.lbCollectorId.Text = "Cédula";
            this.lbCollectorId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxPhone
            // 
            this.txtBoxPhone.Location = new System.Drawing.Point(145, 175);
            this.txtBoxPhone.Name = "txtBoxPhone";
            this.txtBoxPhone.Size = new System.Drawing.Size(183, 20);
            this.txtBoxPhone.TabIndex = 20;
            // 
            // ViewCollectorRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 322);
            this.Controls.Add(this.txtBoxWorkerCode);
            this.Controls.Add(this.txtBoxId);
            this.Controls.Add(this.txtBoxFirstName);
            this.Controls.Add(this.txtBoxLastName);
            this.Controls.Add(this.lbLastName);
            this.Controls.Add(this.lbWorkerCode);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbCollecorName);
            this.Controls.Add(this.lbCollectorPhone);
            this.Controls.Add(this.lbCollectorId);
            this.Controls.Add(this.txtBoxPhone);
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
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.Label lbWorkerCode;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Label lbCollecorName;
        private System.Windows.Forms.Label lbCollectorPhone;
        private System.Windows.Forms.Label lbCollectorId;
        private System.Windows.Forms.TextBox txtBoxPhone;
    }
}