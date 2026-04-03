using Microsoft.Data.SqlClient;

namespace AdoNetDemo;

public class Program
{
    private static void Main(string[] args)
    {
        // . -> 127.0.0.1
        // localhost -> 127.0.0.1
        // 127.0.0.1
        // DESKTOP-123456 -> 127.0.0.1
        // Integrated Security=true -> Use Windows user
        // string connectionString = "Server=.;Database=SoftUni;User Id=Atanas;Password=123456";

        // SQLConnection implements IDisposable, so we can use it in a using statement.
        // using calls sqlConnection.Dispose() -> sqlConnection.Close()
        // How does using work?
        // try{} fainally{.Dispose()} even if an exception is thrown, the finally block will be executed and the connection will be closed.

        // ExecuteNonQuery() -> returns the number of rows affected by the command (INSERT, UPDATE, DELETE)
        // ExecuteScalar() -> returns the first column of the first row in the result set, or a null reference if the result set is empty. It is used when you want to retrieve a single value from the database, such as an aggregate value (e.g., COUNT, SUM) or a specific field.
        // ExecuteReader() -> returns a data reader, which can be used to read multiple rows and columns

        using SqlConnection sqlConnection = new SqlConnection(
            "Server=.;Database=SoftUni;Integrated Security=true;TrustServerCertificate=True");
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "SELECT COUNT(*) FROM [Employees]", sqlConnection);
            // object result = sqlCommand.ExecuteScalar();
            int result = (int)sqlCommand.ExecuteScalar();
            Console.WriteLine(result);

            SqlCommand sqlCommand2 = new SqlCommand(
                "SELECT [FirstName], [LastName], [Salary] FROM [Employees] WHERE FirstName LIKE 'A%'", sqlConnection);

            using (SqlDataReader reader = sqlCommand2.ExecuteReader())
            {

                while (reader.Read())
                {
                    // string firstName = (string)reader[0];
                    // string lastName = (string)reader[1];

                    // reader["FirstName"] = "Nasko" cannot be done because the data reader is read-only
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    decimal salary = (decimal)reader["Salary"];
                    Console.WriteLine(firstName + " " + lastName + " => " + salary);
                }
            }

            SqlCommand updateSalaryCommand = new SqlCommand(
                "UPDATE [Employees] SET Salary = Salary * 1.1", sqlConnection);

            int updateRows = updateSalaryCommand.ExecuteNonQuery();
            Console.WriteLine($"Salary updated for {updateRows} employee/s.");

            var reader2 = sqlCommand2.ExecuteReader();
            using (reader2)
            {
                while (reader2.Read())
                {
                    string firstName = (string)reader2["FirstName"];
                    string lastName = (string)reader2["LastName"];
                    decimal salary = (decimal)reader2["Salary"];
                    Console.WriteLine(firstName + " " + lastName + " => " + salary);
                }
            }
        }
    }
}
