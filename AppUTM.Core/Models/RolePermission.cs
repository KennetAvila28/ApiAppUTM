using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class RolePermission
    {
        [ForeignKey("Permissions")]
        public int PermissionId { get; set; }
        [JsonIgnore]
        public Permission Permission { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        [JsonIgnore]

        public Role Role { get; set; }
    }
}