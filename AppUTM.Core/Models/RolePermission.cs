using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    public class RolePermission
    {
        [ForeignKey("Permissions")]
        public int PermissionId { get; set; }

        public Permission Permission { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}