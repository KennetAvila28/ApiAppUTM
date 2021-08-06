using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    [Table(name: "NavegationMenu")]
    public class NavigationMenu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("ParentNavigationMenu")]
        public Guid? ParentMenuId { get; set; }

        public virtual NavigationMenu ParentNavigationMenu { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public int DisplayOrder { get; set; }

        [NotMapped]
        public bool Permitted { get; set; }

        public bool Visible { get; set; }
    }
}