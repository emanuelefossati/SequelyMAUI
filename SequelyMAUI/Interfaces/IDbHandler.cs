using MySqlConnector;
using SequelyMAUI.Models;

namespace SequelyMAUI.Interfaces
{
    internal interface IDbHandler
    {
        public MySqlConnection? CurrentMySqlConnection { get; set; }
        public Database? CurrentDatabase { get; set; }
        public Table? CurrentTable { get; set; }


        public Task<List<Database>> GetDatabases();
        public Task<List<Table>> GetTables();
        public Task<List<Table>> GetTables(Database database);
        public Task<List<Column>> GetColumns();
        public Task<List<Column>> GetColumns(Table table);


        public Task<List<Object>> GetRows(Table table);
    }
}