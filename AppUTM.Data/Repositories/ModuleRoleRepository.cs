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
   public class ModuleRoleRepository : Repository<ModuleRole>, IModuleRoleRepository
    {
        public async Task<IEnumerable<ModuleRole>> GetAllRoles()
        {
            return await Context.Set<ModuleRole>().Include(x => x.Role).ToListAsync();

        }

        public async Task<IEnumerable<ModuleRole>> GetAll()
        {
            return await Context.Set<ModuleRole>().Include(x => x.Module).ToListAsync();

        }

        public ModuleRoleRepository(DbContext context) : base(context)
        {

        }
    }
}
