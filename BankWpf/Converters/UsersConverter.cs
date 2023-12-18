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
    public class UsersConverter : IValueConverter
    {
        List<User> listUsers = new List<User>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int UserId = (int)value;

            listUsers = AdminWindow.Users;

            User User = listUsers.FirstOrDefault(r => r.IdUser == UserId);
            if (User != null)
            {
                return $"{User.FirstName} {User.SecondName} {User.MiddleName}";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}