using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class Module : BaseEntity
    {
        public string Name { get; set; }

        [IgnoreDataMember]
        public IList<RoleModule> RoleModules { get; set; }
    }
}