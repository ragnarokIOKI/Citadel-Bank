using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class AccountApplication
    {
        public string IdAccountApplication { get; set; } = null;
        public int TypeAccountId { get; set; }
        public decimal AccountDesiredPercentage { get; set; }
        public int AccountApplicationUserId { get; set; }
        public int StatusId { get; set; }
        public bool Bank_Card_Needed { get; set; }
    }
}
