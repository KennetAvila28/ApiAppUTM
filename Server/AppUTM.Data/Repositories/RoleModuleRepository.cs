using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class RoleModuleRepository : Repository<RoleModule>, IRoleModuleRepository
    {
        public RoleModuleRepository(DbContext context) : base(context)
        {
        }
    }
}