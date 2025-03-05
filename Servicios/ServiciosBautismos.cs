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
            return repositorioBautismos.AgregarBautismo(bautismo, error);
        }
    }
}
