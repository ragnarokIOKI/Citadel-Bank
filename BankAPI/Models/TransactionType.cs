using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class TransactionType
    {
        public int IdTransactionType { get; set; }
        public string NameTransactionType { get; set; } = null!;
        public bool? TransactionTypeDeleted { get; set; }
    }
}
