using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    public sealed class Role : BaseEntity
    {
        public string Nombre { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        public List<ModuleRole> ModuleRoles{ get; set; }

        [NotMapped]
        public int[] PermissionsToBeDelete { get; set; }
    }
}