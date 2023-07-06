using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPFApp.Models;
using WPFApp.Repositories;

namespace WPFApp.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO [Staff] ([username], [password]) VALUES (@username, @password)";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM [Staff] WHERE [username] = @username AND [password] = @password";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = credential.Password;
                validUser = (int)command.ExecuteScalar() > 0;
            }
            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE [Staff] SET [username] = @username, [password] = @password WHERE [id] = @id";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = userModel.Username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = userModel.Password;
                command.Parameters.Add("@id", SqlDbType.Int).Value = userModel.Id;
                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<UserModel> GetByAll()
        {
            List<UserModel> users = new List<UserModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Staff]";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserModel user = new UserModel()
                        {
                            Id = reader["id"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = string.Empty
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }

        public UserModel GetById(int id)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Staff] WHERE [id] = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader["id"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = string.Empty
                        };
                    }
                }
            }
            return user;
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [Staff] WHERE [username] = @username";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Id = reader["id"].ToString(),
                            Username = reader["username"].ToString(),
                            Password = string.Empty
                        };
                    }
                }
            }
            return user;
        }

        public void Remove(int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "DELETE FROM [Staff] WHERE [id] = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }
    }
}
