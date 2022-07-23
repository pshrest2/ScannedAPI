using System;
using System.Text.Json.Serialization;

namespace ScannedAPI.Models
{
    public class User
    {
        public User()
        {
        }
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PartitionKey { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Phone { get; set; }
    }
}
