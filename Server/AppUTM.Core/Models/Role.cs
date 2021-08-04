using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    public sealed class Role : BaseEntity
    {
        public string Nombre { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        //Todo:Add relationship Many to many with RoleModule
    }
}