using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FoliosApp.Ventanas
{
    public partial class vUsuarios : Form
    {
        ServiciosUsuarios serviciosUsuarios = new ServiciosUsuarios();

        public vUsuarios()
        {
            InitializeComponent();            
        }

        #region Eventos
        private void btnCancelar_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnGrabar_Click(object sender, System.EventArgs e)
        {
            Error error = new Error();
            ValidarCampos(error);

            if (error.CodError > 0)
            {
                //Documento = txtDocumento.Text;
                //Apellido = txtApellido.Text.ToUpper();
                //Nombre = txtNombre.Text.ToUpper();
                //Libro = int.TryParse(txtLibro.Text, out int iLibro) ? iLibro : 0;
                //Folio = int.TryParse(txtFolio.Text, out int iFolio) ? iFolio : 0;
                //FechaNacimiento = dtpFechaNacimiento.Value;
                //FechaBautismo = dtpFechaBautismo.Value;

                //DialogResult = DialogResult.OK;
                //Close();
            }
            else
            {
                MessageBox2.Show(IconosVarios.Advertencia, "Validaciones", $"Al grabar el registro no se cumplieron las siguientes validaciones: \n• {string.Join("\n• ", error.Mensajes)}", true, 400 );
            }
        }

        private void vUsuarios_Load(object sender, System.EventArgs e)
        {
            Inicio();
        }
        #endregion

        #region Métodos
        private void EnlazarControles()
        {
            txtNombreUsuario.DataBindings.Clear();
            txtPassword.DataBindings.Clear();
            cbxPermisos.DataBindings.Clear();

            txtNombreUsuario.DataBindings.Add("Text", bsUsuarios, "Nombre", false, DataSourceUpdateMode.Never);
            txtPassword.DataBindings.Add("Text", bsUsuarios, "Clave", false, DataSourceUpdateMode.Never);
            cbxPermisos.DataBindings.Add("SelectedValue", bsUsuarios, "IdNivel", false, DataSourceUpdateMode.Never);
        }

        private void Inicio()
        {
            List<Usuario> lUsuarios = new List<Usuario>();
            List<UsuarioNivel> lNivelesUsuario = new List<UsuarioNivel>();
            Error error = new Error();

            Rutinas.FormatoGrilla(dgvUsuarios);

            //Criterios
            lNivelesUsuario = serviciosUsuarios.GetAllNivelesUsuarios(error);

            cbxPermisos.DataSource = lNivelesUsuario;
            cbxPermisos.DisplayMember = "Descripcion";
            cbxPermisos.ValueMember = "IdNivel";

            //Grilla
            lUsuarios = serviciosUsuarios.GetAllUsuarios(error);

            if (error.CodError > 0)
            {
                bsUsuarios.DataSource = lUsuarios;
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }

            EnlazarControles();
            ModoControles(ModoBotones.FilaSeleccionada);
        }

        private void ModoControles(ModoBotones modoBotones)
        {
            switch(modoBotones)
            {
                case ModoBotones.Inicial:
                    txtNombreUsuario.Enabled = false;
                    txtPassword.Enabled = false;
                    cbxPermisos.Enabled = false;

                    btnAgregar.Enabled = true;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;

                    btnGrabar.Visible = false;
                    btnGrabar.Enabled = false;

                    btnCancelar.Visible = false;
                    btnCancelar.Enabled = false;
                    break;

                case ModoBotones.FilaSeleccionada:
                    txtNombreUsuario.Enabled = false;
                    txtPassword.Enabled = false;
                    cbxPermisos.Enabled = false;

                    btnAgregar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnBorrar.Enabled = true;

                    btnGrabar.Visible = false;
                    btnGrabar.Enabled = false;

                    btnCancelar.Visible = false;
                    btnCancelar.Enabled = false;
                    break;

                case ModoBotones.Editar:
                    txtNombreUsuario.Enabled = true;
                    txtPassword.Enabled = true;
                    cbxPermisos.Enabled = true;

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

        private void ValidarCampos(Error error)
        {
            //error.CodError = 1;

            ////Documento
            //if (txtDocumento.Text.Length == 0)
            //{
            //    error.CodError = -1;
            //    error.Mensajes.Add("El campo 'Documento' no puede quedar vacío.");
            //}
            //else
            //{
            //    if (txtDocumento.Text.Length > 10)
            //    {
            //        error.CodError = -1;
            //        error.Mensajes.Add("El campo 'Documento' debe contener hasta 10 caracteres.");
            //    }
            //    else
            //    {
            //        if (!Regex.IsMatch(txtDocumento.Text, @"^\d+$"))
            //        {
            //            error.CodError = -1;
            //            error.Mensajes.Add("El campo 'Documento' debe ser solo numérico.");
            //        }
            //    }
            //}

            ////Apellido
            //if (txtApellido.Text.Length == 0)
            //{
            //    error.CodError = -1;
            //    error.Mensajes.Add("El campo 'Apellido' no puede quedar vacío.");
            //}
            //else
            //{
            //    if (txtApellido.Text.Length > 50)
            //    {
            //        error.CodError = -1;
            //        error.Mensajes.Add("El campo 'Apellido' debe contener hasta 50 caracteres.");
            //    }
            //    else
            //    {
            //        if (!Regex.IsMatch(txtApellido.Text, @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü ]+$"))
            //        {
            //            error.CodError = -1;
            //            error.Mensajes.Add("El campo 'Apellido' debe contener solamente letras y/o espacios.");
            //        }
            //    }
            //}

            ////Nombre
            //if (txtNombre.Text.Length == 0)
            //{
            //    error.CodError = -1;
            //    error.Mensajes.Add("El campo 'Nombre' no puede quedar vacío.");
            //}
            //else
            //{
            //    if (txtNombre.Text.Length > 50)
            //    {
            //        error.CodError = -1;
            //        error.Mensajes.Add("El campo 'Nombre' debe contener hasta 50 caracteres.");
            //    }
            //    else
            //    {
            //        if (!Regex.IsMatch(txtNombre.Text, @"^[A-Za-zÁÉÍÓÚáéíóúÑñÜü ]+$"))
            //        {
            //            error.CodError = -1;
            //            error.Mensajes.Add("El campo 'Nombre' debe contener solamente letras y/o espacios.");
            //        }
            //    }
            //}

            ////Libro
            //if (txtLibro.Text.Length == 0)
            //{
            //    error.CodError = -1;
            //    error.Mensajes.Add("El campo 'Libro' no puede quedar vacío.");
            //}
            //else
            //{ 
            //    if (!Regex.IsMatch(txtLibro.Text, @"^\d+$"))
            //    {
            //        error.CodError = -1;
            //        error.Mensajes.Add("El campo 'Libro' debe ser solo numérico.");
            //    }
            //}

            ////Folio
            //if (txtFolio.Text.Length == 0)
            //{
            //    error.CodError = -1;
            //    error.Mensajes.Add("El campo 'Folio' no puede quedar vacío.");
            //}
            //else
            //{
            //    if (!Regex.IsMatch(txtFolio.Text, @"^\d+$"))
            //    {
            //        error.CodError = -1;
            //        error.Mensajes.Add("El campo 'Folio' debe ser solo numérico.");
            //    }
            //}
        }
        #endregion

        private void bsUsuarios_CurrentChanged(object sender, EventArgs e)
        {
            ModoControles(ModoBotones.FilaSeleccionada);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ModoControles(ModoBotones.Editar);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            ModoControles(ModoBotones.Editar);
        }

    }
}