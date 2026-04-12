using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach.Models;

public class StudentsDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.
            UseSqlServer("Server=.;Database=StudentsDb;Integrated Security=True;TrustServerCertificate=True;");
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
