using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table("HistorialUsoCupones")]
    public class HistorialCupones
    {
        [Key]
        public int Id { get; set; }

        public string Departamento { get; set; }
        public string Matricula { get; set; }


        [ForeignKey("CuponesGenericos")]
        public int? CuponGeneridoId { get; set; }
        
        [ForeignKey("CuponesImagen")]
        public int? CuponImagenId { get; set; }
    }
}
