using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FoliosApp.Ventanas
{
    public partial class vAbout : Form
    {
        ServiciosUsuarios serviciosUsuarios = new ServiciosUsuarios();

        public vAbout()
        {
            InitializeComponent();
        }

        #region Eventos
        private void btnAceptar_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void vAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = Properties.Resources.version;
            lblFecha.Text = Properties.Resources.fecha;
        }
        #endregion

        #region Métodos


        #endregion
    }
}