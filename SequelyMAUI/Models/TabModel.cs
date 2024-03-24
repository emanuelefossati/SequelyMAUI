using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Models
{
    public class TabModel
    {
        public Guid TabGuid { get; set; }  = Guid.NewGuid();
        public MySqlElement? ElementToDisplay { get; set; }
        public DataTable? Data { get; set; }
    }
}
