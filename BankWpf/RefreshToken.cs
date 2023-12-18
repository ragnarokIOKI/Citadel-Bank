using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankWpf
{
    // Класс для работы с токеном авторизации
    internal class Token
    {
        // Свойство для хранения токена
        public static string token { get; set; }

        // Метод для обновления токена
        public async void RefreshToken()
        {
            // Использование HttpClientHandler для отключения проверки сертификата
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                // Использование HttpClient для отправки запроса на обновление токена
                using (var httpClient = new HttpClient(httpClientHandler))
                {
                    // Получение данных из текущего токена
                    dynamic data = JObject.Parse(Token.token);
                    string oldToken = data.access_token;
                    // Создание контента для запроса
                    StringContent content = new StringContent(JsonConvert.SerializeObject(oldToken), Encoding.UTF8, "application/json");
                    // Отправка POST-запроса на сервер для обновления токена
                    using (var response = await httpClient.PostAsync(App.hostString + "/Users/refresh_token?access_token=" + oldToken, null))
                    {
                        try
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // Обновление токена в случае успешного ответа сервера
                                string apiRes = await response.Content.ReadAsStringAsync();
                                token = apiRes;
                            }
                            else
                            {
                                // Вывод сообщения об ошибке в случае неудачного запроса
                                MessageBox.Show("Ошибка обновления токена!");
                            }
                        }
                        catch (Exception ex)
                        {
                            // Вывод сообщения об исключении
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}