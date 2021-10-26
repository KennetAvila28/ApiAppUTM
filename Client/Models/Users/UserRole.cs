using AppUTM.Models.Roles;

namespace AppUTM.Models.Users
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string Nombre { get; set; }
        public RoleReturn Role { get; set; }
    }
}