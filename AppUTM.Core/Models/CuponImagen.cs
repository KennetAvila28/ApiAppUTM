using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table("CuponesImagen")]
    public class CuponImagen
    {
        [Key]
        public int CuponImagenId { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        [ForeignKey("Empresas")]
        public int EmpresaId { get; set; }

    }
}
