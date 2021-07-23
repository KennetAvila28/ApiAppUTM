using AppUTM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<CuponImagen> CuponesImagen { get; set; }
        public DbSet<CuponGenerico> CuponesGenericos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(ur => new { ur.PermissionId, ur.RoleId });
        }
    }
}