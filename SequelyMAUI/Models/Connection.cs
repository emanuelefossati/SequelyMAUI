using DataAnnotationsExtensions;
using MudBlazor;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequelyMAUI.Models
{
    public class Connection : ICloneable
    {
        [Label("Name")]
        public string Name { get; set; } = "";
        [Label("Ip Address")]
        public string Address { get; set; } = "";
        [Label("Port"), Min(0)]
        public int? Port { get; set; }
        [Label("Username")]
        public string Username { get; set; } = "";
        [Label("Password")]
        public string Password { get; set; } = "";

        public object Clone()
        {
            return new Connection
            {
                Name = Name,
                Address = Address,
                Port = Port,
                Username = Username,
                Password = Password
            };
        }

        public void Copy(Connection connection)
        {
            Name = connection.Name;
            Address = connection.Address;
            Port = connection.Port;
            Username = connection.Username;
            Password = connection.Password;
        }

        public bool IsIdentical(Connection connection)
        {
            return Name == connection.Name &&
                Address == connection.Address &&
                Port == connection.Port &&
                Username == connection.Username &&
                Password == connection.Password;
        }
    }
}