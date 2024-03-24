using SequelyMAUI.Models;

namespace SequelyMAUI.Interfaces
{
    internal interface ITabStateHandler
    {
        public int SelectedTabIndex { get; set; }
        public List<TabModel> Tabs { get; }
        public void AddTab(TabModel tab);
        public void RemoveTab(Guid guid);
        public TabModel GetTab(Guid guid);
        public void SaveTabState();
        public Task LoadTabState();

    }
}
