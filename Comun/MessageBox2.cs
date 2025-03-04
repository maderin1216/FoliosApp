using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FoliosApp.Comun
{
    public partial class MessageBox2 : Form
    {
        static MessageBox2 newMessageBox;
        public static DialogResult dialogResult;

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public MessageBox2()
        {
            InitializeComponent();
        }

        #region Eventos
        private void MessageBox_Load(object sender, EventArgs e)
        {
            pnBotones.Left = (pnBotones.Parent.Size.Width - pnBotones.Width) / 2;
        }

        private void btnSi_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.Yes;
            Dispose();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.No;
            Dispose();
        }

        private void pboxCerrar_Click(object sender, EventArgs e)
        {
            dialogResult = DialogResult.None;
            Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageBox2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region Métodos
        public static DialogResult Show(Bitmap imagen, string titulo, string pregunta, int aiAltura = 192)
        {
            newMessageBox = new MessageBox2();
            newMessageBox.Height = aiAltura;

            newMessageBox.lbTitulo.Text = titulo;
            newMessageBox.rtbPrueba.Text = pregunta;

            newMessageBox.pictureBox1.Image = imagen;
            newMessageBox.ShowDialog();

            return dialogResult;
        }

        public static void Show(Bitmap imagen, string titulo, string mensaje, bool bDialogAceptar, int aiAltura = 192)
        {
            newMessageBox = new MessageBox2();
            newMessageBox.lbTitulo.Text = titulo;
            newMessageBox.Height = aiAltura;
            newMessageBox.rtbPrueba.Text = mensaje;

            newMessageBox.pnAceptar.Location = newMessageBox.pnBotones.Location;
            newMessageBox.pnBotones.Visible = false;
            newMessageBox.pnAceptar.Visible = true;

            newMessageBox.pboxCerrar.Visible = false;

            newMessageBox.pictureBox1.Image = imagen;
            newMessageBox.TopMost = true;

            newMessageBox.ShowDialog();
        }
        #endregion
    }
}
