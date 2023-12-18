using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class CreditApplication
    {
        public string IdCreditApplication { get; set; } = null;
        public decimal ApplicationAmount { get; set; }
        public decimal CreditDesiredPercentage { get; set; }
        public int CreditApplicationUserId { get; set; }
        public int StatusId { get; set; }
    }
}
