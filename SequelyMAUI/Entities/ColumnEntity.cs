using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Entities
{
    public class ColumnEntity
    {
        [Key]
        public int Id { get; set; }

        public string? Field { get; set; }
        public string? Type { get; set; }
        public string? Null { get; set; }
        public string? Key { get; set; }
        public string? Default { get; set; }
        public string? Extra { get; set; }

        public TableEntity? Table { get; set; }
    }
}
