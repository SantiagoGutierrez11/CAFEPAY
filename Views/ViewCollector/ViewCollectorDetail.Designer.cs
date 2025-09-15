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
            this.lbCollecorName = new System.Windows.Forms.Label();
            this.lbCollectorPhone = new System.Windows.Forms.Label();
            this.lbCollectorId = new System.Windows.Forms.Label();
            this.txtBoxPhone = new System.Windows.Forms.TextBox();
            this.txtBoxCollectorName = new System.Windows.Forms.TextBox();
            this.txtBoxCollectorId = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.grupBoxCollectorRegister.SuspendLayout();
            this.SuspendLayout();
            // 
            // grupBoxCollectorRegister
            // 
            this.grupBoxCollectorRegister.Controls.Add(this.btnDecline);
            this.grupBoxCollectorRegister.Controls.Add(this.btnAccept);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollecorName);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollectorPhone);
            this.grupBoxCollectorRegister.Controls.Add(this.lbCollectorId);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxPhone);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxCollectorName);
            this.grupBoxCollectorRegister.Controls.Add(this.txtBoxCollectorId);
            this.grupBoxCollectorRegister.Location = new System.Drawing.Point(12, 41);
            this.grupBoxCollectorRegister.Name = "grupBoxCollectorRegister";
            this.grupBoxCollectorRegister.Size = new System.Drawing.Size(336, 277);
            this.grupBoxCollectorRegister.TabIndex = 0;
            this.grupBoxCollectorRegister.TabStop = false;
            this.grupBoxCollectorRegister.Text = "Registrar Recolector";
            this.grupBoxCollectorRegister.Enter += new System.EventHandler(this.grupBoxCollectorRegister_Enter);
            // 
            // lbCollecorName
            // 
            this.lbCollecorName.AutoSize = true;
            this.lbCollecorName.Location = new System.Drawing.Point(82, 109);
            this.lbCollecorName.Name = "lbCollecorName";
            this.lbCollecorName.Size = new System.Drawing.Size(35, 13);
            this.lbCollecorName.TabIndex = 5;
            this.lbCollecorName.Text = "Name";
            this.lbCollecorName.Click += new System.EventHandler(this.lbCollecorName_Click);
            // 
            // lbCollectorPhone
            // 
            this.lbCollectorPhone.AutoSize = true;
            this.lbCollectorPhone.Location = new System.Drawing.Point(68, 167);
            this.lbCollectorPhone.Name = "lbCollectorPhone";
            this.lbCollectorPhone.Size = new System.Drawing.Size(49, 13);
            this.lbCollectorPhone.TabIndex = 4;
            this.lbCollectorPhone.Text = "Telefono";
            this.lbCollectorPhone.Click += new System.EventHandler(this.lbCollectorPhone_Click);
            // 
            // lbCollectorId
            // 
            this.lbCollectorId.AutoSize = true;
            this.lbCollectorId.Location = new System.Drawing.Point(77, 54);
            this.lbCollectorId.Name = "lbCollectorId";
            this.lbCollectorId.Size = new System.Drawing.Size(40, 13);
            this.lbCollectorId.TabIndex = 3;
            this.lbCollectorId.Text = "Cédula";
            this.lbCollectorId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbCollectorId.Click += new System.EventHandler(this.lbCollectorId_Click);
            // 
            // txtBoxPhone
            // 
            this.txtBoxPhone.Location = new System.Drawing.Point(138, 160);
            this.txtBoxPhone.Name = "txtBoxPhone";
            this.txtBoxPhone.Size = new System.Drawing.Size(183, 20);
            this.txtBoxPhone.TabIndex = 2;
            // 
            // txtBoxCollectorName
            // 
            this.txtBoxCollectorName.Location = new System.Drawing.Point(137, 102);
            this.txtBoxCollectorName.Name = "txtBoxCollectorName";
            this.txtBoxCollectorName.Size = new System.Drawing.Size(184, 20);
            this.txtBoxCollectorName.TabIndex = 1;
            // 
            // txtBoxCollectorId
            // 
            this.txtBoxCollectorId.Location = new System.Drawing.Point(138, 49);
            this.txtBoxCollectorId.Name = "txtBoxCollectorId";
            this.txtBoxCollectorId.Size = new System.Drawing.Size(183, 20);
            this.txtBoxCollectorId.TabIndex = 0;
            this.txtBoxCollectorId.TextChanged += new System.EventHandler(this.txtBoxCollectorId_TextChanged);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(6, 224);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(246, 224);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 7;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // ViewCollectorDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 361);
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
        private System.Windows.Forms.TextBox txtBoxCollectorId;
        private System.Windows.Forms.TextBox txtBoxPhone;
        private System.Windows.Forms.TextBox txtBoxCollectorName;
        private System.Windows.Forms.Label lbCollectorId;
        private System.Windows.Forms.Label lbCollectorPhone;
        private System.Windows.Forms.Label lbCollecorName;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Button btnAccept;
    }
}