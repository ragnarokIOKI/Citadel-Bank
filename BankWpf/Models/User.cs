using System;
using System.Collections.Generic;

namespace BankWpf.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string FirstName { get; set; } = null;
        public string SecondName { get; set; } = null;
        public string MiddleName { get; set; }
        public DateTime? Birthday { get; set; }
        public string PassportSeries { get; set; } = null;
        public string PassportNumber { get; set; } = null;
        public string Login { get; set; } = null;
        public string Password { get; set; } = null;
        public string Salt { get; set; } = null;
        public int RoleId { get; set; }
        public bool? UserDeleted { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + MiddleName;
            }
        }
    }
}
