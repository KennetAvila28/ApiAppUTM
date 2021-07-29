using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class CuponGenerico
    {
        public int CuponGeneridoId { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public int PorcentajeDescuento { get; set; }
        public int NumeroPorPersona { get; set; }
        public string Descripcion { get; set; }
        public int EmpresaId { get; set; } 
        public string NombreEmpresa { get; set; }
    }
}
