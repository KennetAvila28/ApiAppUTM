using AppUTM.Core.Models;
using System.Collections.Generic;

namespace AppUTM.Api.DTOS.Users
{
    public class UserReturn
    {
        public int Id { get; set; }
        public int ClaveEmpleado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public IList<UserRole> UserRoles { get; set; }
    }
}