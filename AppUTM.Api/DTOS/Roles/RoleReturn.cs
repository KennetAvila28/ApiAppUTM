using System.Collections.Generic;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Roles
{
    public class RoleReturn
    {
        public string Nombre { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}