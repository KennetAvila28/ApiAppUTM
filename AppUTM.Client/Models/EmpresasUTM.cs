using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models
{
    public class EmpresasUTM
    {
        public string IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string CorreoEmpresa { get; set; }
        public string RFC { get; set; }
        public string PersonaResponsable { get; set; }

        public IList<Empresa> empresas { get; set; }
    }
}
