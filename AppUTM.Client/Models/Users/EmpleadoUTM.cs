using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Client.Models.Users
{
    public class EmpleadoUTM
    {
        public string ClaveEmpleado { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string CorreoInstitucional { get; set; }
        public string NombreArea { get; set; }
        public string Departamento { get; set; }
        public string TipoEmpleado { get; set; }


    }
}
