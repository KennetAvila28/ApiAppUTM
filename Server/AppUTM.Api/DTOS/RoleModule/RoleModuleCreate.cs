namespace AppUTM.Api.DTOS.RoleModule
{
    public class RoleModuleCreate
    {
        public int ModuleId { get; set; }
        public int RoleId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}