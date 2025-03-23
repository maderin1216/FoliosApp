using FoliosApp.Comun;
using FoliosApp.Modelos;
using FoliosApp.Repositorios;
using FoliosApp.Ventanas;
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

        public List<Usuario> GetAllUsuarios(Error error)
        {
            return repositorioUsuarios.GetAllUsuarios(error);
        }

        public List<UsuarioNivel> GetAllNivelesUsuarios(Error error)
        {
            return repositorioUsuarios.GetAllNivelesUsuarios(error);
        }

        public void GrabarIngreso(string usuario, int tipo, Error error)
        {
            repositorioUsuarios.GrabarIngreso(usuario, tipo, error);
        }

        public void UpdateClave(int idUsuario, string nuevaClave, Error error)
        {
            string nuevaClaveMD5;
            Usuario usuario = new Usuario();

            usuario = repositorioUsuarios.GetUsuario(idUsuario, error);

            if (usuario != null)
            {
                nuevaClaveMD5 = Rutinas.GenerarMD5(usuario.Nombre, nuevaClave, error);

                if (error.CodError > 0)
                {
                    repositorioUsuarios.UpdateClave(idUsuario, nuevaClaveMD5, error);
                }
                else
                {
                    error.CodError = -1;
                    error.Mensaje = Rutinas.GetMensajeError("No se pudo generar nueva clave.");
                }
            }
            else
            {
                error.CodError = -1;
                error.Mensaje = Rutinas.GetMensajeError("No se encontró la información solicitada del usuario.");
            }
        }

        public void AgregarUsuario(Usuario usuario, Error error)
        {
            string sClaveMD5;

            sClaveMD5 = Rutinas.GenerarMD5(usuario.Nombre, usuario.Clave, error);

            usuario.Clave = sClaveMD5;
            repositorioUsuarios.AgregarUsuario(usuario, error);
        }

        public void ActualizarUsuario(Usuario usuario, Error error)
        {
            repositorioUsuarios.ActualizarUsuario(usuario, error);
        }

        public void BorrarUsuario(Usuario usuario, Error error)
        {
            repositorioUsuarios.BorrarUsuario(usuario, error);
        }
    }
}
