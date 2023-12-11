using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class AccountApplicationHistory
    {
        public int IdAccountApplicationHistory { get; set; }
        public string AccountApplicationId { get; set; } = null!;
        public DateTime DateTime { get; set; }
        public string ChangeType { get; set; } = null!;
        public string ChangedColumn { get; set; } = null!;
        public string OldValue { get; set; } = null!;
        public string NewValue { get; set; } = null!;
    }
}
