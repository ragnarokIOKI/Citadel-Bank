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
    internal class AccountTypesConverter : IValueConverter
    {
        List<AccountType> listAccountTypes = new List<AccountType>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int AccountTypeId = (int)value;

            listAccountTypes = AdminWindow.AccountTypes;

            AccountType AccountType = listAccountTypes.FirstOrDefault(r => r.IdAccountType == AccountTypeId);
            return AccountType != null ? AccountType.NameAccountType : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
