using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Repositorios;
using System.Collections.Generic;

namespace FoliosApp.Servicios
{
    public class ServiciosUsuarios
    {
        RepositorioUsuarios repositorioUsuarios = new RepositorioUsuarios();

        public Usuario GetUsuario(string nombre, Error error)
        {
            return repositorioUsuarios.GetUsuario(nombre, error);
        }
    }
}
