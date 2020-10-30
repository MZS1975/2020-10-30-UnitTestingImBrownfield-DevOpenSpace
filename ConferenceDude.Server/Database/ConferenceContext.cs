using Microsoft.EntityFrameworkCore;

namespace ConferenceDude.Server.Database
{
    public class ConferenceContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=App_Data/conference.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>()
                .HasIndex(s => s.Title)
                .IsUnique();
        }
    }
}