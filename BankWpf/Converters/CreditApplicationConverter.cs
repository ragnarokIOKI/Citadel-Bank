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
    public class CreditApplicationConverter : IValueConverter
    {
        List<CreditApplication> listCreditApplications = new List<CreditApplication>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string CreditApplicationId = value.ToString();

            listCreditApplications = AdminWindow.CreditApplications;

            CreditApplication CreditApplication = listCreditApplications.FirstOrDefault(r => r.IdCreditApplication.Contains(CreditApplicationId));
            return CreditApplication != null ? CreditApplication.IdCreditApplication : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}