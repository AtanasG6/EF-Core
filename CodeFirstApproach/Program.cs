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

        bool exists = dbContext.Courses.Any(c => c.Name.StartsWith("Entity"));
        int coursesCount = dbContext.Courses.Count();

        // ! - null forgiving operator, because we are sure that there is a course with Id = 1
        Course course1 = dbContext.Courses.FirstOrDefault(c => c.Id == 1)!;
        course1.Name = "Atanas changed the name!!!";

        dbContext.Courses.Remove(course1);

        // If EF Core does not support sth like RANK, ROW_NUMBER, etc., we can always execute raw SQL queries 
        // dbContext.Database.ExecuteSqlRaw("DELETE FROM Courses WHERE Id = 1");

        // The ChangeTracker will track the changes we made to course1 and will generate the appropriate SQL commands to update the database when we call SaveChanges()
        Course course2 = dbContext.Courses.FirstOrDefault(c => c.Id == 2)!;
        course2.Name = "Atanas changed the name!!!";

        // Using the navigational property 
        dbContext.Students.Where(x => x.Grades.Average(g => g.Value) > 4.50M);

        // Old Query Syntax
        var courses = from c in dbContext.Courses
                      where c.Name.StartsWith("Entity")
                      select c;

        // New Method Syntax
        var courses2 = dbContext.Courses.Where(c => c.Name.StartsWith("Entity"));

        // This will insert 3 new records in the database
        // 1. A new Student with FirstName = "Stoyan" and LastName = "Shopov"
        // 2. A new Course with Name = "C# OOP"
        // 3. A new Grade with Value = 5.50M, StudentId = the Id of the newly created Student and CourseId = the Id of the newly created Course
        dbContext.Grades.Add(new Grade
        {
            Student = new Student { FirstName = "Stoyan", LastName = "Shopov" },
            Course = new Course { Name = "C# OOP" },
            Value = 5.50M
        });

        var student = dbContext.Students.FirstOrDefault();
        student.FirstName = "Test";
        var student2 = dbContext.Students.FirstOrDefault();
        Console.WriteLine(student2.FirstName); // This will print "Test" because student and student2 are the same object in memory, they are both tracked by the ChangeTracker, and they both reference the same record in the database. When we change the FirstName property of student, it also changes the FirstName property of student2 because they are the same object.

        // Alternative
        // var student2 = new StudentsDbContext().Students.FirstOrDefault();

        dbContext.SaveChanges();
    }
}
