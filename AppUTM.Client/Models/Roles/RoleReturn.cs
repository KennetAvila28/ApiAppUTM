using System.Collections.Generic;
using AppUTM.Core.Models;

namespace AppUTM.Client.Models.Roles
{
    public class RoleReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}