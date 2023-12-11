using System;
using System.Collections.Generic;

namespace BankAPI.Models
{
    public partial class Role
    {
        public int IdRole { get; set; }
        public string NameRole { get; set; } = null!;
        public bool? RoleDeleted { get; set; }
    }
}
