using Dapper;
using MySqlConnector;
using SequelyMAUI.Entities;
using SequelyMAUI.Interfaces;
using System.Data;
using System.Diagnostics;

namespace SequelyMAUI.Services
{
    internal class DbService : IDbService
    {
        public MySqlConnection? CurrentMySqlConnection { get; set; }
        public DatabaseEntity? CurrentDatabase { get; set; }
        public TableEntity? CurrentTable { get; set; }

        public async Task<List<DatabaseEntity>> GetDatabases()
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            var dbNames = await CurrentMySqlConnection.QueryAsync<string>("SHOW DATABASES");

            return dbNames.Select(name => new DatabaseEntity 
            { 
                Name = name
            
            }).ToList();
        }

        public async Task<List<TableEntity>> GetTables()
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");

            if(CurrentDatabase == null)
                throw new Exception("No database is selected");

            var tableNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW TABLES FROM {CurrentDatabase}");

            return tableNames.Select(name => new TableEntity 
            { 
                Name = name,
                Database = CurrentDatabase
            }).ToList();
        }

        public async Task<List<TableEntity>> GetTables(DatabaseEntity database)
        {
            if(CurrentMySqlConnection == null)
                throw new Exception("No connection");   

            var tableNames = await CurrentMySqlConnection.QueryAsync<string>($"SHOW TABLES FROM {database.Name}");

            return tableNames.Select(name => new TableEntity 
            { 
                Name = name,
                Database = database
            }).ToList();
        }

        public async Task<DataTable?> RunQueryAsync(string query)
        {
            if (CurrentMySqlConnection == null)
                throw new Exception("No connection");

            try
            {
                var reader = await CurrentMySqlConnection.ExecuteReaderAsync(query);

                DataTable table = new DataTable();
                table.Load(reader);

                return table;
            }
            catch (Exception e)
            {
                await Application.Current!.MainPage!.DisplayAlert("Error", e.Message, "OK");
                return null;

            }
        }

        public DataTable RunQuery(string query)
        {
            if (CurrentMySqlConnection == null)
                throw new Exception("No connection");

            var reader = CurrentMySqlConnection.ExecuteReader(query);

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }
    }
}
