using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using ExerciseDapper.Models;

namespace ExerciseDapper.Repositories;

public class UserRepository
{
    private readonly string _connectionString;

    public UserRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<bool> InsertUserAsync(Users user)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Users (Name, Age) VALUES (@Name, @Age)";
            var result = await db.ExecuteAsync(query, new { user.Name, user.Age });
            return result > 0;
        }
    }
}
