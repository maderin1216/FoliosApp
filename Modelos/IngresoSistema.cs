using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoliosApp.Modelos
{
    public class IngresoSistema
    {
        public int Id { get; set; }
        public int TipoIngreso { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
