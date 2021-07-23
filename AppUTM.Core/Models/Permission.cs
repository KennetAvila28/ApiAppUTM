using System.Collections.Generic;

namespace AppUTM.Core.Models
{
    public class Permission : BaseEntity
    {
        public string Module { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}