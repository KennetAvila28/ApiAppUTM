using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class ModuleRepository : Repository<Module>, IModuleRepository

    {
        public ModuleRepository(DbContext context) : base(context)
        {
        }
    }
}