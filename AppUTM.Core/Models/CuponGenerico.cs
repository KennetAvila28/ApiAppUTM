using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    [Table("CuponesGenericos")]
    public class CuponGenerico
    {
        [Key]
        public int CuponGeneridoId { get; set; }

        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public int PorcentajeDescuento { get; set; }
        public int NumeroPorPersona { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("Empresas")]
        public int EmpresaId { get; set; }
    }
}