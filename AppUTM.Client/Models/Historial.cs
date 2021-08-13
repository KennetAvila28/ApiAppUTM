using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class Historial
    {
        public int Id { get; set; }
        public string Departamento { get; set; }
        public string Matricula { get; set; }
        public int? CuponGeneridoId { get; set; }
        public int? CuponImagenId { get; set; }
    }
}
