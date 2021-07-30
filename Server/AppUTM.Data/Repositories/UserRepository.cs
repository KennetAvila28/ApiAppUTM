using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository

    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}