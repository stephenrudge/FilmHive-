using FilmHive.Models;
using FilmHive.Utills.FilmHive.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace FilmHive.Repositories

{
    public class UserRepository : BaseRepository, IUserRepository
    { 

        public UserRepository(IConfiguration configuration) : base(configuration) { }
        

        public List<User> GetUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        Id
                                        ,[name]
                                        ,email
                                        ,password
                                        From[users]";
                    var reader = cmd.ExecuteReader();

                    var users = new List<User>();
                    while (reader.Read())
                    {
                        users.Add(new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "name"),
                            Email = DbUtils.GetString(reader, "email"),
                            Password = DbUtils.GetString(reader, "password"),
                        });
                    }
                    reader.Close();
                    return users;
                }
            }
        }


        public User? GetUserByEmail(string email)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Select
                                        Id
                                        ,[name]
                                        ,email
                                        ,password
                                        From[users]
                                        Where email = @email";
                    DbUtils.AddParameter(cmd, "@email",email);
                    var reader = cmd.ExecuteReader();

                    User? user = null;
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "name"),
                            Email = DbUtils.GetString(reader, "email"),
                            Password = DbUtils.GetString(reader, "password"),
                        };
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        public void Add(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [USERS] ( name, email, password)
                    OUTPUT INSERTED.ID
                    VALUES  (@name,@email, @password)";
                    DbUtils.AddParameter(cmd, "@name", user.Name);
                    DbUtils.AddParameter(cmd, "@email", user.Email);
                    DbUtils.AddParameter(cmd, "@password", user.Password);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }



        public void Update(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE [Users]
                           SET Type = @name, @email, @password, 
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@name", user.Name);
                    DbUtils.AddParameter(cmd, "@email", user.Email);
                    DbUtils.AddParameter(cmd, "@password", user.Password);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM [Users] WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
