using System;

namespace FoliosApp.Modelos
{
    public class Confirmacion
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int Libro { get; set; }
        public int Folio { get; set; }
        public DateTime Fecha { get; set; }
    }
}
