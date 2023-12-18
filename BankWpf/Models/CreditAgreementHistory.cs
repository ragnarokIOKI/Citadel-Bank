using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class CreditAgreementHistory
    {
        public int IdCreditAgreementHistory { get; set; }
        public string CreditAgreementId { get; set; } = null;
        public DateTime DateTime { get; set; }
        public string ChangeType { get; set; } = null;
        public string ChangedColumn { get; set; } = null;
        public string OldValue { get; set; } = null;
        public string NewValue { get; set; } = null;
    }
}
