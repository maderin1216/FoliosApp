
namespace FoliosApp.Comun
{
    partial class MessageBox2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox2));
            this.lbTitulo = new System.Windows.Forms.Label();
            this.pnBotones = new System.Windows.Forms.Panel();
            this.btnSi = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.tbDetalles = new System.Windows.Forms.TextBox();
            this.pnDetalles = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pboxCerrar = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnAceptar = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtbPrueba = new System.Windows.Forms.RichTextBox();
            this.pnBotones.SuspendLayout();
            this.pnDetalles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCerrar)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnAceptar.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Calibri", 14F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(83, 9);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(156, 23);
            this.lbTitulo.TabIndex = 4;
            this.lbTitulo.Text = "Título del mensaje";
            // 
            // pnBotones
            // 
            this.pnBotones.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnBotones.Controls.Add(this.btnSi);
            this.pnBotones.Controls.Add(this.btnNo);
            this.pnBotones.Location = new System.Drawing.Point(163, 144);
            this.pnBotones.Name = "pnBotones";
            this.pnBotones.Size = new System.Drawing.Size(181, 40);
            this.pnBotones.TabIndex = 7;
            // 
            // btnSi
            // 
            this.btnSi.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnSi.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSi.Location = new System.Drawing.Point(0, 2);
            this.btnSi.Name = "btnSi";
            this.btnSi.Size = new System.Drawing.Size(74, 35);
            this.btnSi.TabIndex = 0;
            this.btnSi.Text = "Sí";
            this.btnSi.UseVisualStyleBackColor = true;
            this.btnSi.Click += new System.EventHandler(this.btnSi_Click);
            // 
            // btnNo
            // 
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnNo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            this.btnNo.Location = new System.Drawing.Point(106, 2);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(74, 35);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // tbDetalles
            // 
            this.tbDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDetalles.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDetalles.Location = new System.Drawing.Point(0, 0);
            this.tbDetalles.Multiline = true;
            this.tbDetalles.Name = "tbDetalles";
            this.tbDetalles.ReadOnly = true;
            this.tbDetalles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDetalles.Size = new System.Drawing.Size(491, 118);
            this.tbDetalles.TabIndex = 8;
            // 
            // pnDetalles
            // 
            this.pnDetalles.Controls.Add(this.tbDetalles);
            this.pnDetalles.Location = new System.Drawing.Point(9, 202);
            this.pnDetalles.Name = "pnDetalles";
            this.pnDetalles.Size = new System.Drawing.Size(491, 118);
            this.pnDetalles.TabIndex = 9;
            this.pnDetalles.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(60, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pboxCerrar
            // 
            this.pboxCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxCerrar.Image = global::FoliosApp.Properties.Resources.icono_ventana_cerrar;
            this.pboxCerrar.Location = new System.Drawing.Point(473, 8);
            this.pboxCerrar.Name = "pboxCerrar";
            this.pboxCerrar.Size = new System.Drawing.Size(26, 25);
            this.pboxCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pboxCerrar.TabIndex = 11;
            this.pboxCerrar.TabStop = false;
            this.pboxCerrar.Click += new System.EventHandler(this.pboxCerrar_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pnAceptar);
            this.panel1.Controls.Add(this.lbTitulo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 192);
            this.panel1.TabIndex = 12;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MessageBox2_MouseDown);
            // 
            // pnAceptar
            // 
            this.pnAceptar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnAceptar.Controls.Add(this.btnAceptar);
            this.pnAceptar.Location = new System.Drawing.Point(162, 99);
            this.pnAceptar.Name = "pnAceptar";
            this.pnAceptar.Size = new System.Drawing.Size(181, 40);
            this.pnAceptar.TabIndex = 8;
            this.pnAceptar.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAceptar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(52, 2);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(74, 35);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.rtbPrueba);
            this.panel2.Location = new System.Drawing.Point(84, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 92);
            this.panel2.TabIndex = 9;
            // 
            // rtbPrueba
            // 
            this.rtbPrueba.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbPrueba.BackColor = System.Drawing.Color.White;
            this.rtbPrueba.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbPrueba.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbPrueba.Location = new System.Drawing.Point(3, 3);
            this.rtbPrueba.Name = "rtbPrueba";
            this.rtbPrueba.ReadOnly = true;
            this.rtbPrueba.Size = new System.Drawing.Size(369, 86);
            this.rtbPrueba.TabIndex = 0;
            this.rtbPrueba.Text = "Contenido del mensaje de altura variable. Contenido del mensaje del mensaje de al" +
    "tura variable. Contenido del mensaje de altura variable. Contenido del mensaje d" +
    "e altura variable.";
            // 
            // MessageBox2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 192);
            this.Controls.Add(this.pboxCerrar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnDetalles);
            this.Controls.Add(this.pnBotones);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MessageBox2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Título";
            this.Load += new System.EventHandler(this.MessageBox_Load);
            this.pnBotones.ResumeLayout(false);
            this.pnDetalles.ResumeLayout(false);
            this.pnDetalles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pboxCerrar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnAceptar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Panel pnBotones;
        private System.Windows.Forms.Button btnSi;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.TextBox tbDetalles;
        private System.Windows.Forms.Panel pnDetalles;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pboxCerrar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnAceptar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RichTextBox rtbPrueba;
    }
}