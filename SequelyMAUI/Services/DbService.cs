using Dapper;
using MySqlConnector;
using SequelyMAUI.Entities;
using SequelyMAUI.Interfaces;
using SequelyMAUI.Models;
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
                Name = name, 
                //Tables = this.GetTables(name).Result
            
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

        public async Task<List<Dictionary<string, object>>> PerformQuery(string query)
        {
            if (CurrentMySqlConnection == null)
                throw new Exception("No connection");

            var rows = new List<Dictionary<string, object>>();

            using (var reader = await CurrentMySqlConnection.ExecuteReaderAsync(query))
            {
                var columns = new List<string>();
                columns.AddRange(Enumerable.Range(0, reader.FieldCount).Select(reader.GetName));

                while(reader.Read())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                        row.Add(reader.GetName(i), reader.GetValue(i));

                    rows.Add(row);
                }

                if(rows.Count == 0)
                    return new List<Dictionary<string, object>> { new Dictionary<string, object> { {"", columns } } };


                return rows;
            }

        }

        public async Task<DataTable> RunQuery(string query)
        {
            if (CurrentMySqlConnection == null)
                throw new Exception("No connection");

            var reader = await CurrentMySqlConnection.ExecuteReaderAsync(query);

            DataTable table = new DataTable();
            table.Load(reader);

            return table;
        }

        //public async Task<List<Dictionary<string, object>?>> PerformQuery(string query)
        //{
        //    if (CurrentMySqlConnection == null)
        //        throw new Exception("No connection");



        //    return rows.ToList();
        //}

    }
}
