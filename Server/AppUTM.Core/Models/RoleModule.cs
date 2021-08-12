using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class RoleModule
    {
        [ForeignKey("Modules")]
        public int ModuleId { get; set; }

        [JsonIgnore]
        public Module Module { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }

        public bool Read { get; set; }
        public bool Write { get; set; }
    }
}