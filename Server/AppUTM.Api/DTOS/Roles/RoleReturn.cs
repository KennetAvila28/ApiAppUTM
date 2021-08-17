using System.Collections.Generic;
using AppUTM.Api.DTOS.RoleModule;

namespace AppUTM.Api.DTOS.Roles
{
    public class RoleReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<RoleModuleReturn> RoleModules { get; set; }
    }
}