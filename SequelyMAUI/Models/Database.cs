using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Models
{
    public class Database : MySqlElement
    {
        public List<Table>? Tables { get; set; }
    }
}
