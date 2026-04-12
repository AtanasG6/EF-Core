using DbFirstApproach.Models;

namespace DbFirstApproach;

public class Program
{
    public static void Main(string[] args)
    {
        // dotnet ef dbcontext scaffold "Server=.;Database=Softuni;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
        var dbContext = new SoftuniContext();
        var employees = dbContext.Employees.ToList();
        foreach (var employee in employees) 
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");
    }
}
