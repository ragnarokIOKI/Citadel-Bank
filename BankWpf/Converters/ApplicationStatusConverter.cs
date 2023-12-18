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
    internal class ApplicationStatusConverter : IValueConverter
    {
        List<ApplicationStatus> list = new List<ApplicationStatus>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(value.ToString(), out int StatId))
                return 0;

            list = AdminWindow.ApplicationStatuses;

            ApplicationStatus stat = list.FirstOrDefault(r => r.IdApplicationStatus == StatId);
            return stat != null ? stat.NameStatus : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}