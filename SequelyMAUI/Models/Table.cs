using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Models
{
    public class Table : MySqlElement
    {
        public List<Column>? Columns { get; set; }

        public Database? Database { get; set; }
    }
}
