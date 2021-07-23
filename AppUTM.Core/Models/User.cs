using System.Collections.Generic;

namespace AppUTM.Core.Models
{
    public class User : BaseEntity
    {
        public int ClaveEmpleado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}