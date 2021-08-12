using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public List<RoleModule> RoleModules { get; set; }
    }
}