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
            ModoBotonesInferiores(ModoBotones.FilaSeleccionada);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            vBautismos vBautismos = new vBautismos();
            DialogResult drAgregar;
            Error error = new Error();
            Bautismo bautismo = new Bautismo();

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
                    MessageBox2.Show(IconosVarios.Tilde, "", "El registro se agregó correctamente.");


                }
                else
                {
                    MessageBox2.Show(IconosVarios.Error, "Ocurrió un error al agregar el registro.", error.Mensaje, true);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Busqueda();
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
        #endregion
    }
}

enum ModoBotones
{
    Inicial = 0,
    Editar = 1,
    FilaSeleccionada = 2
}