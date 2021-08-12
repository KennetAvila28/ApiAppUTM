using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace AppUTM.Core.Models
{
    public sealed class Role : BaseEntity
    {
        public string Nombre { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<RoleModule> RoleModules { get; set; }
    }
}