using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FoliosApp.Ventanas
{
    public partial class vLogin : Form
    {
        ServiciosUsuarios serviciosUsuarios = new ServiciosUsuarios();

        public vLogin()
        {
            InitializeComponent();
        }

        #region Eventos
        private void btnSalir_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, System.EventArgs e)
        {
            Error error = new Error();
            string nombreUsuario;
            string pass;
            string md5 = "";
            Usuario usuario = new Usuario();

            nombreUsuario = txtNombreUsuario.Text;
            pass = txtClave.Text;
            usuario = serviciosUsuarios.GetUsuario(nombreUsuario, error);

            if (error.CodError > 0)
            {
                md5 = Rutinas.GenerarMD5(nombreUsuario, pass, error);

                if(md5 == usuario.Clave)
                {
                    try
                    {
                        serviciosUsuarios.GrabarIngreso(nombreUsuario, 1, error);
                        
                        ConectorMySql.CrearConexion(error);
                        Hide();

                        Principal principal = new Principal();
                        principal.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox2.Show(IconosVarios.Error, "", "Ocurrió un error al intentar conectarse al sistema.", true);
                    }
                }
                else
                {
                    serviciosUsuarios.GrabarIngreso(nombreUsuario, 2, error);
                    MessageBox2.Show(IconosVarios.Error, "", "Acceso incorrecto al sistema", true);
                }
            }
            else
            {
                serviciosUsuarios.GrabarIngreso(nombreUsuario, 3, error);
                MessageBox2.Show(IconosVarios.Error, "", "Acceso incorrecto al sistema", true);
            }
            
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
            }
        }

        private void txtNombreUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnIngresar.PerformClick();
            }
        }

        private void vLogin_Shown(object sender, EventArgs e)
        {
            txtNombreUsuario.Focus();
        }

        #endregion

        #region Métodos


        #endregion
    }
}