using Microsoft.Data.SqlClient;

namespace Repository
{
    public interface ISqlServerClient:IDisposable
    {
        SqlDataReader Sql(string query);
        void NonQuery(string sql);
        public void CloseConnection();
    }
}