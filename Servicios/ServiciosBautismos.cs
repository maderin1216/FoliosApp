using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Repositorios;
using System.Collections.Generic;

namespace FoliosApp.Servicios
{
    public class ServiciosBautismos
    {
        RepositorioBautismos repositorioBautismos = new RepositorioBautismos();
        
        public List<Bautismo> GetAllBautismos(Error error)
        {
            return repositorioBautismos.GetAllBautismos(error);
        }

        public List<Bautismo> GetBautismosCriterio(CriteriosBusqueda criterio, string busqueda, Error error)
        {
            return repositorioBautismos.GetBautismosCriterio(criterio, busqueda, error);
        }

        public void AgregarBautismo(Bautismo bautismo, Error error)
        {
            repositorioBautismos.AgregarBautismo(bautismo, error);
        }

        public void EditarBautismo(Bautismo bautismo, Error error)
        {
            repositorioBautismos.EditarBautismo(bautismo, error);
        }

        public void BorrarBautismo(Bautismo bautismo, Error error)
        {
            repositorioBautismos.BorrarBautismo(bautismo, error);
        }
    }
}
