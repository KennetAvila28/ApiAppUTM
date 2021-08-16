using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class FavoriteRepository : Repository<Favorites>, IFavoriteRepository
    {
        public FavoriteRepository(DbContext context) : base(context)
        {
        }
    }
}