namespace CAFEPAY
{
    partial class frmGestionRecolectores
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcGestionRecolectores = new System.Windows.Forms.TabControl();
            this.tpRegistrarRecolector = new System.Windows.Forms.TabPage();
            this.gbListaRecolectores = new System.Windows.Forms.GroupBox();
            this.btnActualizarLista = new System.Windows.Forms.Button();
            this.btnDarBaja = new System.Windows.Forms.Button();
            this.dgvRecolectores = new System.Windows.Forms.DataGridView();
            this.gbRegistrarRecolector = new System.Windows.Forms.GroupBox();
            this.btnLimpiarCampos = new System.Windows.Forms.Button();
            this.btnRegistrar = new System.Windows.Forms.Button();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCedula = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tcGestionRecolectores.SuspendLayout();
            this.tpRegistrarRecolector.SuspendLayout();
            this.gbListaRecolectores.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecolectores)).BeginInit();
            this.gbRegistrarRecolector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcGestionRecolectores
            // 
            this.tcGestionRecolectores.Controls.Add(this.tpRegistrarRecolector);
            this.tcGestionRecolectores.Controls.Add(this.tabPage2);
            this.tcGestionRecolectores.Location = new System.Drawing.Point(77, 56);
            this.tcGestionRecolectores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tcGestionRecolectores.Name = "tcGestionRecolectores";
            this.tcGestionRecolectores.SelectedIndex = 0;
            this.tcGestionRecolectores.Size = new System.Drawing.Size(665, 292);
            this.tcGestionRecolectores.TabIndex = 0;
            // 
            // tpRegistrarRecolector
            // 
            this.tpRegistrarRecolector.Controls.Add(this.gbListaRecolectores);
            this.tpRegistrarRecolector.Controls.Add(this.gbRegistrarRecolector);
            this.tpRegistrarRecolector.Location = new System.Drawing.Point(4, 22);
            this.tpRegistrarRecolector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpRegistrarRecolector.Name = "tpRegistrarRecolector";
            this.tpRegistrarRecolector.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpRegistrarRecolector.Size = new System.Drawing.Size(657, 266);
            this.tpRegistrarRecolector.TabIndex = 0;
            this.tpRegistrarRecolector.Text = "Gestión Recolector";
            this.tpRegistrarRecolector.UseVisualStyleBackColor = true;
            // 
            // gbListaRecolectores
            // 
            this.gbListaRecolectores.Controls.Add(this.btnActualizarLista);
            this.gbListaRecolectores.Controls.Add(this.btnDarBaja);
            this.gbListaRecolectores.Controls.Add(this.dgvRecolectores);
            this.gbListaRecolectores.Location = new System.Drawing.Point(336, 33);
            this.gbListaRecolectores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbListaRecolectores.Name = "gbListaRecolectores";
            this.gbListaRecolectores.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbListaRecolectores.Size = new System.Drawing.Size(292, 201);
            this.gbListaRecolectores.TabIndex = 1;
            this.gbListaRecolectores.TabStop = false;
            this.gbListaRecolectores.Text = "Lista de Recolectores Registrados";
            // 
            // btnActualizarLista
            // 
            this.btnActualizarLista.Location = new System.Drawing.Point(188, 131);
            this.btnActualizarLista.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnActualizarLista.Name = "btnActualizarLista";
            this.btnActualizarLista.Size = new System.Drawing.Size(86, 28);
            this.btnActualizarLista.TabIndex = 3;
            this.btnActualizarLista.Text = "Actualizar Lista";
            this.btnActualizarLista.UseVisualStyleBackColor = true;
            // 
            // btnDarBaja
            // 
            this.btnDarBaja.Location = new System.Drawing.Point(16, 132);
            this.btnDarBaja.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDarBaja.Name = "btnDarBaja";
            this.btnDarBaja.Size = new System.Drawing.Size(83, 27);
            this.btnDarBaja.TabIndex = 1;
            this.btnDarBaja.Text = "Dar de Baja";
            this.btnDarBaja.UseVisualStyleBackColor = true;
            this.btnDarBaja.Click += new System.EventHandler(this.btnDarBaja_Click);
            // 
            // dgvRecolectores
            // 
            this.dgvRecolectores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecolectores.Location = new System.Drawing.Point(28, 34);
            this.dgvRecolectores.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvRecolectores.Name = "dgvRecolectores";
            this.dgvRecolectores.RowHeadersWidth = 51;
            this.dgvRecolectores.RowTemplate.Height = 24;
            this.dgvRecolectores.Size = new System.Drawing.Size(236, 72);
            this.dgvRecolectores.TabIndex = 0;
            // 
            // gbRegistrarRecolector
            // 
            this.gbRegistrarRecolector.Controls.Add(this.btnLimpiarCampos);
            this.gbRegistrarRecolector.Controls.Add(this.btnRegistrar);
            this.gbRegistrarRecolector.Controls.Add(this.txtTelefono);
            this.gbRegistrarRecolector.Controls.Add(this.txtNombre);
            this.gbRegistrarRecolector.Controls.Add(this.txtCedula);
            this.gbRegistrarRecolector.Controls.Add(this.lblTelefono);
            this.gbRegistrarRecolector.Controls.Add(this.lblNombre);
            this.gbRegistrarRecolector.Controls.Add(this.lblCedula);
            this.gbRegistrarRecolector.Location = new System.Drawing.Point(28, 33);
            this.gbRegistrarRecolector.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRegistrarRecolector.Name = "gbRegistrarRecolector";
            this.gbRegistrarRecolector.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.gbRegistrarRecolector.Size = new System.Drawing.Size(271, 201);
            this.gbRegistrarRecolector.TabIndex = 0;
            this.gbRegistrarRecolector.TabStop = false;
            this.gbRegistrarRecolector.Text = "Registrar Nuevo Recolector";
            // 
            // btnLimpiarCampos
            // 
            this.btnLimpiarCampos.Location = new System.Drawing.Point(143, 154);
            this.btnLimpiarCampos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLimpiarCampos.Name = "btnLimpiarCampos";
            this.btnLimpiarCampos.Size = new System.Drawing.Size(96, 27);
            this.btnLimpiarCampos.TabIndex = 7;
            this.btnLimpiarCampos.Text = "Limpiar Campos";
            this.btnLimpiarCampos.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(34, 154);
            this.btnRegistrar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(87, 27);
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(84, 103);
            this.txtTelefono.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(76, 20);
            this.txtTelefono.TabIndex = 5;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(80, 70);
            this.txtNombre.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(104, 20);
            this.txtNombre.TabIndex = 4;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(76, 37);
            this.txtCedula.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(76, 20);
            this.txtCedula.TabIndex = 3;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(32, 106);
            this.lblTelefono.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(52, 13);
            this.lblTelefono.TabIndex = 2;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(32, 72);
            this.lblNombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(32, 40);
            this.lblCedula.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(43, 13);
            this.lblCedula.TabIndex = 0;
            this.lblCedula.Text = "Cédula:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(657, 266);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmGestionRecolectores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 405);
            this.Controls.Add(this.tcGestionRecolectores);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmGestionRecolectores";
            this.Text = "Sistema de Gestión de Pagos - Caficultora";
            this.tcGestionRecolectores.ResumeLayout(false);
            this.tpRegistrarRecolector.ResumeLayout(false);
            this.gbListaRecolectores.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecolectores)).EndInit();
            this.gbRegistrarRecolector.ResumeLayout(false);
            this.gbRegistrarRecolector.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcGestionRecolectores;
        private System.Windows.Forms.TabPage tpRegistrarRecolector;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbRegistrarRecolector;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCedula;
        private System.Windows.Forms.Button btnLimpiarCampos;
        private System.Windows.Forms.Button btnRegistrar;
        private System.Windows.Forms.GroupBox gbListaRecolectores;
        private System.Windows.Forms.Button btnActualizarLista;
        private System.Windows.Forms.Button btnDarBaja;
        private System.Windows.Forms.DataGridView dgvRecolectores;
    }
}

