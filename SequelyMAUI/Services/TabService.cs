using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SequelyMAUI.Entities;
using SequelyMAUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Services
{
    internal class TabService(SQLiteContext context, IMemoryCache memoryCache, IConnectionService connectionService, IDbService dbService) : ITabService
    {
        private readonly SQLiteContext _Context = context;
        private readonly IConnectionService _ConnectionService = connectionService;
        private readonly IMemoryCache _MemoryCache = memoryCache;
        private readonly IDbService _DbService = dbService;

        public IQueryable<TabEntity> TabEntities => _Context.Tabs.Where(x => x.Connection == _ConnectionService.CurrentConnection!);
        public List<TabEntity> Tabs { get; set; } = [];

        public int SelectedTabPanelIndex { get; set; }

        public async Task CreateTab(SqlEntity entity)
        {
            TabEntity tab;

            if (entity is DatabaseEntity)
                tab = await CreateDatabaseTab((entity as DatabaseEntity)!);

            else if (entity is TableEntity)
                tab = await CreateTableTab((entity as TableEntity)!);

            else
                throw new Exception("SqlEntity must be either a database or a table");

            if (tab.State.Data == null)
                return;

            foreach (DataRow dataRow in tab.State.Data!.Rows)
                tab.State.SelectionDictionary!.Add(dataRow!, false);

            await _Context.Tabs.AddAsync(tab);
            await _Context.SaveChangesAsync();

            SelectedTabPanelIndex = TabEntities.Count() - 1;
            await StoreTabPanelIndex();

            Tabs.Add(tab);

        }
        public async Task DeleteTab(TabEntity tab)
        {
            _Context.Tabs.Remove(tab);
            await _Context.SaveChangesAsync();


            Tabs.Remove(tab);
        }

        public async Task EditTab(TabEntity tab)
        {
            _Context.Tabs.Update(tab);
            await _Context.SaveChangesAsync();
        }

        public async Task StoreTabPanelIndex()
        {
            await SecureStorage.SetAsync($"connections({_ConnectionService.CurrentConnection!.Id}).selectedTabPanelIndex", SelectedTabPanelIndex.ToString());
        }

        public DataTable? RetrieveCachedData(TabEntity tab)
        {
            _MemoryCache.TryGetValue(tab, out DataTable? data);

            return data;
        }

        public void CacheData(TabEntity tab, DataTable data)
        {
            _MemoryCache.Set(tab, data);
        }

        public async Task Init()
        {
            var data = await SecureStorage.GetAsync($"connections({_ConnectionService.CurrentConnection!.Id}).selectedTabPanelIndex");
            SelectedTabPanelIndex = int.TryParse(data, out int index)? index: -1;

            Tabs = await TabEntities.ToListAsync();

            foreach (var tab in Tabs)
            {
                try
                {
                    if (tab.DataFetchingQuery == null)
                        return;

                    if (!await CheckIfTabElementExists(tab))
                    {
                        await DeleteTab(tab);
                        return;
                    }

                    tab.State = new TabState()
                    {
                        Data = await _DbService.RunQueryAsync(tab.DataFetchingQuery),
                        SelectionDictionary = [],
                        SubTabIndex = 0,
                        AllRowsSelected = false

                    };

                    foreach (DataRow dataRow in tab.State.Data!.Rows)
                    {
                        tab.State.SelectionDictionary!.Add(dataRow, false);
                    }
                }
                catch (Exception e)
                {
                    await Application.Current!.MainPage!.DisplayAlert("Error", e.Message, "OK");
                    await DeleteTab(tab);
                }
            }

        }

        private async Task<TabEntity> CreateDatabaseTab(DatabaseEntity db)
        {
            string query = $@"
                            SELECT
                                t.TABLE_NAME as `Table`,
                                CASE
                                    WHEN t.DATA_LENGTH < 1024 THEN CONCAT(t.DATA_LENGTH, ' Bytes')
                                    WHEN t.DATA_LENGTH < 1024 * 1024 THEN CONCAT(ROUND(t.DATA_LENGTH / 1024, 2), ' KB')
                                    WHEN t.DATA_LENGTH < 1024 * 1024 * 1024 THEN CONCAT(ROUND(t.DATA_LENGTH / (1024 * 1024), 2), ' MB')
                                    ELSE CONCAT(ROUND(t.DATA_LENGTH / (1024 * 1024 * 1024), 2), ' GB')
                                END AS Size,
                                t.ENGINE as Engine,
                                t.TABLE_COLLATION as Collation
                            FROM information_schema.TABLES AS t
                            WHERE t.TABLE_SCHEMA = '{db!.Name}'";

            TabEntity tab = new()
            {
                Connection = _ConnectionService.CurrentConnection!,
                Name = db!.Name,
                Type = TabType.Database,
                DataFetchingQuery = query,
                DbName = db!.Name,
                State = new TabState
                {
                    Data = await _DbService.RunQueryAsync(query),
                    SelectionDictionary = [],
                    SubTabIndex = 0,
                    AllRowsSelected = false,
                    TypingQuery = string.Empty
                }
            };

            return tab;
        }

        private async Task<TabEntity> CreateTableTab(TableEntity table)
        {
            string query = $"SELECT * FROM {table!.Database!.Name}.{table!.Name}";

            TabEntity tab = new()
            {
                Type = TabType.Table,
                Connection = _ConnectionService.CurrentConnection!,
                Name = table!.Name,
                DataFetchingQuery = query,
                DbName = table!.Database!.Name,
                State = new TabState
                {
                    Data = await _DbService.RunQueryAsync(query),
                    SelectionDictionary = [],
                    SubTabIndex = 0,
                    AllRowsSelected = false,
                    TypingQuery = string.Empty
                }
            };

            return tab;
        }

        public async Task<bool> CheckIfTabElementExists(TabEntity tab)
        {
            var db = await _DbService.RunQueryAsync($"SHOW DATABASES LIKE '{tab.DbName}';");

            if (db!.Rows.Count == 0)
                return false;

            if (tab.Type == TabType.Database)
                return true;

            await _DbService.RunQueryAsync($"USE {tab.DbName};");

            var table = await _DbService.RunQueryAsync($"SHOW TABLES LIKE '{tab.Name}';");

            return table!.Rows.Count == 1;

        }
    }
}
