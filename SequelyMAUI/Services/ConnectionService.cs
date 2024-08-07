using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SequelyMAUI.Interfaces;
using System.Text.Json;
using SequelyMAUI.Entities;
using MudBlazor;



namespace SequelyMAUI.Services
{
    internal class ConnectionService(SQLiteContext context) : IConnectionService
    {
        private readonly SQLiteContext _context = context;

        public DbSet<ConnectionEntity> Connections => _context.Connections;

        public ConnectionEntity? CurrentConnection { get; private set; }

        public async Task EditConnection(ConnectionEntity connection)
        {
            _context.Update(connection);
            await _context.SaveChangesAsync();
        }

        public async Task CreateConnection(ConnectionEntity connection)
        {
            await _context.Connections.AddAsync(connection);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteConnection(ConnectionEntity connection)
        {
            _context.Connections.Remove(connection);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> ConnectTo(ConnectionEntity connection)
        {
            string connectionString = $"Server={connection.Address};Port={connection.Port};User ID={connection.Username};Password={connection.Password};";

            try
            {
                CurrentConnection = connection;
                CurrentConnection.ActualConnection = new MySqlConnection(connectionString);

                await CurrentConnection.ActualConnection.OpenAsync();
                await SecureStorage.Default.SetAsync("current_connection_id", $"{CurrentConnection.Id}");

                return null;
            }

            catch (Exception e)
            {
                CurrentConnection!.ActualConnection?.Dispose();
                CurrentConnection = null;

                return e.Message;
            }
        }

        public async Task<string?> TestConnection(ConnectionEntity connection)
        {
            string connectionString = $"Server={connection.Address};Port={connection.Port};User ID={connection.Username};Password={connection.Password};";

            try
            {
                connection.ActualConnection = new MySqlConnection(connectionString);

                await connection.ActualConnection.OpenAsync();
                connection.ActualConnection?.Dispose();

                return null;
            }

            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task RetrievePreviousConnection()
        {
            if (!int.TryParse(await SecureStorage.Default.GetAsync("current_connection_id"), out int id))
                return;

             
            ConnectionEntity? previousConnection = await _context.Connections.FindAsync(id);
            if (previousConnection == null)
                return;

            string? error = await ConnectTo(previousConnection);
            if (error != null)
                throw new Exception(error);
        }

        public async Task Disconnect()
        {
            SecureStorage.Default.Remove("current_connection_id");

            if (CurrentConnection == null || CurrentConnection.ActualConnection == null)
                return;

            await CurrentConnection.ActualConnection.CloseAsync();

            CurrentConnection.ActualConnection.Dispose();
            CurrentConnection = null;
        }
    }
}