using Sigma.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sigma.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }

        // Overriding OnModelCreating to configure the entity model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensuring that the Email field is unique
            modelBuilder.Entity<Candidate>()
                .HasIndex(c => c.Email)  // Creating an index on the Email field
                .IsUnique();  // Making the index unique

            base.OnModelCreating(modelBuilder);
        }
        

    }
}
