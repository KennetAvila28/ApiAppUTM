using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //public bool Status { get; set; }
        //public DateTime CreateAt { get; set; }



        public List<ModuleRole> ModuleRoles { get; set; }

        [NotMapped]
        public int[] RolesToBeDelete { get; set; }
    }
}
