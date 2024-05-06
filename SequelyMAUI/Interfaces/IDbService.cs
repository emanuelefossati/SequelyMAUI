using MySqlConnector;
using SequelyMAUI.Entities;
using SequelyMAUI.Models;
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
        public Task<List<Column>> GetColumns();
        public Task<List<Column>> GetColumns(Table table);


        public Task<List<Object>> GetRows(Table table);

        public Task<List<Dictionary<string, object>>> PerformQuery(string query);
        public Task<DataTable?> RunQueryAsync(string query);
        public DataTable RunQuery(string query);
    }
}