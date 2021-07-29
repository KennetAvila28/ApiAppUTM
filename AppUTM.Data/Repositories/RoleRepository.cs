using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}