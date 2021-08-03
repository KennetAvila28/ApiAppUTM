using System.Collections.Generic;

namespace AppUTM.Models.Users
{
    public class UserForUpdateDto
    {
        public int Id { get; set; }
        public int ClaveEmpleado { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Correo { get; set; }
        public IList<UserRole> UserRoles { get; set; }
        public int Status { get; set; }
    }
}