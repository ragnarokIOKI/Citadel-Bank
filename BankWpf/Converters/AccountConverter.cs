using BankWpf.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BankWpf
{
    public class AccountConverter : IValueConverter
    {
        List<Account> listAccounts = new List<Account>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string AccountId = value.ToString();

            listAccounts = AdminWindow.Accounts;

            Account Account = listAccounts.FirstOrDefault(r => r.IdAccount.Contains(AccountId));
            return Account != null ? Account.IdAccount : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}