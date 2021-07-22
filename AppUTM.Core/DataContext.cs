using System;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Core
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }
    }
}
