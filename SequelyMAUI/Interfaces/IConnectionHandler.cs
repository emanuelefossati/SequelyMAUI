using MySqlConnector;
using SequelyMAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Interfaces
{
    internal interface IConnectionHandler
    {
        public MySqlConnection? CurrentMySqlConnection { get; }
        public string? MySqlConnectionErrorMessage { get; }
        public Task<List<Connection>> GetConnections();
        public Task SaveConnections(List<Connection> connections);
        public Task ConnectTo(Connection connection);
        public Task Disconnect();
        public Task<Connection?> RetrievePreviousConnection();
    }
}
