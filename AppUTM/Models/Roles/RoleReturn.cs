using System.Collections.Generic;

namespace AppUTM.Client.Models.Roles
{
    public class RoleReturn
    {
        public string Nombre { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}