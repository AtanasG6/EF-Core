using CodeFirstApproach.Models;

namespace CodeFirstApproach;

public class Program
{
    public static void Main(string[] args)
    {
        var dbContext = new StudentsDbContext();
        dbContext.Database.EnsureCreated();
        dbContext.Courses.AddRange(
            new Course { Name = "Entity Framework Core" },
            new Course { Name = "SQL Server" }
            );
        dbContext.SaveChanges();
    }
}
