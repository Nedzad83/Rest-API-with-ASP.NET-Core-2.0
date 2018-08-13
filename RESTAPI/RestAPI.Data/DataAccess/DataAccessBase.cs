using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace RestAPI.Data
{
    public class DataAccessBase
    {
        protected ConnectionType _connectionType;
        protected string _connectionString;

        protected IDbConnection GetConnection()
        {
            switch (_connectionType)
            {
                case ConnectionType.SqlServer:
                    return new SqlConnection(_connectionString);
                case ConnectionType.MySql:
                    return new MySqlConnection(_connectionString);
                default:
                    return new SqlConnection(_connectionString);
            }
        }

        protected IDbDataAdapter GetDataAdapter(IDbCommand cmd)
        {
            switch (_connectionType)
            {
                case ConnectionType.SqlServer:
                    return new SqlDataAdapter((SqlCommand)cmd);
                case ConnectionType.MySql:
                    return new MySqlDataAdapter((MySqlCommand)cmd);
                default:
                    return new SqlDataAdapter((SqlCommand)cmd);
            }
        }

    }
}
