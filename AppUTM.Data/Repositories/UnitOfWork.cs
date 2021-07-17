using System.Threading.Tasks;
using AppUTM.Core.Repositories;
using AppUTM.Data;

namespace AppUTM.Api.AppUTM.DATA.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        protected readonly DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
