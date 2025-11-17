namespace CAFEPAY.Views.ViewPayment
{
    partial class ViewMenuPayment
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
            this.btnConsult = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConsult
            // 
            this.btnConsult.Location = new System.Drawing.Point(348, 87);
            this.btnConsult.Name = "btnConsult";
            this.btnConsult.Size = new System.Drawing.Size(207, 53);
            this.btnConsult.TabIndex = 1;
            this.btnConsult.Text = "Consultar";
            this.btnConsult.UseVisualStyleBackColor = true;
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(50, 87);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(207, 53);
            this.btnPayment.TabIndex = 2;
            this.btnPayment.Text = "Pagar";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(247, 208);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(109, 42);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Regresar";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ViewMenuPayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 280);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.btnConsult);
            this.Name = "ViewMenuPayment";
            this.Text = "ViewMainPayment";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConsult;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnBack;
    }
}