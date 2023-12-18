using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class Card
    {
        public string CardNumber { get; set; } = null;
        public string CardHolder { get; set; } = null;
        public string Validity { get; set; } = null;
        public string Ccv { get; set; } = null;
        public string CardAccountId { get; set; } = null;
        public bool? CardDeleted { get; set; }
        public string Card_Deletion_Reason { get; set; }
    }
}
