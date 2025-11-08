namespace CAFEPAY.Views.ViewOrigin
{
    partial class ViewMain
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
            this.btnCollectors = new System.Windows.Forms.Button();
            this.btnHarvests = new System.Windows.Forms.Button();
            this.btnCollects = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCollectors
            // 
            this.btnCollectors.Location = new System.Drawing.Point(16, 193);
            this.btnCollectors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCollectors.Name = "btnCollectors";
            this.btnCollectors.Size = new System.Drawing.Size(237, 76);
            this.btnCollectors.TabIndex = 0;
            this.btnCollectors.Text = "Recolectores";
            this.btnCollectors.UseVisualStyleBackColor = true;
            this.btnCollectors.Click += new System.EventHandler(this.btnCollectors_Click);
            // 
            // btnHarvests
            // 
            this.btnHarvests.Location = new System.Drawing.Point(273, 193);
            this.btnHarvests.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHarvests.Name = "btnHarvests";
            this.btnHarvests.Size = new System.Drawing.Size(237, 76);
            this.btnHarvests.TabIndex = 1;
            this.btnHarvests.Text = "Cosechas";
            this.btnHarvests.UseVisualStyleBackColor = true;
            this.btnHarvests.Click += new System.EventHandler(this.btnHarvests_Click);
            // 
            // btnCollects
            // 
            this.btnCollects.Location = new System.Drawing.Point(535, 193);
            this.btnCollects.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCollects.Name = "btnCollects";
            this.btnCollects.Size = new System.Drawing.Size(237, 76);
            this.btnCollects.TabIndex = 2;
            this.btnCollects.Text = "Recolectas";
            this.btnCollects.UseVisualStyleBackColor = true;
            this.btnCollects.Click += new System.EventHandler(this.btnCollects_Click);
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.btnCollects);
            this.Controls.Add(this.btnHarvests);
            this.Controls.Add(this.btnCollectors);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ViewMain";
            this.Text = "ViewMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCollectors;
        private System.Windows.Forms.Button btnHarvests;
        private System.Windows.Forms.Button btnCollects;
    }
}