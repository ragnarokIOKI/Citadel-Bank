using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class Account
    {
        public string IdAccount { get; set; } = null;
        public decimal Balance { get; set; }
        public int Percent { get; set; }
        public int TypeId { get; set; }
        public int UserAccountId { get; set; }
        public bool AccountDeleted { get; set; }
        public string Account_Deletion_Reason { get; set; }
    }
}
