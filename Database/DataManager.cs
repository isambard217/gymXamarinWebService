using WeActiveLibrary.Models;
using WeActiveLibrary.Database;
using WeActiveWebservice.Models.Local;
using Microsoft.Extensions.Options;
using NetCoreLibrary.Data.MySql;
using System;
using MySql.Data.MySqlClient;

namespace WeActiveWebservice.Database {

    public class DataManager : CommonDataManager {

        private readonly ConnectionStrings _connectionStrings;

        private string connectionString;

        public DataManager(IOptions<ConnectionStrings> options) {

            this._connectionStrings = options.Value;
        }

        public DataManager(string ConnectionString) : base(ConnectionString) {


            connectionString = ConnectionString;
        }

        public Gym GetGym (int userId) {

            Gym gym = new Gym();

            using (MySqlConnection connection = new MySqlConnection(connectionString)) {

                connection.Open();

                string query = "SELECT g.* FROM Gym g JOIN gym_user gu ON g.Id = gu.GymId WHERE gu.UserId = @userId";

                MySqlCommand mysqlcommand = new MySqlCommand(query, connection);

                mysqlcommand.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader mySqlDataReader = mysqlcommand.ExecuteReader();

                if (mySqlDataReader.HasRows) {

                    while (mySqlDataReader.Read()) {
                        
                        gym.Id = mySqlDataReader.GetInt32(mySqlDataReader.GetOrdinal("Id"));
                        gym.Name = mySqlDataReader.GetString(mySqlDataReader.GetOrdinal("Name"));
                    }
                }

                mySqlDataReader.Close();

            }

            return gym;

        }

        public string RegisterUser(User user){

            /* using (MySqlConnection connection = new MySqlConnection()) {

                 connection.Open();

                 string query = "INSERT INTO user VALUE ()";

             }*/
            return "";

        }
        public User GetUser(string email) {
                    
                User user = new User();
        
				using (MySqlConnection connection = new MySqlConnection(connectionString)) {

                    connection.Open();

                    string query = @"SELECT * FROM 
                                     user WHERE email 
                                     = @email";

                    MySqlCommand mysqlcommand = new MySqlCommand(query, connection);

                    mysqlcommand.Parameters.AddWithValue("@email", email);

                    MySqlDataReader mysqlreader = mysqlcommand.ExecuteReader();

                    if (mysqlreader.HasRows == true) {
                                        
                        while (mysqlreader.Read()) {
                            
                            user.Id = mysqlreader.GetInt32(mysqlreader.GetOrdinal("id"));
                            user.Email = mysqlreader.GetString(mysqlreader.GetOrdinal("Email"));
                            user.Password = mysqlreader.GetString(mysqlreader.GetOrdinal("Password"));

                        }

                    }
                    
                    mysqlreader.Close();

                }

            return user;
        }

        public bool AddSession(Session session) {

			string query = @"INSERT INTO
                                     Session(SessionKey) 
                                  VALUES(@sessionKey);
                                ";
            using (MySqlConnection mysqlconnection = new MySqlConnection()) {

                MySqlCommand command = new MySqlCommand(query, mysqlconnection);

                command.Parameters.AddWithValue("@sessionKey", session.SessionKey);

                  int numberOfRowAffected = command.ExecuteNonQuery();

                MySqlDataReader mysqlDataReader = command.ExecuteReader();

                if (mysqlDataReader.HasRows == true) {

                    while (mysqlDataReader.Read()) { 
                    
                    
                    }

                }
            }

        }

        public Session GetActiveSession(int userId) {

            Session ActiveSession = new Session();

            using (MySqlConnection connection = new MySqlConnection()) {

                string query = @"SELECT * 
                                FROM Session s
                                JOIN User u ON s.userId = u.Id
                                WHERE s.Id = @id";

                using (MySqlConnection mySqlConnection = new MySqlConnection()) {

                    MySqlCommand command = new MySqlCommand(query, mySqlConnection);

                    command.Parameters.AddWithValue("@id", userId);

                    MySqlDataReader mysqlDataReader = command.ExecuteReader();

                    if (mysqlDataReader.HasRows == true) {

                        while (mysqlDataReader.Read()) {

                            ActiveSession.User.Id = mysqlDataReader.GetInt32(mysqlDataReader.GetOrdinal("UserId"));
                            ActiveSession.SessionKey = mysqlDataReader.GetString(mysqlDataReader.GetOrdinal("SessionKey"));

                        }
                    } 
                    mysqlDataReader.Close();
                }

            }

            return ActiveSession;

		}



            /* public User GetUser(string email) {

                 string queryString = @"SELECT * 
                                          FROM user 
                                          WHERE Email = @email 
                                          AND Deleted = 0";

                 using (SqlConnection conn = new SqlConnection(this.connectionString)) {

                     SqlCommand command = new SqlCommand(queryString, conn);
                     command.Parameters.AddWithValue("@email", "Your-Parm-Value");
                     conn.Open();

                     SqlDataReader reader = command.ExecuteReader();

                     try {

                         while (reader.Read()) {

                             Console.WriteLine(String.Format("{0}, {1}",
                             reader["tPatCulIntPatIDPk"], reader["tPatSFirstname"]));// etc
                         }

                     } catch (Exception e) {

                         Console.WriteLine(e.ToString());

                     } finally {

                         reader.Close();

                         // Always call Close when done reading.
                     }
                     return new User();

                 }

             }*/




            /*public User GetUser(string email) {


                using (MySqlDataManager mySqlDataManager = new MySqlDataManager()) {

                    mySqlDataManager.ConnectionString = _connectionStrings.DefaultConnection;


                    string query = @"SELECT * 
                                    FROM user
                                    WHERE name=@email";

                    mySqlDataManager.mySqlCommandParameterSet.ClearParameters();
                    mySqlDataManager.mySqlCommandParameterSet.AddParameter("@email", email);


                    return new User();
                    //MySqlDataReader reader = mySqlDataManager.Ex

                }*/









            /*public string GetActiveSession(int id) {

                try {

                    using (MySqlDataManager mySqlDataManager = new MySqlDataManager()) {

                        mySqlDataManager.ConnectionString = GetConnectionString();

                    }

                } catch {

                }
            }*/

        
    }
}

