using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScannedAPI.Dtos
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
