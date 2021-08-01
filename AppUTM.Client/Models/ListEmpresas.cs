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
    }
}
