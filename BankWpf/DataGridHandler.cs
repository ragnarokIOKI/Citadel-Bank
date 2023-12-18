using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankWpf
{
    // Класс для обработки данных в DataGrid
    internal class DataGridHandler
    {
        // Создание DataTable из списка объектов с определенным индексом
        public static DataTable CreateDataTable<T>(IEnumerable<T> list, int index)
        {
            // Получение типа объекта
            Type type = typeof(T);
            // Получение свойств объекта
            var properties = type.GetProperties();

            // Создание DataTable
            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            // Добавление колонок в DataTable на основе свойств объекта
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            // Заполнение DataTable значениями из списка объектов
            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                // Исключение объектов с определенным значением свойства
                if (properties[index].GetValue(entity).ToString() != "1")
                {
                    for (int i = 0; i < properties.Length; i++)
                    {
                        values[i] = properties[i].GetValue(entity);
                    }
                    dataTable.Rows.Add(values);
                }
            }
            return dataTable;
        }

        // Преобразование DataTable обратно в список объектов
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            // Проход по строкам DataTable
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        // Получение объекта из DataRow
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            // Проход по столбцам DataTable и свойствам объекта
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    // Присвоение значения свойству объекта из DataRow
                    if (pro.Name == column.ColumnName)
                        try
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        catch { }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
