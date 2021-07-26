using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.DTOS.Users
{
    public class UserForUpdateDto
    {
        public int Id { get; set; }
        public int ClaveEmpleado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public string[] RoleNames { get; set; }
        public int Status { get; set; }
    }
}