using MySqlConnector;
using SequelyMAUI.Entities;
using System.Data;

namespace SequelyMAUI.Interfaces
{
    internal interface IDbService
    {
        public MySqlConnection? CurrentMySqlConnection { get; set; }
        public DatabaseEntity? CurrentDatabase { get; set; }
        public TableEntity? CurrentTable { get; set; }


        public Task<List<DatabaseEntity>> GetDatabases();
        public Task<List<TableEntity>> GetTables();
        public Task<List<TableEntity>> GetTables(DatabaseEntity database);
        public Task<DataTable?> RunQueryAsync(string query);
        public DataTable RunQuery(string query);
    }
}