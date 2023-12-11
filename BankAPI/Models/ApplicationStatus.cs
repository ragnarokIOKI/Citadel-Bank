using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class ApplicationStatus
    {
        public int IdApplicationStatus { get; set; }
        public string NameStatus { get; set; } = null!;
    }
}
