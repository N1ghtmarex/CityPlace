using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Domain
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    /// <param name="options"></param>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Адреса
        /// </summary>
        public DbSet<Address> Addresses { get; set; }
        /// <summary>
        /// Локации
        /// </summary>
        public DbSet<Location> Locations { get; set;}
        /// <summary>
        /// Пользователи
        /// </summary>
        public DbSet<User> Users { get; set; }
        /// <summary>
        /// Избранное пользователей
        /// </summary>
        public DbSet<UserFavorite> UserFavorites { get; set; }

        /// <inheritdoc/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc/>
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <inheritdoc/>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly);
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSnakeCaseNamingConvention();
        }
    }
}
