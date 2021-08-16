using AppUTM.Core.Models;

namespace AppUTM.Api.DTOS.RoleModule
{
    public class RoleModuleReturn
    {
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        public int RoleId { get; set; }

        public Role Role { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}