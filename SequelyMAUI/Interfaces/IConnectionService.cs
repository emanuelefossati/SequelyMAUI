using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SequelyMAUI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Interfaces
{
    internal interface IConnectionService
    {
        public DbSet<ConnectionEntity> Connections { get; }
        public ConnectionEntity? CurrentConnection { get; }

        public Task CreateConnection(ConnectionEntity connection);
        public Task DeleteConnection(ConnectionEntity connection);
        public Task EditConnection(ConnectionEntity connection);
        public Task<string?> ConnectTo(ConnectionEntity connection);

        public Task<string?> TestConnection(ConnectionEntity connection);
        public Task Disconnect();
        public Task RetrievePreviousConnection();
    }
}
