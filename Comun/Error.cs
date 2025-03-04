using System.Collections.Generic;

namespace FoliosApp.Comun
{
    public class Error
    {
        public int CodError { get; set; }
        public string Mensaje { get; set; }
        public List<string> Mensajes { get; set; } = new List<string>();
    }
}
