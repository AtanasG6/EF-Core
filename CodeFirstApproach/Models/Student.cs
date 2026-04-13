namespace CodeFirstApproach.Models;

public class Student
{
    // Convention over Configuration
    // PRIMARY KEY
    // IDENTITY (1, 1)
    // same as publi int StudentId { get; set; }
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public ICollection<Grade> Grades { get; set; }
}
