using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SequelyMAUI.Entities;
using SequelyMAUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Services
{
    internal class TabService : ITabService
    {
        private readonly SQLiteContext _Context;
        private readonly IConnectionService _ConnectionService;
        private readonly IMemoryCache _MemoryCache;

        public IQueryable<TabEntity> Tabs => _Context.Tabs.Where(x => x.Connection == _ConnectionService.CurrentConnection!);

        public int SelectedTabPanelIndex { get; set; }

        public TabService(SQLiteContext context, IMemoryCache memoryCache, IConnectionService connectionService)
        {
            _Context = context;
            _MemoryCache = memoryCache;
            _ConnectionService = connectionService;
        }

        public async Task CreateTab(TabEntity tab)
        {
            await _Context.Tabs.AddAsync(tab);
            await _Context.SaveChangesAsync();

            SelectedTabPanelIndex = Tabs.Count() - 1;
            await StoreTabPanelIndex();
        }

        public async Task DeleteTab(TabEntity tab)
        {
            _Context.Tabs.Remove(tab);
            await _Context.SaveChangesAsync();

            await StoreTabPanelIndex();
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
        }
    }
}
