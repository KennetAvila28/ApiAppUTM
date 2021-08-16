using System.Collections.Generic;

namespace AppUTM.Api.DTOS.Roles
{
    public class RoleReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IList<Core.Models.RoleModule> RoleModules { get; set; }
    }
}