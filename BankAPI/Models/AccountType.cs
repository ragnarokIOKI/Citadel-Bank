using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class AccountType
    {
        public int IdAccountType { get; set; }
        public string NameAccountType { get; set; } = null!;
        public bool? AccountTypeDeleted { get; set; }
    }
}
