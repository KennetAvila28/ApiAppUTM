using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    public class ModuleRole
    {
        [ForeignKey("Modules")]
        public int ModuleId { get; set; }

        [JsonIgnore]
        public Module Module { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        [JsonIgnore]
        public Role Role { get; set; }
    }
}
