namespace CAFEPAY.Views.ViewCollector
{
    partial class ViewCollectorModifyId
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
            this.lbText = new System.Windows.Forms.Label();
            this.textBoxId = new System.Windows.Forms.MaskedTextBox();
            this.lbTextConfirm = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbText.Location = new System.Drawing.Point(124, 9);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(327, 29);
            this.lbText.TabIndex = 0;
            this.lbText.Text = "Esta modificando la cedula";
            // 
            // textBoxId
            // 
            this.textBoxId.Location = new System.Drawing.Point(129, 112);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(322, 20);
            this.textBoxId.TabIndex = 1;
            this.textBoxId.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.textBoxId_MaskInputRejected);
            // 
            // lbTextConfirm
            // 
            this.lbTextConfirm.AutoSize = true;
            this.lbTextConfirm.Location = new System.Drawing.Point(220, 96);
            this.lbTextConfirm.Name = "lbTextConfirm";
            this.lbTextConfirm.Size = new System.Drawing.Size(132, 13);
            this.lbTextConfirm.TabIndex = 2;
            this.lbTextConfirm.Text = "Confirme cedula a cambiar";
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(129, 161);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 3;
            this.btnAccept.Text = "Aceptar";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(376, 161);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(75, 23);
            this.btnDecline.TabIndex = 4;
            this.btnDecline.Text = "Rechazar";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // ViewCollectorModifyId
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 187);
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.lbTextConfirm);
            this.Controls.Add(this.textBoxId);
            this.Controls.Add(this.lbText);
            this.Name = "ViewCollectorModifyId";
            this.Text = "ViewCollectorModifyId";
            this.Load += new System.EventHandler(this.ViewCollectorModifyId_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.MaskedTextBox textBoxId;
        private System.Windows.Forms.Label lbTextConfirm;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnDecline;
    }
}