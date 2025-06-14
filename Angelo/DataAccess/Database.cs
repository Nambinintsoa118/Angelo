using Npgsql;
using System.Data;

namespace Angelo.DataAccess
{
    public static class Database
    {
        private static string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=ton_mot_de_passe;Database=angelo_db";

        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
