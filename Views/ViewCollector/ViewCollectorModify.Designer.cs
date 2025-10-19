namespace CAFEPAY.Views.ViewCollector
{
    partial class ViewCollectorModify
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
            this.lbWkCode = new System.Windows.Forms.Label();
            this.lbId = new System.Windows.Forms.Label();
            this.lbFirstName = new System.Windows.Forms.Label();
            this.lbLastName = new System.Windows.Forms.Label();
            this.lbPhone = new System.Windows.Forms.Label();
            this.lbStatus = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.lbWorkerCode = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbWkCode
            // 
            this.lbWkCode.AutoSize = true;
            this.lbWkCode.Location = new System.Drawing.Point(44, 55);
            this.lbWkCode.Name = "lbWkCode";
            this.lbWkCode.Size = new System.Drawing.Size(71, 13);
            this.lbWkCode.TabIndex = 0;
            this.lbWkCode.Text = "Id Recolector";
            // 
            // lbId
            // 
            this.lbId.AutoSize = true;
            this.lbId.Location = new System.Drawing.Point(44, 89);
            this.lbId.Name = "lbId";
            this.lbId.Size = new System.Drawing.Size(40, 13);
            this.lbId.TabIndex = 1;
            this.lbId.Text = "Cedula";
            // 
            // lbFirstName
            // 
            this.lbFirstName.AutoSize = true;
            this.lbFirstName.Location = new System.Drawing.Point(44, 123);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(49, 13);
            this.lbFirstName.TabIndex = 2;
            this.lbFirstName.Text = "Nombres";
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(44, 157);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(49, 13);
            this.lbLastName.TabIndex = 3;
            this.lbLastName.Text = "Apellidos";
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Location = new System.Drawing.Point(44, 191);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(49, 13);
            this.lbPhone.TabIndex = 4;
            this.lbPhone.Text = "Telefono";
            this.lbPhone.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Location = new System.Drawing.Point(49, 226);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(40, 13);
            this.lbStatus.TabIndex = 5;
            this.lbStatus.Text = "Estado";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(144, 82);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(193, 20);
            this.textBoxId.TabIndex = 7;
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(144, 117);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(193, 20);
            this.textBoxFirstName.TabIndex = 8;
            this.textBoxFirstName.TextChanged += new System.EventHandler(this.textBoxFirstName_TextChanged);
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(144, 150);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(193, 20);
            this.textBoxLastName.TabIndex = 9;
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(144, 188);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(193, 20);
            this.textBoxPhone.TabIndex = 10;
            // 
            // cmbStatus
            // 
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(144, 220);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(193, 21);
            this.cmbStatus.TabIndex = 11;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(52, 282);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 12;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(262, 282);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 13;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // lbWorkerCode
            // 
            this.lbWorkerCode.AutoSize = true;
            this.lbWorkerCode.Location = new System.Drawing.Point(141, 55);
            this.lbWorkerCode.Name = "lbWorkerCode";
            this.lbWorkerCode.Size = new System.Drawing.Size(52, 13);
            this.lbWorkerCode.TabIndex = 14;
            this.lbWorkerCode.Text = "---------------";
            // 
            // ViewCollectorModify
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 337);
            this.Controls.Add(this.lbWorkerCode);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.textBoxPhone);
            this.Controls.Add(this.textBoxLastName);
            this.Controls.Add(this.textBoxFirstName);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.lbPhone);
            this.Controls.Add(this.lbLastName);
            this.Controls.Add(this.lbFirstName);
            this.Controls.Add(this.lbId);
            this.Controls.Add(this.lbWkCode);
            this.Name = "ViewCollectorModify";
            this.Text = "Modificar Recolector";
            this.Load += new System.EventHandler(this.ViewCollectorModify__Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbWkCode;
        private System.Windows.Forms.Label lbId;
        private System.Windows.Forms.Label lbFirstName;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Label lbWorkerCode;
    }
}