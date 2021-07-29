using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class Cupones
    {
        public Empresa Empresa { get; set; }
        public IEnumerable<CuponGenerico> cuponesGenericos { get; set; }
        public IEnumerable<CuponImagen> cuponesImagen { get; set; }
    }
}
