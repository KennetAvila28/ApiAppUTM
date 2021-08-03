using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Client.Models.Users
{
    public class UserReturn
    {
        public int Id { get; set; }
        [DisplayName("Clave")]
        public int ClaveEmpleado { get; set; }
        public string Nombres { get; set; }
        [DisplayName("Apellido Paterno")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        [DisplayName("Roles")]
        public IList<UserRole> UserRoles { get; set; }
    }
}