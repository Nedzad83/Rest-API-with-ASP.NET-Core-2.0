using MySql.Data.MySqlClient;
using RestAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestAPI.Data.Repositories
{
    public class EnvironmentRepository
    {
        #region Initialization

        public EnvironmentRepository()
        {
        }
        #endregion

        #region CreateEnvironment
        public String CreateEnvironment(string env, string app, string pri, string sec, string tri, string connection )
        {
            StringBuilder message = new StringBuilder();
            using (MySqlConnection conn = new MySqlConnection(connection))
            {
                using (MySqlCommand cmd = new MySqlCommand("createEnv", conn)) // call here your store procedure..
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@env", env);
                    cmd.Parameters.AddWithValue("@app", app);
                    cmd.ExecuteNonQuery();
                    using (var reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            while (reader.Read())
                            {
                                message.Append(Convert.ToString(reader["msg"]));
                            }
                        }
                        catch { }
                    }
                    conn.Close();
                }
            }
            return message.ToString();
        }
        #endregion
        
    }
}
