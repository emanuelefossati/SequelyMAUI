using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Models
{
    public class Column
    {
        public string? Field { get; set; }
        public string? Type { get; set; }
        public string? Null { get; set; }
        public string? Key { get; set; }
        public string? Default { get; set; }
        public string? Extra { get; set; }

        public Table? Table { get; set; }
    }
}