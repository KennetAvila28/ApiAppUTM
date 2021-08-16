using AppUTM.Models.Modules;
using AppUTM.Models.Roles;

namespace AppUTM.Models.RoleModules
{
    public class RoleModuleReturn
    {
        public int ModuleId { get; set; }
        public ModuleReturn Module { get; set; }
        public int RoleId { get; set; }

        public RoleReturn Role { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}