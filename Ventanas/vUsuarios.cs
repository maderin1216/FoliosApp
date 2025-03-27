using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FoliosApp.Ventanas
{
    public partial class vUsuarios : Form
    {
        ServiciosUsuarios serviciosUsuarios = new ServiciosUsuarios();
        ModoVentana modoVentana = ModoVentana.Ninguno;

        public vUsuarios()
        {
            InitializeComponent();            
        }

        #region Eventos
        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            ModoControles(ModoBotones.Inicial);
        }

        private void btnCerrar_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void btnGrabar_Click(object sender, System.EventArgs e)
        {
            Error error = new Error();
            Usuario usuario = new Usuario();

            usuario.Id = ((Usuario)bsUsuarios.Current).Id;
            usuario.Nombre = txtNombreUsuario.Text;
            usuario.IdNivel = Convert.ToInt32(cbxPermisos.SelectedValue);
            usuario.Clave = txtClave.Text;

            if (modoVentana == ModoVentana.Agregar)
            {
                serviciosUsuarios.AgregarUsuario(usuario, error);
            }

            if (modoVentana == ModoVentana.Modificar)
            {
                serviciosUsuarios.ActualizarUsuario(usuario, error);
            }

            if(error.CodError > 0)
            {
                MessageBox2.Show(IconosVarios.Tilde, "", "Los datos se grabaron correctamente.", true);

                ModoControles(ModoBotones.Inicial);

                bsUsuarios.Position = ((List<Usuario>)bsUsuarios.List).FindIndex(u => u.Id == usuario.Id);
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "No se pudo grabar información", $"Detalles: {error.Mensaje}", true);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Error error = new Error();
            DialogResult drBorrar;
            Usuario usuario = new Usuario();

            usuario = (Usuario)bsUsuarios.Current;

            if (usuario.Nombre.ToUpper() != "ADMIN")
            {
                if (usuario.Nombre.ToUpper() != UsuarioActivo._usuario.Nombre.ToUpper())
                {
                    drBorrar = MessageBox2.Show(IconosVarios.Advertencia, "Confirmar borrado", "¿Está seguro que desea eliminar el registro seleccionado?");

                    if (drBorrar == DialogResult.Yes)
                    {
                        serviciosUsuarios.BorrarUsuario(usuario, error);

                        if (error.CodError > 0)
                        {
                            MessageBox2.Show(IconosVarios.Tilde, "", "El registro se eliminó correctamente.", true);

                            CargaInicialGrilla();
                            bsUsuarios.Position = 0;
                        }
                        else
                        {
                            MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al eliminar el registro.", error.Mensaje, true);
                        }
                    }
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Advertencia, "", "No se puede borrar el usuario activo", true);
                }
            }
            else
            {
                MessageBox2.Show(IconosVarios.Advertencia, "", "No se puede borrar el usuario 'Admin'", true);
            }
        }

        private void vUsuarios_Load(object sender, System.EventArgs e)
        {
            Inicio();
        }

        private void bsUsuarios_CurrentChanged(object sender, EventArgs e)
        {
            ModoControles(ModoBotones.FilaSeleccionada);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            modoVentana = ModoVentana.Modificar;
            ModoControles(ModoBotones.Editar);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            modoVentana = ModoVentana.Agregar;
            ModoControles(ModoBotones.Agregar);
            txtNombreUsuario.Focus();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {
            vUsuariosCambioClave vUsuariosCambioClave = new vUsuariosCambioClave(btnCambiarClave.Tag.ToString());
            DialogResult dialogResult;
            Error error = new Error();

            dialogResult = vUsuariosCambioClave.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                serviciosUsuarios.UpdateClave(((Usuario)bsUsuarios.Current).Id, vUsuariosCambioClave.NuevaClave, error);

                if (error.CodError > 0)
                {
                    MessageBox2.Show(IconosVarios.Tilde, "", $"La clave del usuario '{vUsuariosCambioClave.NombreUsuario}' se actualizó correctamente.", true);
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "", $"La clave del usuario '{vUsuariosCambioClave.NombreUsuario}' no pudo ser actualizada. Detalle: {error.Mensaje}", true);
                }
            }
        }

        private void btnVerClave_MouseDown(object sender, MouseEventArgs e)
        {
            txtClave.PasswordChar = '\0';
        }

        private void btnVerClave_MouseUp(object sender, MouseEventArgs e)
        {
            txtClave.PasswordChar = '•';
        }
        #endregion

        #region Métodos
        private void CargaInicialGrilla()
        {
            List<Usuario> lUsuarios = new List<Usuario>();
            Error error = new Error();

            lUsuarios = serviciosUsuarios.GetAllUsuarios(error);

            if (error.CodError > 0)
            {
                bsUsuarios.DataSource = lUsuarios;

                ModoControles(ModoBotones.FilaSeleccionada);
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }
        }

        private void EnlazarControles()
        {
            txtNombreUsuario.DataBindings.Clear();
            btnCambiarClave.DataBindings.Clear();
            cbxPermisos.DataBindings.Clear();

            txtNombreUsuario.DataBindings.Add("Text", bsUsuarios, "Nombre", false, DataSourceUpdateMode.Never);
            btnCambiarClave.DataBindings.Add("Tag", bsUsuarios, "Nombre", false, DataSourceUpdateMode.Never);
            cbxPermisos.DataBindings.Add("SelectedValue", bsUsuarios, "IdNivel", false, DataSourceUpdateMode.Never);
        }

        private void Inicio()
        {            
            List<UsuarioNivel> lNivelesUsuario = new List<UsuarioNivel>();
            Error error = new Error();

            Rutinas.FormatoGrilla(dgvUsuarios);

            //Criterios
            lNivelesUsuario = serviciosUsuarios.GetAllNivelesUsuarios(error);

            cbxPermisos.DataSource = lNivelesUsuario;
            cbxPermisos.DisplayMember = "Descripcion";
            cbxPermisos.ValueMember = "IdNivel";

            //Grilla
            CargaInicialGrilla();

            EnlazarControles();
            ModoControles(ModoBotones.FilaSeleccionada);
        }

        private void ModoControles(ModoBotones modoBotones)
        {
            switch(modoBotones)
            {
                case ModoBotones.Inicial:
                    CargaInicialGrilla(); //La carga inicial pasa al estado a "FilaSeleccionada"
                    dgvUsuarios.Enabled = true;
                    btnCambiarClave.Enabled = false;
                    btnCambiarClave.Visible = true;
                    btnVerClave.Enabled = false;
                    btnVerClave.Visible = false;
                    txtClave.Visible = false;

                    modoVentana = ModoVentana.Ninguno;
                    break;

                case ModoBotones.FilaSeleccionada:
                    txtNombreUsuario.Enabled = false;
                    btnCambiarClave.Enabled = false;
                    cbxPermisos.Enabled = false;
                    btnVerClave.Enabled = false;
                    btnVerClave.Visible = false;

                    btnAgregar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnBorrar.Enabled = true;

                    btnGrabar.Visible = false;
                    btnGrabar.Enabled = false;

                    btnCancelar.Visible = false;
                    btnCancelar.Enabled = false;
                    break;

                case ModoBotones.Editar:
                    dgvUsuarios.Enabled = false;

                    txtNombreUsuario.Enabled = false;
                    btnCambiarClave.Enabled = true;
                    cbxPermisos.Enabled = true;

                    btnAgregar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;

                    btnGrabar.Visible = true;
                    btnGrabar.Enabled = true;

                    btnCancelar.Visible = true;
                    btnCancelar.Enabled = true;
                    break;

                case ModoBotones.Agregar:
                    dgvUsuarios.ClearSelection();
                    dgvUsuarios.Enabled = false;
                    txtNombreUsuario.Clear();
                    cbxPermisos.SelectedIndex = 0;

                    txtNombreUsuario.Enabled = true;
                    txtClave.Visible = true;
                    txtClave.Clear();
                    btnCambiarClave.Enabled = false;
                    btnCambiarClave.Visible = false;
                    cbxPermisos.Enabled = true;
                    btnVerClave.Enabled = true;
                    btnVerClave.Visible = true;

                    btnAgregar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;

                    btnGrabar.Visible = true;
                    btnGrabar.Enabled = true;

                    btnCancelar.Visible = true;
                    btnCancelar.Enabled = true;
                    break;
            }
        }
        #endregion
    }
}