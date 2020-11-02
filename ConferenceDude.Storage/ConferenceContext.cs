namespace ConferenceDude.Storage
{
    using Microsoft.EntityFrameworkCore;
    using Sessions;

    public class ConferenceContext : DbContext
    {
        public ConferenceContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<PersistentSession> Sessions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersistentSession>()
                .HasIndex(s => s.Title)
                .IsUnique();
        }
    }
}