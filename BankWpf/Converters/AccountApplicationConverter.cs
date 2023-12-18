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
    public class AccountApplicationConverter : IValueConverter
    {
        List<AccountApplication> listAccountApplications = new List<AccountApplication>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string AccountApplicationId = value.ToString();

            listAccountApplications = AdminWindow.AccountApplications;

            AccountApplication AccountApplication = listAccountApplications.FirstOrDefault(r => r.IdAccountApplication.Contains(AccountApplicationId));
            return AccountApplication != null ? AccountApplication.IdAccountApplication : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}