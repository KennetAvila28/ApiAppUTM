using AppUTM.Models.Permissions;

namespace AppUTM.Models.Roles
{
    public class RolePermission
    {
        public string Module { get; set; }
        public int PermissionId { get; set; }
        public PermissionReturn Permission { get; set; }
    }
}