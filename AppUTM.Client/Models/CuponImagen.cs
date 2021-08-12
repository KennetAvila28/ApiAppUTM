using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class CuponImagen
    {
        public int CuponImagenId { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int EmpresaId { get; set; }
        public IFormFile Foto { get; set; }
        public string Domain { get; set; }
        public string NombreEmpresa { get; set; }
        public bool Activa { get; set; }
    }
}
