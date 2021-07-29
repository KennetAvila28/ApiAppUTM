using System.ComponentModel.DataAnnotations.Schema;

namespace AppUTM.Core.Models
{
    public class UserRole
    {
        [ForeignKey("Users")]
        public int UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
    }
}