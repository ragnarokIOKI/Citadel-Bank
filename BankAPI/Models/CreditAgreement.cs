using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class CreditAgreement
    {
        public string IdCreditAgreement { get; set; } = null!;
        public DateTime DateDrawing { get; set; }
        public DateTime DateTermination { get; set; }
        public decimal CreditRate { get; set; }
        public int CreditAgreementUserId { get; set; }
        public string CreditApplicationId { get; set; } = null!;
        public bool? CreditAgreementDeleted { get; set; }
    }
}
