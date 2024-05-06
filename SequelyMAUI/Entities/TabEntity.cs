using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Entities
{
    public enum TabType
    {
        Database,
        Table,
        Query
    }

    public struct TabState
    {
        public DataTable? Data { get; set; }

        public DataRow? SelectedRow { get; set; }

        public string TypingQuery { get; set; }

        public Dictionary<DataRow, bool>? SelectionDictionary { get; set; }

        public int SubTabIndex { get; set; }

        public bool? AllRowsSelected;




    }

    public class TabEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DbName { get; set; } = string.Empty;
        public ConnectionEntity Connection { get; set; } = new ConnectionEntity();

        public TabType Type { get; set; }

        public string? DataFetchingQuery { get; set; }

        [NotMapped]
        public TabState State;

    }
}
