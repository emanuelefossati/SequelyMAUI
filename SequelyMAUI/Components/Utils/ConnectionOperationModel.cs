
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SequelyMAUI.Models;

namespace SequelyMAUI.Components.Utils
{
    public enum ConnectionOperationType
    {
        Connect,
        Delete
    }
    internal class ConnectionOperationModel
    {
        public List<ConnectionOperationType> Operations { get; set; } = new List<ConnectionOperationType>();
        public Connection? ConnectionModel { get; set; }
    }
}
