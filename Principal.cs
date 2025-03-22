using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Servicios;
using FoliosApp.Ventanas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoliosApp
{
    public partial class Principal : Form
    {
        ServiciosBautismos serviciosBautismos = new ServiciosBautismos();
        ServiciosConfirmaciones serviciosConfirmaciones = new ServiciosConfirmaciones();

        public Principal()
        {
            InitializeComponent();
            Inicio();
        }

        #region Eventos
        private void bsBautismos_CurrentChanged(object sender, EventArgs e)
        {
            if (bsBautismos.Current != null)
            {
                ModoBotonesInferiores(ModoBotones.FilaSeleccionada);
            }
            else
            {
                ModoBotonesInferiores(ModoBotones.Inicial);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            vBautismos vBautismos = new vBautismos();
            DialogResult drAgregar;
            Error error = new Error();
            Bautismo bautismo = new Bautismo();
            int iIndiceGrilla;

            vBautismos.modoVentana = ModoVentana.Agregar;
            vBautismos.lblTitulo.Text = "Agregar certificado bautismo";
            drAgregar = vBautismos.ShowDialog();

            if(drAgregar == DialogResult.OK)
            {
                bautismo.Documento = vBautismos.Documento;
                bautismo.Apellido = vBautismos.Apellido;
                bautismo.Nombre = vBautismos.Nombre;
                bautismo.Libro = vBautismos.Libro;
                bautismo.Folio = vBautismos.Folio;
                bautismo.FechaNacimiento = vBautismos.FechaNacimiento;
                bautismo.FechaBautismo = vBautismos.FechaBautismo;

                serviciosBautismos.AgregarBautismo(bautismo, error);

                if(error.CodError > 0)
                {
                    MessageBox2.Show(IconosVarios.Tilde, "", "El registro se agregó correctamente.", true);

                    RecargarGrilla();
                    iIndiceGrilla = ((List<Bautismo>)bsBautismos.List).FindIndex(b => b.Id == bautismo.Id);
                    if (iIndiceGrilla > 0)
                    {
                        bsBautismos.Position = iIndiceGrilla;
                    }
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al agregar el registro.", error.Mensaje, true);
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Error error = new Error();
            DialogResult drBorrar;
            Bautismo bautismo = new Bautismo();

            drBorrar = MessageBox2.Show(IconosVarios.Advertencia, "Confirmar borrado", "¿Está seguro que desea eliminar el registro seleccionado?");

            if(drBorrar == DialogResult.Yes)
            {
                bautismo = (Bautismo)bsBautismos.Current;
                serviciosBautismos.BorrarBautismo(bautismo, error);

                if (error.CodError > 0)
                {
                    MessageBox2.Show(IconosVarios.Tilde, "", "El registro se eliminó correctamente.", true);

                    RecargarGrilla();
                    bsBautismos.Position = 0;
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al eliminar el registro.", error.Mensaje, true);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            vBautismos vBautismos = new vBautismos();
            DialogResult drAgregar;
            Error error = new Error();
            Bautismo bautismo = new Bautismo();
            int iIndiceGrilla;

            vBautismos.modoVentana = ModoVentana.Modificar;
            vBautismos.lblTitulo.Text = "Editar certificado bautismo";
            bautismo = (Bautismo)bsBautismos.Current;
            vBautismos.Documento = bautismo.Documento;
            vBautismos.Apellido = bautismo.Apellido;
            vBautismos.Nombre = bautismo.Nombre;
            vBautismos.Libro = bautismo.Libro;
            vBautismos.Folio = bautismo.Folio;
            vBautismos.FechaBautismo = bautismo.FechaBautismo;
            vBautismos.FechaNacimiento = bautismo.FechaNacimiento;

            drAgregar = vBautismos.ShowDialog();

            if (drAgregar == DialogResult.OK)
            {
                bautismo.Documento = vBautismos.Documento;
                bautismo.Apellido = vBautismos.Apellido;
                bautismo.Nombre = vBautismos.Nombre;
                bautismo.Libro = vBautismos.Libro;
                bautismo.Folio = vBautismos.Folio;
                bautismo.FechaNacimiento = vBautismos.FechaNacimiento;
                bautismo.FechaBautismo = vBautismos.FechaBautismo;

                serviciosBautismos.EditarBautismo(bautismo, error);

                if (error.CodError > 0)
                {
                    MessageBox2.Show(IconosVarios.Tilde, "", "El registro se modificó correctamente.", true);

                    RecargarGrilla();

                    iIndiceGrilla = ((List<Bautismo>)bsBautismos.List).FindIndex(b => b.Id == bautismo.Id);                    
                    if (iIndiceGrilla > 0)
                    {
                        bsBautismos.Position = iIndiceGrilla;
                    }
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al modificar el registro.", error.Mensaje, true);
                }
            }
        }

        private void Principal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            Error error = new Error();
            List<Bautismo> lBautismos = new List<Bautismo>();
            List<Confirmacion> lConfirmaciones = new List<Confirmacion>();
            List<dynamic> lCriterios;

            //Grilla dgvBautismos
            lBautismos = serviciosBautismos.GetAllBautismos(error);
            if (error.CodError > 0)
            {
                bsBautismos.DataSource = lBautismos;
                lblCantidadResultadosBautismo.Text = lBautismos.Count.ToString();
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }

            //Grilla dgvConfirmaciones
            lConfirmaciones = serviciosConfirmaciones.GetAllConfirmaciones(error);
            if (error.CodError > 0)
            {
                bsConfirmaciones.DataSource = lConfirmaciones;
                lblResultadosConfirmaciones.Text = lConfirmaciones.Count.ToString();
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }

            //Criterios
            lCriterios = new List<dynamic>
            {
                new { 
                    Texto = "Todas las columnas",
                    Valor = 0
                },
                new { 
                    Texto = "Documento",
                    Valor = 1
                },
                new {
                    Texto = "Apellido",
                    Valor = 2
                },
                new {
                    Texto = "Nombre",
                    Valor = 3
                },
                new {
                    Texto = "Libro",
                    Valor = 4
                },
                new {
                    Texto = "Folio",
                    Valor = 5
                },                
                new {
                    Texto = "Apellido + Nombre",
                    Valor = 6
                }
            };
            cbxCriterios.DataSource = lCriterios;
            cbxCriterios.DisplayMember = "Texto";
            cbxCriterios.ValueMember = "Valor";
        }

        private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Busqueda();
            }

            if(e.KeyCode == Keys.Down)
            {
                dgvBautismos.Focus();
            }
        }

        #endregion

        #region ToolStripMenuItem
        private void administrarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vUsuarios vUsuarios = new vUsuarios();

            vUsuarios.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Métodos
        private void Busqueda()
        {
            Error error = new Error();
            List<Bautismo> lBautismos = new List<Bautismo>();
            int iCriterio;

            iCriterio = Convert.ToInt32(cbxCriterios.SelectedValue);
            lBautismos = serviciosBautismos.GetBautismosCriterio((CriteriosBusqueda)iCriterio, txtFiltro.Text, error);
            if (error.CodError > 0)
            {
                bsBautismos.DataSource = lBautismos;
                lblCantidadResultadosBautismo.Text = lBautismos.Count.ToString();
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }
        }

        private void Inicio()
        {
            Rutinas.FormatoGrilla(dgvBautismos);
            Rutinas.FormatoGrilla(dgvConfirmaciones);
            ModoBotonesInferiores(ModoBotones.Inicial);
        }

        private void ModoBotonesInferiores(ModoBotones modo)
        {
            switch(modo)
            {
                case ModoBotones.Inicial:
                    btnAgregar.Enabled = true;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;
                    break;

                case ModoBotones.Editar:
                    btnAgregar.Enabled = false;
                    btnEditar.Enabled = false;
                    btnBorrar.Enabled = false;
                    break;

                case ModoBotones.FilaSeleccionada:
                    btnAgregar.Enabled = true;
                    btnEditar.Enabled = true;
                    btnBorrar.Enabled = true;
                    break;
            }
        }

        private void RecargarGrilla()
        {
            Busqueda();
        }

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Bautismo> lBautismos = new List<Bautismo>();
            List<Confirmacion> lConfirmaciones = new List<Confirmacion>();
            Error error = new Error();
            TabPage tab = tabControl1.SelectedTab;

            //switch(tabControl1.SelectedIndex)
            //{
            //    case 0:
            //        //Grilla dgvBautismos
            //        lBautismos = serviciosBautismos.GetAllBautismos(error);
            //        if (error.CodError > 0)
            //        {
            //            bsBautismos.DataSource = lBautismos;
            //            lblCantidadResultadosBautismo.Text = lBautismos.Count.ToString();
            //        }
            //        else
            //        {
            //            MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            //        }
            //        break;
                
            //    case 1:
            //        //Grilla dgvConfirmaciones
            //        lConfirmaciones = serviciosConfirmaciones.GetAllConfirmaciones(error);
            //        if (error.CodError > 0)
            //        {
            //            bsConfirmaciones.DataSource = lConfirmaciones;
            //            lblResultadosConfirmaciones.Text = lConfirmaciones.Count.ToString();
            //        }
            //        else
            //        {
            //            MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            //        }
            //        break;
            //}
        }

        private void btnAgregarConfirmacion_Click(object sender, EventArgs e)
        {
            vConfirmaciones vConfirmaciones = new vConfirmaciones();
            DialogResult drAgregar;
            Error error = new Error();
            Confirmacion confirmacion = new Confirmacion();
            int iIndiceGrilla;

            vConfirmaciones.modoVentana = ModoVentana.Agregar;
            vConfirmaciones.lblTitulo.Text = "Agregar certificado confirmación";
            drAgregar = vConfirmaciones.ShowDialog();

            if (drAgregar == DialogResult.OK)
            {
                confirmacion.Documento = vConfirmaciones.Documento;
                confirmacion.Apellido = vConfirmaciones.Apellido;
                confirmacion.Nombre = vConfirmaciones.Nombre;
                confirmacion.Libro = vConfirmaciones.Libro;
                confirmacion.Folio = vConfirmaciones.Folio;
                confirmacion.Fecha = vConfirmaciones.FechaConfirmacion;

                serviciosConfirmaciones.AgregarConfirmacion(confirmacion, error);

                if (error.CodError > 0)
                {
                    MessageBox2.Show(IconosVarios.Tilde, "", "El registro se agregó correctamente.", true);

                    RecargarGrilla();
                    iIndiceGrilla = ((List<Confirmacion>)bsConfirmaciones.List).FindIndex(b => b.Id == confirmacion.Id);

                    if (iIndiceGrilla > 0)
                    {
                        bsConfirmaciones.Position = iIndiceGrilla;
                    }
                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al agregar el registro.", error.Mensaje, true);
                }
            }
        }
    }
}

enum ModoBotones
{
    Inicial = 0,
    Editar = 1,
    FilaSeleccionada = 2
}