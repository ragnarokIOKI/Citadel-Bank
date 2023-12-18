using System.Windows;

namespace BankWpf
{
    /// <summary>
    /// Логика взаимодействия для всего приложения
    /// </summary>
    public partial class App : Application
    {
       public static string hostString { get; } = "https://localhost:7154/api/";
       public static string role;
    }
}
