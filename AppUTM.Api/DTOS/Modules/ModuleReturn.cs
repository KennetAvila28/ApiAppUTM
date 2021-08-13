using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.DTOS.Modules
{
    public class ModuleReturn
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        //public IList<ModuleRole> ModuleRoles { get; set; }
    }
}
