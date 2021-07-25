﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.DTOS.Cupones
{
    public class CuponImagenReturn
    {
        public int CuponImagenId { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public int EmpresaId { get; set; }
    }

    public class CuponImagenCreate
    {   
        public DateTime FechaExpiracion { get; set; }
        public int CuponesUsados { get; set; }
        public int CuponesVisitados { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }    
        public int EmpresaId { get; set; }
        public IFormFile Foto { get; set; }
    }
}
