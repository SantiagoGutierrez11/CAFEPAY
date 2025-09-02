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
            this.btnReactivar = new System.Windows.Forms.Button();
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
            this.tcGestionRecolectores.Location = new System.Drawing.Point(103, 69);
            this.tcGestionRecolectores.Name = "tcGestionRecolectores";
            this.tcGestionRecolectores.SelectedIndex = 0;
            this.tcGestionRecolectores.Size = new System.Drawing.Size(887, 360);
            this.tcGestionRecolectores.TabIndex = 0;
            // 
            // tpRegistrarRecolector
            // 
            this.tpRegistrarRecolector.Controls.Add(this.gbListaRecolectores);
            this.tpRegistrarRecolector.Controls.Add(this.gbRegistrarRecolector);
            this.tpRegistrarRecolector.Location = new System.Drawing.Point(4, 25);
            this.tpRegistrarRecolector.Name = "tpRegistrarRecolector";
            this.tpRegistrarRecolector.Padding = new System.Windows.Forms.Padding(3);
            this.tpRegistrarRecolector.Size = new System.Drawing.Size(879, 331);
            this.tpRegistrarRecolector.TabIndex = 0;
            this.tpRegistrarRecolector.Text = "Gestión Recolector";
            this.tpRegistrarRecolector.UseVisualStyleBackColor = true;
            // 
            // gbListaRecolectores
            // 
            this.gbListaRecolectores.Controls.Add(this.btnActualizarLista);
            this.gbListaRecolectores.Controls.Add(this.btnReactivar);
            this.gbListaRecolectores.Controls.Add(this.btnDarBaja);
            this.gbListaRecolectores.Controls.Add(this.dgvRecolectores);
            this.gbListaRecolectores.Location = new System.Drawing.Point(448, 41);
            this.gbListaRecolectores.Name = "gbListaRecolectores";
            this.gbListaRecolectores.Size = new System.Drawing.Size(389, 247);
            this.gbListaRecolectores.TabIndex = 1;
            this.gbListaRecolectores.TabStop = false;
            this.gbListaRecolectores.Text = "Lista de Recolectores Registrados";
            // 
            // btnActualizarLista
            // 
            this.btnActualizarLista.Location = new System.Drawing.Point(251, 161);
            this.btnActualizarLista.Name = "btnActualizarLista";
            this.btnActualizarLista.Size = new System.Drawing.Size(115, 35);
            this.btnActualizarLista.TabIndex = 3;
            this.btnActualizarLista.Text = "Actualizar Lista";
            this.btnActualizarLista.UseVisualStyleBackColor = true;
            // 
            // btnReactivar
            // 
            this.btnReactivar.Location = new System.Drawing.Point(149, 162);
            this.btnReactivar.Name = "btnReactivar";
            this.btnReactivar.Size = new System.Drawing.Size(85, 34);
            this.btnReactivar.TabIndex = 2;
            this.btnReactivar.Text = "Reactivar";
            this.btnReactivar.UseVisualStyleBackColor = true;
            // 
            // btnDarBaja
            // 
            this.btnDarBaja.Location = new System.Drawing.Point(22, 163);
            this.btnDarBaja.Name = "btnDarBaja";
            this.btnDarBaja.Size = new System.Drawing.Size(111, 33);
            this.btnDarBaja.TabIndex = 1;
            this.btnDarBaja.Text = "Dar de Baja";
            this.btnDarBaja.UseVisualStyleBackColor = true;
            // 
            // dgvRecolectores
            // 
            this.dgvRecolectores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecolectores.Location = new System.Drawing.Point(37, 42);
            this.dgvRecolectores.Name = "dgvRecolectores";
            this.dgvRecolectores.RowHeadersWidth = 51;
            this.dgvRecolectores.RowTemplate.Height = 24;
            this.dgvRecolectores.Size = new System.Drawing.Size(314, 88);
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
            this.gbRegistrarRecolector.Location = new System.Drawing.Point(37, 41);
            this.gbRegistrarRecolector.Name = "gbRegistrarRecolector";
            this.gbRegistrarRecolector.Size = new System.Drawing.Size(361, 247);
            this.gbRegistrarRecolector.TabIndex = 0;
            this.gbRegistrarRecolector.TabStop = false;
            this.gbRegistrarRecolector.Text = "Registrar Nuevo Recolector";
            // 
            // btnLimpiarCampos
            // 
            this.btnLimpiarCampos.Location = new System.Drawing.Point(191, 189);
            this.btnLimpiarCampos.Name = "btnLimpiarCampos";
            this.btnLimpiarCampos.Size = new System.Drawing.Size(128, 33);
            this.btnLimpiarCampos.TabIndex = 7;
            this.btnLimpiarCampos.Text = "Limpiar Campos";
            this.btnLimpiarCampos.UseVisualStyleBackColor = true;
            // 
            // btnRegistrar
            // 
            this.btnRegistrar.Location = new System.Drawing.Point(45, 189);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new System.Drawing.Size(116, 33);
            this.btnRegistrar.TabIndex = 6;
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.UseVisualStyleBackColor = true;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(112, 127);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(100, 22);
            this.txtTelefono.TabIndex = 5;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(107, 86);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(138, 22);
            this.txtNombre.TabIndex = 4;
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(101, 46);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(100, 22);
            this.txtCedula.TabIndex = 3;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Location = new System.Drawing.Point(42, 130);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(64, 16);
            this.lblTelefono.TabIndex = 2;
            this.lblTelefono.Text = "Teléfono:";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(42, 89);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(59, 16);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre:";
            // 
            // lblCedula
            // 
            this.lblCedula.AutoSize = true;
            this.lblCedula.Location = new System.Drawing.Point(42, 49);
            this.lblCedula.Name = "lblCedula";
            this.lblCedula.Size = new System.Drawing.Size(53, 16);
            this.lblCedula.TabIndex = 0;
            this.lblCedula.Text = "Cédula:";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(879, 331);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // frmGestionRecolectores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 499);
            this.Controls.Add(this.tcGestionRecolectores);
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
        private System.Windows.Forms.Button btnReactivar;
        private System.Windows.Forms.Button btnDarBaja;
        private System.Windows.Forms.DataGridView dgvRecolectores;
    }
}

