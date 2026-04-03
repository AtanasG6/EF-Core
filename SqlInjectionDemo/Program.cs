using Microsoft.Data.SqlClient;

namespace SqlInjectionDemo;

public class Program
{
    private static void Main(string[] args)
    {
        // If someone types in the username: ' OR 1=1;-- and any password, the query will become:
        // SELECT COUNT(*) FROM [Users] WHERE Username = '' OR 1=1;--' AND Password = '';
        // Or Username: '; DELETE FROM [Users];-- and any password, the query will become:
        // SELECT COUNT(*) FROM [Users] WHERE Username = ''; DELETE FROM [Users];--' AND Password = '';

        Console.Write("Enter username:");
        string username = Console.ReadLine();
        Console.Write("Enter password:");
        string password = Console.ReadLine();

        using SqlConnection sqlConnection = new SqlConnection(
        "Server=.;Database=Service;Integrated Security=true;TrustServerCertificate=True");
        {
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(
                "SELECT COUNT(*) FROM [Users] WHERE Username = '" + username
                + "' AND Password = '" + password + "';", sqlConnection);

            int usersCount = (int)sqlCommand.ExecuteScalar();

            if (usersCount > 0)
            {
                Console.WriteLine("Welcome to our secret data.");
            }
            else
            {
                Console.WriteLine("Access forbidden.");
            }

            // Solutin: Use parameterized queries to prevent SQL injection attacks.
            SqlCommand sqlCommand2 = new SqlCommand(
               "SELECT COUNT(*) FROM [Users] WHERE Username = @Username AND Password = @Password;", sqlConnection);
            sqlCommand2.Parameters.AddWithValue("@Username", username);
            sqlCommand2.Parameters.AddWithValue("@Password", password);
        }
    }
}
