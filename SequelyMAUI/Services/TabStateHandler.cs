using SequelyMAUI.Interfaces;
using SequelyMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace SequelyMAUI.Services
{
    internal class TabStateHandler : ITabStateHandler
    {
        public int SelectedTabIndex { get; set; }
        public List<TabModel> Tabs { get; } = new List<TabModel>();

        public void AddTab(TabModel tab)
        {
            Tabs.Add(tab);
        }

        public TabModel GetTab(Guid guid)
        {
            return Tabs.First(x => x.TabGuid == guid);
        }

        public async Task LoadTabState()
        {
            string? json = await SecureStorage.Default.GetAsync("tabs");

            if (json == null)
                return;

            Tabs.AddRange(JsonSerializer.Deserialize<List<TabModel>>(json)!);
        }

        public void RemoveTab(Guid guid)
        {
            Tabs.Remove(GetTab(guid));
        }

        public void SaveTabState()
        {
            string json = JsonSerializer.Serialize(Tabs);
            SecureStorage.Default.SetAsync("tabs", json);
        }
    }
}
