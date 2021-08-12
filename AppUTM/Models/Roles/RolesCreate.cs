using System.Collections.Generic;

namespace AppUTM.Models.Roles
{
    public class RolesCreate
    {
        public string Nombre { get; set; }
        public IList<RoleModule> RoleModules { get; set; }
    }
}