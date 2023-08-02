using Microsoft.EntityFrameworkCore;

namespace VentionTask.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(i => i.UserName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(i => i.UserIdentifier)
                .IsUnique();
        }
    }
}
