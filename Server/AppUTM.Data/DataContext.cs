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
        public DbSet<RoleModule> RoleModules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Coordination> Coordinations { get; set; }
        public DbSet<EventFavorites> EventFavorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<RoleModule>().HasKey(ur => new { ur.ModuleId, ur.RoleId });
            modelBuilder.Entity<Event>().HasOne(s => s.Author).WithMany(g => g.Events).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<Coordination>().HasMany(g => g.Events).WithOne(s => s.Author).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<UserRole>().HasOne(r => r.Role).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(r => r.User).WithMany(m => m.UserRoles).HasForeignKey(fk => fk.UserId);
            modelBuilder.Entity<RoleModule>().HasOne(r => r.Role).WithMany(m => m.RoleModules).HasForeignKey(fk => fk.RoleId);
            modelBuilder.Entity<RoleModule>().HasOne(r => r.Module).WithMany(m => m.RoleModules).HasForeignKey(fk => fk.ModuleId);
            modelBuilder.Entity<Coordination>()
                .HasMany(g => g.Events)
                .WithOne(s => s.Author)
                .HasForeignKey(s => s.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorites>().HasMany(p => p.Events).WithMany(e => e.Favorites)
                .UsingEntity<EventFavorites>(
                    ef => ef.HasOne(prop => prop.Event)
                        .WithMany().HasForeignKey(prop => prop.EventId),
                    ef => ef.HasOne(prop => prop.Favorites)
                        .WithMany().HasForeignKey(prop => prop.FavoriteId),
                    ef =>
                    {
                        ef.HasKey(prop => new { prop.FavoriteId, prop.EventId });
                    }
                );
        }
    }
}