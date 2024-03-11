using Dapper;
using MySqlConnector;
using SequelyMAUI.Interfaces;
using SequelyMAUI.Models;

namespace SequelyMAUI.Services
{
    internal class DbHandler : IDbHandler
    {
        public MySqlConnection? CurrentMySqlConnection { get; set; }
        public Database? CurrentDatabase { get; set; }
        public Table? CurrentTable { get; set; }

        public async Task<List<Database>> GetDatabases()
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            var dbNames = await CurrentMySqlConnection.QueryAsync<string>("SHOW DATABASES");

            return dbNames.Select(name => new Database 
            { 
                Name = name, 
                //Tables = this.GetTables(name).Result
            
            }).ToList();
        }

        public async Task<List<Table>> GetTables()
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            if(CurrentDatabase == null)
                throw new Exception("No database is selected");

            var tableNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW TABLES FROM {CurrentDatabase}");

            return tableNames.Select(name => new Table 
            { 
                Name = name,
                Database = CurrentDatabase
            }).ToList();
        }

        public async Task<List<Table>> GetTables(Database database)
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");   

            var tableNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW TABLES FROM {database.Name}");

            return tableNames.Select(name => new Table 
            { 
                Name = name,
                Database = database
            }).ToList();
        }

        public async Task<List<Column>> GetColumns()
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            if(CurrentDatabase == null)
                throw new Exception("No database is selected");

            if(CurrentTable == null)
                throw new Exception("No table is selected");

            var columnNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW COLUMNS FROM {CurrentDatabase}.{CurrentTable.Name}");

            return columnNames.Select(name => new Column { Field = name }).ToList();
        }

        public async Task<List<Column>> GetColumns(Table table)
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            if (table.Database == null)
                throw new Exception("No database is selected");

            var columnNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW COLUMNS FROM {table.Database!.Name}.{table.Name}");

            return columnNames.Select(name => new Column 
            { 
                Field = name,
                Table = table
            }).ToList();
        }



        public async Task<List<Object>> GetRows(Table table)
        {
            if (CurrentMySqlConnection == null)
                throw new Exception("No connection");

            if (table.Database == null)
                throw new Exception("No database is selected");

            var rows = await CurrentMySqlConnection.QueryAsync($"SELECT * FROM {table.Database!.Name}.{table.Name}");

            return rows.ToList();
        }


    }
}
