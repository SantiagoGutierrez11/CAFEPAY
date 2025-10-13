namespace CAFEPAY.Views.ViewCollector
{
    partial class ViewCollectorDetail
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
            this.grupBoxCollectorRegister = new System.Windows.Forms.GroupBox();
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
            this.grupBoxCollectorRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupBoxCollectorRegister
            // 
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxWorkerCode);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxId);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxFirstName);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxLastName);
            this.grupBoxCollectorRegister.Controls.Add(this.lbLastName);
            this.grupBoxCollectorRegister.Controls.Add(this.lbWorkerCode);
            this.grupBoxCollectorRegister.Controls.Add(this.lbStatus);
            this.grupBoxCollectorRegister.Controls.Add(this.cmbStatus);
            this.grupBoxCollectorRegister.Controls.Add(this.btnDecline);
            this.grupBoxCollectorRegister.Controls.Add(this.btnAccept);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollecorName);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollectorPhone);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollectorId);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxPhone);
            this.grupBoxCollectorRegister.Location = new System.Drawing.Point(12, 32);
            this.grupBoxCollectorRegister.Name = "grupBoxCollectorRegister";
            this.grupBoxCollectorRegister.Size = new System.Drawing.Size(358, 317);
            this.grupBoxCollectorRegister.TabIndex = 0;
            this.grupBoxCollectorRegister.TabStop = false;
            this.grupBoxCollectorRegister.Text = "Registrar Recolector";
            this.grupBoxCollectorRegister.Enter += new System.EventHandler(this.grupBoxCollectorRegister_Enter);
            // 
            // txtBoxWorkerCode
            // 
            this.txtBoxWorkerCode.Location = new System.Drawing.Point(139, 56);
            this.txtBoxWorkerCode.Name = "txtBoxWorkerCode";
            this.txtBoxWorkerCode.Size = new System.Drawing.Size(181, 20);
            this.txtBoxWorkerCode.TabIndex = 1;
            this.txtBoxWorkerCode.TextChanged += new System.EventHandler(this.textBoxWorkerCode_TextChanged);
            // 
            // txtBoxId
            // 
            this.txtBoxId.Location = new System.Drawing.Point(138, 89);
            this.txtBoxId.Name = "txtBoxId";
            this.txtBoxId.Size = new System.Drawing.Size(182, 20);
            this.txtBoxId.TabIndex = 2;
            this.txtBoxId.TextChanged += new System.EventHandler(this.textBoxId_TextChanged);
            // 
            // txtBoxFirstName
            // 
            this.txtBoxFirstName.Location = new System.Drawing.Point(137, 123);
            this.txtBoxFirstName.Name = "txtBoxFirstName";
            this.txtBoxFirstName.Size = new System.Drawing.Size(184, 20);
            this.txtBoxFirstName.TabIndex = 3;
            this.txtBoxFirstName.TextChanged += new System.EventHandler(this.textBoxFirstName_TextChanged);
            // 
            // txtBoxLastName
            // 
            this.txtBoxLastName.Location = new System.Drawing.Point(138, 158);
            this.txtBoxLastName.Name = "txtBoxLastName";
            this.txtBoxLastName.Size = new System.Drawing.Size(182, 20);
            this.txtBoxLastName.TabIndex = 4;
            this.txtBoxLastName.TextChanged += new System.EventHandler(this.txtBoxLastName_TextChanged_2);
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(68, 161);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(49, 13);
            this.lbLastName.TabIndex = 12;
            this.lbLastName.Text = "Apellidos";
            // 
            // lbWorkerCode
            // 
            this.lbWorkerCode.AutoSize = true;
            this.lbWorkerCode.Location = new System.Drawing.Point(46, 58);
            this.lbWorkerCode.Name = "lbWorkerCode";
            this.lbWorkerCode.Size = new System.Drawing.Size(71, 13);
            this.lbWorkerCode.TabIndex = 10;
            this.lbWorkerCode.Text = "Id Recolector";
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(77, 226);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(40, 13);
            this.lbStatus.TabIndex = 9;
            this.lbStatus.Text = "Estado";
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(137, 223);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(184, 21);
            this.cmbStatus.TabIndex = 6;
            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(this.cmbStatus_SelectedIndexChanged);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(246, 269);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 7;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(21, 269);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // lbCollecorName
            // 
            this.lbCollecorName.AutoSize = true;
            this.lbCollecorName.Location = new System.Drawing.Point(68, 126);
            this.lbCollecorName.Name = "lbCollecorName";
            this.lbCollecorName.Size = new System.Drawing.Size(49, 13);
            this.lbCollecorName.TabIndex = 5;
            this.lbCollecorName.Text = "Nombres";
            this.lbCollecorName.Click += new System.EventHandler(this.lbCollecorName_Click);
            // 
            // lbCollectorPhone
            // 
            this.lbCollectorPhone.AutoSize = true;
            this.lbCollectorPhone.Location = new System.Drawing.Point(68, 194);
            this.lbCollectorPhone.Name = "lbCollectorPhone";
            this.lbCollectorPhone.Size = new System.Drawing.Size(49, 13);
            this.lbCollectorPhone.TabIndex = 4;
            this.lbCollectorPhone.Text = "Telefono";
            this.lbCollectorPhone.Click += new System.EventHandler(this.lbCollectorPhone_Click);
            // 
            // lbCollectorId
            // 
            this.lbCollectorId.AutoSize = true;
            this.lbCollectorId.Location = new System.Drawing.Point(77, 92);
            this.lbCollectorId.Name = "lbCollectorId";
            this.lbCollectorId.Size = new System.Drawing.Size(40, 13);
            this.lbCollectorId.TabIndex = 3;
            this.lbCollectorId.Text = "Cédula";
            this.lbCollectorId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCollectorId.Click += new System.EventHandler(this.lbCollectorId_Click);
            // 
            // txtBoxPhone
            // 
            this.txtBoxPhone.Location = new System.Drawing.Point(138, 187);
            this.txtBoxPhone.Name = "txtBoxPhone";
            this.txtBoxPhone.Size = new System.Drawing.Size(183, 20);
            this.txtBoxPhone.TabIndex = 5;
            this.txtBoxPhone.TextChanged += new System.EventHandler(this.txtBoxPhone_TextChanged);
            // 
            // ViewCollectorDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 361);
            this.Controls.Add(this.grupBoxCollectorRegister);
            this.Name = "ViewCollectorDetail";
            this.Text = "Registrar Recolector";
            this.Load += new System.EventHandler(this.ViewCollectorDetail_Load);
            this.grupBoxCollectorRegister.ResumeLayout(false);
            this.grupBoxCollectorRegister.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grupBoxCollectorRegister;
        private System.Windows.Forms.TextBox txtBoxPhone;
        private System.Windows.Forms.Label lbCollectorId;
        private System.Windows.Forms.Label lbCollectorPhone;
        private System.Windows.Forms.Label lbCollecorName;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lbWorkerCode;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.TextBox txtBoxLastName;
        private System.Windows.Forms.TextBox txtBoxId;
        private System.Windows.Forms.TextBox txtBoxFirstName;
        private System.Windows.Forms.TextBox txtBoxWorkerCode;
    }
}