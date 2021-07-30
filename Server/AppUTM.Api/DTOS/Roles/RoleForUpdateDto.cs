using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.Roles
{
    public class RoleForUpdateDto
    {
        public string Nombre { get; set; }
        public IList<RolePermission> RolePermissions { get; set; }
        public int[] PermissionsToBeDelete { get; set; }
    }
}