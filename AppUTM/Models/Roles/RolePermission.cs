using AppUTM.Models.Modules;

namespace AppUTM.Models.Roles
{
    public class RoleModule
    {
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public ModuleReturn Module { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}