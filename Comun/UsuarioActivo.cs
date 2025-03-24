using FoliosApp.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoliosApp.Comun
{
    public static class UsuarioActivo
    {
        public static Usuario _usuario { get; set; }
        public static int NivelPermisos { get; set; }

        public static void SetUsuarioActivo(Usuario usuario)
        {
            _usuario = usuario;
            NivelPermisos = usuario.Nivel;
        }
    }
}
