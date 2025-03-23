using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FoliosApp.Ventanas
{
    public partial class vUsuariosCambioClave : Form
    {
        ServiciosUsuarios serviciosUsuarios = new ServiciosUsuarios();
        public string NuevaClave;
        public string NombreUsuario;

        public vUsuariosCambioClave(string sNombreUsuario)
        {
            InitializeComponent();
            lblNombreUsuario.Text = sNombreUsuario;
            NombreUsuario = sNombreUsuario;
        }

        #region Eventos
        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
           Close();
        }

        private void btnGrabar_Click(object sender, System.EventArgs e)
        {
            NuevaClave = txtNuevaClave.Text;
            DialogResult = DialogResult.OK;
            Hide();
        }        

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGrabar.PerformClick();
            }
        }

        private void vLogin_Shown(object sender, EventArgs e)
        {
            txtNuevaClave.Focus();
        }

        #endregion

        #region Métodos


        #endregion
    }
}