using AppUTM.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppUTM.Api.DTOS.Modules
{
    public class moduleToBeUpdated
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public IList<ModuleRole> ModuleRoles { get; set; }
        public int[] RolesToBeDelete { get; set; }
    }
}
