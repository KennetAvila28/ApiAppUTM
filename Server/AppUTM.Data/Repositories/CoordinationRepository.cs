using AppUTM.Core.Models;
using AppUTM.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data.Repositories
{
    public class CoordinationRepository : Repository<Coordination>, ICoordinationsRepository
    {
        public CoordinationRepository(DbContext context) : base(context)
        {
        }
    }
}