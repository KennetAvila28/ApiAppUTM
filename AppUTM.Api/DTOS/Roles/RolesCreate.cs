using System.Collections.Generic;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Roles
{
    public class RolesCreate
    {
        public string Nombre { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
    }
}