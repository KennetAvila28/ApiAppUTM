using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.DTOS.HistorialCupones
{
    public class HistorialCuponesReturn
    {    
        public int Id { get; set; }
        public string Departamento { get; set; }
        public string Matricula { get; set; }
        public int? CuponGeneridoId { get; set; }
        public int? CuponImagenId { get; set; }
    }
}
