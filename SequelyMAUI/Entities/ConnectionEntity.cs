using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MudBlazor;
using DataAnnotationsExtensions;
using MySqlConnector;
using System.ComponentModel.DataAnnotations.Schema;

namespace SequelyMAUI.Entities
{
    public class ConnectionEntity : ICloneable
    {
        [Key]
        public int? Id { get; set; }

        [Label("Name")]
        public string Name { get; set; } = string.Empty;
        [Label("Address")]
        public string Address { get; set; } = string.Empty;
        [Label("Port"), Min(0)]
        public int? Port { get; set; }
        [Label("Username")]
        public string Username { get; set; } = string.Empty;
        [Label("Password")]
        public string Password { get; set; } = string.Empty;

        public List<DatabaseEntity> Databases { get; set; } = new List<DatabaseEntity>();

        [NotMapped]
        public MySqlConnection? ActualConnection { get; set; }

        public object Clone()
        {
            return new ConnectionEntity
            {
                Name = Name,
                Address = Address,
                Port = Port,
                Username = Username,
                Password = Password
            };
        }

        public void Copy(ConnectionEntity connection)
        {
            Name = connection.Name;
            Address = connection.Address;
            Port = connection.Port;
            Username = connection.Username;
            Password = connection.Password;
        }

        public bool IsIdenticalTo(ConnectionEntity connection)
        {
            return Name == connection.Name &&
                Address == connection.Address &&
                Port == connection.Port &&
                Username == connection.Username &&
                Password == connection.Password;
        }


    }
}
