using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class AccountType
    {
        public int IdAccountType { get; set; }
        public string NameAccountType { get; set; } = null;
        public bool? AccountTypeDeleted { get; set; }
    }
}
