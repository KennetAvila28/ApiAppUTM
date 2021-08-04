using AppUTM.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AppUTM.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<EventFavorite> EventFavorites { get; set; }
        public DbSet<Coordination> Coordinations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RolePermission>().HasKey(ur => new { ur.PermissionId, ur.RoleId });
            modelBuilder.Entity<EventFavorite>().HasKey(ur => new { ur.EventId, ur.FavoriteId });
            modelBuilder.Entity<Event>().HasOne(s => s.Author).WithMany(g => g.Events).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<Coordination>().HasMany(g => g.Events).WithOne(s => s.Author).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<UserRole>().HasOne(r => r.Role).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(r => r.User).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.UserId);
            modelBuilder.Entity<RolePermission>().HasOne(r => r.Role).WithMany(m => m.RolePermissions).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<RolePermission>().HasOne(r => r.Permission).WithMany(m => m.RolePermissions).HasForeignKey(fk => fk.PermissionId);

            modelBuilder.Entity<EventFavorite>().HasOne(r => r.Event).WithMany(m => m.EventFavorite).HasForeignKey(fk => fk.EventId);
            modelBuilder.Entity<EventFavorite>().HasOne(r => r.Favorite).WithMany(m => m.EventsfFavorites).HasForeignKey(fk => fk.FavoriteId);
            modelBuilder.Entity<Coordination>()
                .HasMany(g => g.Events)
                .WithOne(s => s.Author)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}