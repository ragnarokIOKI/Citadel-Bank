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
    public class CreditAgreementConverter : IValueConverter
    {
        List<CreditAgreement> listCreditAgreements = new List<CreditAgreement>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string CreditAgreementId = value.ToString();

            listCreditAgreements = AdminWindow.CreditAgreements;

            CreditAgreement CreditAgreement = listCreditAgreements.FirstOrDefault(r => r.IdCreditAgreement.Contains(CreditAgreementId));
            return CreditAgreement != null ? CreditAgreement.IdCreditAgreement : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}