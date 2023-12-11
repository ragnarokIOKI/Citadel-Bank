using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class Transaction
    {
        public int IdTransaction { get; set; }
        public string NameTransaction { get; set; } = null!;
        public DateTime DateTransaction { get; set; }
        public decimal SummTransaction { get; set; }
        public string TransactionAccountId { get; set; } = null!;
        public int TransactionTypeId { get; set; }
        public bool? TransactionDeleted { get; set; }
    }
}
