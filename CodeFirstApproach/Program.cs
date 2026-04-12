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

        // ! - null forgiving operator, because we are sure that there is a course with Id = 1
        Course course = dbContext.Courses.FirstOrDefault(c => c.Id == 1)!;
        course.Name = "Atanas changed the name!!!";

        dbContext.Courses.Remove(course);

        dbContext.SaveChanges();
    }
}
