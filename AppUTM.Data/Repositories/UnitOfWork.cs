using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUTM.Core;
using AppUTM.Core.Repositories;

namespace AppUTM.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DataContext Context;
        public UnitOfWork(DataContext context)
        {
            Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
