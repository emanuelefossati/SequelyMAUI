using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Entities
{
    enum TabType
    {
        Database,
        Table,
        Query
    }

    public class TabEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ConnectionEntity Connection { get; set; } = new ConnectionEntity();

        TabType Type { get; set; }

        TabEntity? Parent { get; set; }
        List<TabEntity> Children { get; set; } = new List<TabEntity>();

        public string? Query { get; set; }
    }
}
