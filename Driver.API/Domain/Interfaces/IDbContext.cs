using System.Data;

namespace Driver.API.Domain.Interfaces
{
    public interface IDbContext
    {
        int Execute(string sql);
        int Execute(string sql, Dictionary<string, object> Paramters);
        DataTable GetDataTable(string sql);
        DataTable GetDataTable(string sql, Dictionary<string, object> Paramters);
    }
}
