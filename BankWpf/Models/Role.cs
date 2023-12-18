using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class Role
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; } = null;
        public bool? RoleDeleted { get; set; }
    }
}
