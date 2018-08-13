using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Helpers
{
    public class QueryExecutor
    {
        #region Read Data
        DataTable ReadData(string query, ICollection<MySqlParameter> sqlParams, string conn)
        {
            var dt = new DataTable();
            
            using (var myConnection = new MySqlConnection(conn))
            {
                myConnection.Open();

                using (var myCommand = myConnection.CreateCommand())
                {
                    myCommand.CommandText = query;

                    foreach (var mySqlParameter in sqlParams)
                    {
                        myCommand.Parameters.Add(mySqlParameter);
                    }

                    using (var myReader = myCommand.ExecuteReader())
                    {
                        dt.Load(myReader, LoadOption.OverwriteChanges);
                    }
                }
            }

            return dt;
        }
        #endregion

    }
}
