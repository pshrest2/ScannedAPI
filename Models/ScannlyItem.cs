using System;
using Newtonsoft.Json;

namespace ScannedAPI.Models
{
    public class ScannlyItem
    {
        public ScannlyItem()
        {
        }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }
}

