using Microsoft.Data.Sqlite;
using Todo.Models.Entities;
using Todo.Models.Interfaces;
using Todo.Models.Repositories.Interfaces;
using System.Data.SQLite;

namespace Todo.Models.Repositories
{
    public class SQLiteUserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public SQLiteUserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void AddUser(IUser user)
        {
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Id, Name) VALUES (@id, @name)";
            cmd.Parameters.AddWithValue("@id", user.Id);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.ExecuteNonQuery();
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            var users = new List<IUser>();
            using var conn = new SQLiteConnection(_connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name FROM Users";
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new VisitorUser(reader.GetInt32(0), reader.GetString(1)));
            }
            return users;
        }
    }

}
