using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository

    {
        public PermissionRepository(DbContext context) : base(context)
        {
        }
    }
}