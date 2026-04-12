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
        Course course1 = dbContext.Courses.FirstOrDefault(c => c.Id == 1)!;
        course1.Name = "Atanas changed the name!!!";

        dbContext.Courses.Remove(course1);

        // If EF Core does not support sth like RANK, ROW_NUMBER, etc., we can always execute raw SQL queries 
        // dbContext.Database.ExecuteSqlRaw("DELETE FROM Courses WHERE Id = 1");

        // The ChangeTracker will track the changes we made to course1 and will generate the appropriate SQL commands to update the database when we call SaveChanges()
        Course course2 = dbContext.Courses.FirstOrDefault(c => c.Id == 2)!;
        course2.Name = "Atanas changed the name!!!";

        dbContext.SaveChanges();
    }
}
