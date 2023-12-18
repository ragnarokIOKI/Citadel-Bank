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
    public class TransactionTypesConverter : IValueConverter
    {
        List<TransactionType> listTransactionTypes = new List<TransactionType>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int TranTypeId = (int)value;

            listTransactionTypes = AdminWindow.TransactionTypes;

            TransactionType tranType = listTransactionTypes.FirstOrDefault(r => r.IdTransactionType == TranTypeId);
            return tranType != null ? tranType.NameTransactionType : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
