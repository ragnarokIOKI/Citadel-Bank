using BankWpf.Models;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankWpf
{
    /// <summary>
    /// Логика взаимодействия для окна авторизации
    /// </summary>
    public partial class MainWindow : Window
    {
        // Создание объекта Random для генерации случайного числа
        static Random rnd = new Random();
        // Статическая переменная для хранения случайного числа
        public static int value = rnd.Next(100, 999);
        // Переменные для хранения логина и пароля
        string log, pass;
        public MainWindow()
        {
            InitializeComponent();
            // Получение доступа к реестру
            RegistryKey registry = Registry.CurrentUser;
            // Создание или открытие подраздела реестра "CitadelBank"
            RegistryKey key = registry.CreateSubKey("CitadelBank");
            try
            {
                // Чтение логина и пароля из реестра
                log = key.GetValue("login").ToString();
                pass = key.GetValue("password").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Установка логина и пароля в соответствующие поля ввода
            tbLogin.Text = log;
            tbPassword.Password = pass;
        }

        // Метод для сохранения логина и пароля в реестре
        private void regSet(string login, string pass)
        {
            // Получение доступа к реестру
            RegistryKey registry = Registry.CurrentUser;
            // Создание или открытие подраздела реестра "CitadelBank"
            RegistryKey key = registry.CreateSubKey("CitadelBank");
            try
            {
                // Установка логина и пароля в реестр
                key.SetValue("login", login);
                key.SetValue("password", pass);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Обработчик события нажатия кнопки "Войти"
        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != string.Empty)
            {
                if (tbPassword.Password != string.Empty)
                {
                    // Создание объекта пользователя на основе введенных данных
                    User user = new User()
                    {
                        Login = tbLogin.Text,
                        Password = tbPassword.Password.ToString()
                    };
                    // Использование HttpClient для отправки запроса на сервер
                    using (var httpClient = new HttpClient())
                    {
                        // Сериализация объекта пользователя в JSON
                        StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                        // Отправка POST-запроса на сервер
                        using (var response = await httpClient.PostAsync("https://localhost:7154/" + $"LogIn?login={user.Login}&password={user.Password}", content))
                        {
                            if (response.StatusCode == HttpStatusCode.BadRequest)
                            {
                                // Вывод сообщения об ошибке, если запрос неудачен
                                string errorContent = await response.Content.ReadAsStringAsync();
                                MessageBox.Show(errorContent);
                            }
                            else
                            {
                                // Обработка успешного ответа сервера
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                user = JsonConvert.DeserializeObject<User>(apiResponse);
                                // Сохранение логина и пароля в реестре
                                regSet(tbLogin.Text, tbPassword.Password.ToString());

                                try
                                {
                                    if (response.IsSuccessStatusCode)
                                    {
                                        // Обработка успешного входа
                                        string apiRes = await response.Content.ReadAsStringAsync();
                                        Token.token = apiRes;
                                        dynamic data = JObject.Parse(Token.token);
                                        App.role = data.role;
                                        try
                                        {
                                            AdminWindow admin = new AdminWindow();

                                            if (App.role == "2")
                                            {
                                                // Предупреждение о недоступности приложения для клиентов банка
                                                MessageBoxResult result = MessageBox.Show("Клиенты банка не могут пользоваться данным приложением, просим прощения за причинённые неудобства.", "Права доступа", MessageBoxButton.YesNo, MessageBoxImage.Question);
                                                if (result == MessageBoxResult.Yes)
                                                {
                                                    Application.Current.Shutdown();
                                                }
                                            }
                                            else
                                            {
                                                // Открытие окна администратора
                                                admin.Show();
                                                this.Close();
                                                Close();
                                            }

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Не верный логин или пароль.");
                                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Не верный логин или пароль.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Отсутствует подключение.");
                                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                                }
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Заполните данное поле!");
                    tbPassword.Focus();
                }
            }
            else
            {
                MessageBox.Show("Заполните данное поле!");
                tbLogin.Focus();
            }
        }

        // Обработчик события нажатия кнопки "Выход"
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            // Завершение работы приложения
            Application.Current.Shutdown();
        }
    }
}