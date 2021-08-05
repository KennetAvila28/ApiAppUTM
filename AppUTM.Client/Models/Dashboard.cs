using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class Dashboard
    {
        public List<Empresa> Empresas { get; set; }
        public List<CuponGenerico> CuponesGenericos { get; set; }
        public List<CuponImagen> CuponesImagenes { get; set; }
        public int CuponesTotales { get; set; }
    }
}
