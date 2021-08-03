using System.Collections.Generic;

namespace AppUTM.Models.Roles
{
    public class RoleReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}