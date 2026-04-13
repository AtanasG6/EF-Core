using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach.Models;

// The DbContext class relates to the Unit of Work pattern.
public class StudentsDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.
            UseSqlServer("Server=.;Database=StudentsDb;Integrated Security=True;TrustServerCertificate=True;");
    }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Fluent API configuration
        modelBuilder.Entity<Student>(e =>
        {
            e.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);
        });
    }

    // The DbSet relates to the Repository pattern.
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
