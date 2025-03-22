using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = "Credit"; // "Debit" o "Credit"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
