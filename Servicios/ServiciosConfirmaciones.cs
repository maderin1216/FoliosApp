using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Repositorios;
using System.Collections.Generic;

namespace FoliosApp.Servicios
{
    public class ServiciosConfirmaciones
    {
        RepositorioConfirmaciones repositorioConfirmaciones = new RepositorioConfirmaciones();
        
        public List<Confirmacion> GetAllConfirmaciones(Error error)
        {
            return repositorioConfirmaciones.GetAllConfirmaciones(error);
        }

        public List<Confirmacion> GetBautismosCriterio(CriteriosBusqueda criterio, string busqueda, Error error)
        {
            return repositorioConfirmaciones.GetConfirmacionesCriterio(criterio, busqueda, error);
        }

        public void AgregarConfirmacion(Confirmacion confirmacion, Error error)
        {
            repositorioConfirmaciones.AgregarConfirmacion(confirmacion, error);
        }

        public void EditarConfirmacion(Confirmacion confirmacion, Error error)
        {
            repositorioConfirmaciones.EditarConfirmacion(confirmacion, error);
        }

        public void BorrarConfirmacion(Confirmacion confirmacion, Error error)
        {
            repositorioConfirmaciones.BorrarConfirmacion(confirmacion, error);
        }
    }
}
