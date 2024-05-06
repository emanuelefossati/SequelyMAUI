using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SequelyMAUI.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Interfaces
{
    internal interface ITabService
    {
        public IQueryable<TabEntity> TabEntities { get; }
        public List<TabEntity> Tabs { get; set; }
        public int SelectedTabPanelIndex { get; set; }

        public Task DeleteTab(TabEntity tab);
        public Task EditTab(TabEntity tab);
        public Task StoreTabPanelIndex();

        public Task CreateTab(SqlEntity entity);

        public DataTable? RetrieveCachedData(TabEntity tab);
        public void CacheData(TabEntity tab, DataTable data);

        public Task Init();

        public Task<bool> CheckIfTabElementExists(TabEntity tab);
    }
}
