using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AppUTM.Core.Models
{
    public class UserRole
    {
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        [JsonIgnore]
        public Role Role { get; set; }
    }
}