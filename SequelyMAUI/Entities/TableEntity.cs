using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Entities
{
    public class TableEntity : SqlEntity
    {
        public DatabaseEntity Database { get; set; } = new DatabaseEntity();
        public List<ColumnEntity> Columns { get; set; } = new List<ColumnEntity>();
    }
}
