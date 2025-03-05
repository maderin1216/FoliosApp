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

            drAgregar = vBautismos.ShowDialog();

            if (drAgregar == DialogResult.OK)
            {
                bautismo.Documento = vBautismos.Documento;
                bautismo.Apellido = vBautismos.Apellido;
                bautismo.Nombre = vBautismos.Nombre;
                bautismo.Libro = vBautismos.Libro;
                bautismo.Folio = vBautismos.Folio;

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

        private void Principal_Load(object sender, EventArgs e)
        {
            Error error = new Error();
            List<Bautismo> lBautismos = new List<Bautismo>();
            List<dynamic> lCriterios;

            //Grilla
            lBautismos = serviciosBautismos.GetAllBautismos(error);
            if(error.CodError > 0)
            {
                bsBautismos.DataSource = lBautismos;
                lblCantidadResultados.Text = lBautismos.Count.ToString();
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
                lblCantidadResultados.Text = lBautismos.Count.ToString();
            }
            else
            {
                MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al obtener información.", error.Mensaje, true);
            }
        }

        private void Inicio()
        {
            Rutinas.FormatoGrilla(dgvBautismos);
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
    }
}

enum ModoBotones
{
    Inicial = 0,
    Editar = 1,
    FilaSeleccionada = 2
}