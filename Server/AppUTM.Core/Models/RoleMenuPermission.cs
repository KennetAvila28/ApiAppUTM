using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table(name: "RoleMenuPermission")]
    public class RoleMenuPermission
    {
        public int RoleId { get; set; }

        public Guid NavigationMenuId { get; set; }

        public NavigationMenu NavigationMenu { get; set; }
    }
}