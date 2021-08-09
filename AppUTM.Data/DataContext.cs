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

        public DbSet<Module> Modules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }


        public DbSet<ModuleRole> ModuleRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(ur => new { ur.PermissionId, ur.RoleId });
            modelBuilder.Entity<ModuleRole>().HasKey(ur => new { ur.ModuleId, ur.RoleId });

            modelBuilder.Entity<UserRole>().HasOne<Role>(r => r.Role).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<UserRole>().HasOne<User>(r => r.User).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.UserId);
            modelBuilder.Entity<RolePermission>().HasOne<Role>(r => r.Role).WithMany(m => m.RolePermissions).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<RolePermission>().HasOne<Permission>(r => r.Permission).WithMany(m => m.RolePermissions).HasForeignKey(fk => fk.PermissionId);
            modelBuilder.Entity<ModuleRole>().HasOne<Module>(r => r.Module).WithMany(m => m.ModuleRoles).HasForeignKey(fk => fk.ModuleId);
            modelBuilder.Entity<ModuleRole>().HasOne<Role>(r => r.Role).WithMany(m => m.ModuleRoles).HasForeignKey(fk => fk.RoleId);
        }
    }
}