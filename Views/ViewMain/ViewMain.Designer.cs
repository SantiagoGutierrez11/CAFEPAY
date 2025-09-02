namespace CAFEPAY.Views.ViewMain
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
            System.Windows.Forms.Button btnViewCollector;
            this.btnViewHarvest = new System.Windows.Forms.Button();
            this.btnCollect = new System.Windows.Forms.Button();
            btnViewCollector = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnViewHarvest
            // 
            this.btnViewHarvest.Location = new System.Drawing.Point(94, 132);
            this.btnViewHarvest.Name = "btnViewHarvest";
            this.btnViewHarvest.Size = new System.Drawing.Size(202, 86);
            this.btnViewHarvest.TabIndex = 0;
            this.btnViewHarvest.Text = "Cosechas";
            this.btnViewHarvest.UseVisualStyleBackColor = true;
            this.btnViewHarvest.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnViewCollector
            // 
            btnViewCollector.Location = new System.Drawing.Point(317, 132);
            btnViewCollector.Name = "btnViewCollector";
            btnViewCollector.Size = new System.Drawing.Size(202, 86);
            btnViewCollector.TabIndex = 1;
            btnViewCollector.Text = "Recolectores";
            btnViewCollector.UseVisualStyleBackColor = true;
            // 
            // btnCollect
            // 
            this.btnCollect.Location = new System.Drawing.Point(555, 132);
            this.btnCollect.Name = "btnCollect";
            this.btnCollect.Size = new System.Drawing.Size(195, 86);
            this.btnCollect.TabIndex = 2;
            this.btnCollect.Text = "Recolecciones";
            this.btnCollect.UseVisualStyleBackColor = true;
            this.btnCollect.Click += new System.EventHandler(this.btnCollect_Click);
            // 
            // ViewMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCollect);
            this.Controls.Add(btnViewCollector);
            this.Controls.Add(this.btnViewHarvest);
            this.Name = "ViewMain";
            this.Text = "ViewMain";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnViewHarvest;
        private System.Windows.Forms.Button btnCollect;
    }
}