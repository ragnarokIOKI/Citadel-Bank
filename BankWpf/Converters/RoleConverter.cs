using BankWpf.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows.Data;

namespace BankWpf
{
    public class RoleConverter : IValueConverter
    {
        List<Role> listRoles = new List<Role>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int roleId = (int)value;

            listRoles = AdminWindow.Roles;

            Role role = listRoles.FirstOrDefault(r => r.IdRole == roleId);
            return role != null ? role.NameRole : "";  
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
