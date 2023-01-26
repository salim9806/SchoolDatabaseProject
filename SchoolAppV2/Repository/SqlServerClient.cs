using DataAccess;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository
{
    public class SqlServerClient:ISqlServerClient
    {
        private string? _connectionString;
        private SqlConnection _connection;

        public SqlServerClient(ApplicationDbContext dbContext)
        {
            _connectionString = dbContext.Database.GetConnectionString();
            _connection = new SqlConnection(_connectionString);
        }


        public SqlDataReader Sql(string query)
        {
            SqlCommand command = new SqlCommand(query, _connection);
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();

            return command.ExecuteReader();
        }
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void NonQuery(string sql)
        {
            SqlCommand command = new SqlCommand(sql, _connection);
            if (_connection.State == System.Data.ConnectionState.Closed)
                _connection.Open();

            command.ExecuteNonQuery();
        }
    }
}
