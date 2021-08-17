using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class ListEmpresas
    {
        public IEnumerable<Empresa> empresasRegistradas { get; set; }
        public IEnumerable<EmpresasUTM> empresasUTM { get; set; }

        public IEnumerable<CuponGenerico> cuponesGenericos { get; set; }
        public IEnumerable<CuponImagen> cuponesImagen { get; set; }
        public int EmpresasSinRFCsinCupones { get; set; }
    }
}
