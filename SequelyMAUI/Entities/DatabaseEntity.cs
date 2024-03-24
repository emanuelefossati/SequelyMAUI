using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Entities
{
    public class DatabaseEntity : SqlEntity
    {
        public ConnectionEntity Connection { get; set; } = new ConnectionEntity();
     
        public List<TableEntity>? Tables { get; set; } = new List<TableEntity>();
    }
}
