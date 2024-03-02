using MySqlConnector;
using SequelyMAUI.Interfaces;
using SequelyMAUI.Models;
using System.Text.Json;

namespace SequelyMAUI.Services
{
    internal class ConnectionHandler : IConnectionHandler
    {
        public MySqlConnection? CurrentMySqlConnection { get; private set; }
        public string? MySqlConnectionErrorMessage { get; private set; }

        public List<Connection> Connections { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public async Task<List<Connection>> GetConnections()
        {
            string? json = await SecureStorage.Default.GetAsync("connections");

            if (json == null)
            {
                var connections = new List<Connection>();

                for(int i = 0; i < 20; i++)
                {
                    connections.Add(new Connection
                    {
                        Name = $"Connection {i + 1}",
                        Address = "localhost",
                        Port = 3306,
                        Username = "root",
                        Password = ""
                    });
                }

                return connections;
            }

            return JsonSerializer.Deserialize<List<Connection>>(json)!;
        }
        public async Task SaveConnections(List<Connection> connections)
        {
            string json = JsonSerializer.Serialize(connections);
            await SecureStorage.Default.SetAsync("connections", json);
        }
        public async Task ConnectTo(Connection connection)
        {
            string connectionString = $"Server={connection.Address};Port={connection.Port};User ID={connection.Username};Password={connection.Password};";
            MySqlConnectionErrorMessage = null;

            try
            {
                CurrentMySqlConnection = new MySqlConnection(connectionString);
                await CurrentMySqlConnection.OpenAsync();

                string json = JsonSerializer.Serialize(connection);
                await SecureStorage.Default.SetAsync("current_connection", json);


            }

            catch (Exception ex)
            {
                CurrentMySqlConnection = null;
                throw;
            }
        }

        public async Task<Connection?> RetrievePreviousConnection()
        {
            string? json = await SecureStorage.Default.GetAsync("current_connection");

            if (json == null)
            {
                return null;
            }

            var connection = JsonSerializer.Deserialize<Connection>(json)!;

            await ConnectTo(connection);

            return connection;
        }

        public async Task Disconnect()
        {
            if (CurrentMySqlConnection == null)
            {
                return;
            }

            await CurrentMySqlConnection.CloseAsync();
            CurrentMySqlConnection.Dispose();
            CurrentMySqlConnection = null;
        }
    }
}