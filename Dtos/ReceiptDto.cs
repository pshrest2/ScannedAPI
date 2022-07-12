using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScannedAPI.Dtos
{
    public class ReceiptDto
    {
        public ReceiptDto()
        {
            Items = new List<Item>();
        }

        public string Merchant{ get; set; }
        public List<Item> Items { get; set; }
        public float TaxPercent { get; set; }
        public float SubTotal { get; set; }
        public float Total { get; set; }
        public float Tip { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
