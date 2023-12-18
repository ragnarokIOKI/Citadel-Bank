using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class Transaction
    {
        public int IdTransaction { get; set; }
        public string NameTransaction { get; set; } = null;
        public DateTime DateTransaction { get; set; }
        public string SummTransaction { get; set; }
        public string TransactionAccountId { get; set; } = null;
        public int TransactionTypeId { get; set; }
        public bool? TransactionDeleted { get; set; }
    }
}
