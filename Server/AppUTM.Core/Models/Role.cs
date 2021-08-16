using System.Collections.Generic;

namespace AppUTM.Core.Models
{
    public sealed class Role : BaseEntity
    {
        public string Nombre { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RoleModule> RoleModules { get; set; }
    }
}