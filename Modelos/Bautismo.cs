using System;

namespace FoliosApp.Modelos
{
    public class Bautismo
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public int Libro { get; set; }
        public int Folio { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public DateTime FechaBautismo { get; set; }
        public DateTime FechaCarga { get; set; }
        public DateTime FechaEdicion { get; set; }
    }
}
