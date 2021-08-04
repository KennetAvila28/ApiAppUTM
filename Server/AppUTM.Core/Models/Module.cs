using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Core.Models
{
    public class Module : BaseEntity
    {
        //Todo:Create class RoleModule
        //Todo:Add relationship Many to many with RoleModule
        public string Nombre { get; set; }

        //public List<> Type { get; set; }
    }
}