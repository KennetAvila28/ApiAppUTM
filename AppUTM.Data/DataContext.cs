using System;
using AppUTM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {   }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<CuponImagen> CuponesImagen { get; set; }
        public DbSet<CuponGenerico> CuponesGenericos { get; set; }

    }
}
