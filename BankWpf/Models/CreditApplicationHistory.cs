using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class CreditApplicationHistory
    {
        public int IdCreditApplicationHistory { get; set; }
        public string CreditApplicationId { get; set; } = null;
        public DateTime DateTime { get; set; }
        public string ChangeType { get; set; } = null;
        public string ChangedColumn { get; set; } = null;
        public string OldValue { get; set; } = null;
        public string NewValue { get; set; } = null;
    }
}
