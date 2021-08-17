using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public sealed class Role : BaseEntity
    {
        public string Nombre { get; set; }

        [IgnoreDataMember]
        public IList<UserRole> UserRoles { get; set; }

        public IList<RoleModule> RoleModules { get; set; }
    }
}