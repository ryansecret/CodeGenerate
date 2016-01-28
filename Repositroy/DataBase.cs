using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace Repositroy
{
    public static class Database
    {
        private static readonly string ConnectString = ConfigurationManager.ConnectionStrings["database"].ConnectionString;

        public static IDbConnection Open()
        {
            var db = new MySqlConnection(ConnectString);
            db.Open();
            return db;
        }
    }
}