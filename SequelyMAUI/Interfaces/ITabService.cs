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
        public IQueryable<TabEntity> Tabs { get; }
        public int SelectedTabPanelIndex { get; set; }

        public Task CreateTab(TabEntity tab);
        public Task DeleteTab(TabEntity tab);
        public Task EditTab(TabEntity tab);
        public Task StoreTabPanelIndex();

        public DataTable? RetrieveCachedData(TabEntity tab);
        public void CacheData(TabEntity tab, DataTable data);

        public Task Init();

    }
}
