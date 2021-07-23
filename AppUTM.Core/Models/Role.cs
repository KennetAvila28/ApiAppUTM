using System.Collections.Generic;

namespace AppUTM.Core.Models
{
    public class Role : BaseEntity
    {
        public string Nombre { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}