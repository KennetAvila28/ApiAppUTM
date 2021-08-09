using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUTM.Data.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(DbContext context) : base(context)
        {
        }
    }
}
