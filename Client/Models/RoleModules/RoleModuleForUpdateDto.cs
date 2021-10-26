namespace AppUTM.Models.RoleModules
{
    public class RoleModuleForUpdateDto
    {
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}