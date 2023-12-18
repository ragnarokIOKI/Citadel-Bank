using System;
using System.Text.RegularExpressions;

namespace UnitTestProjectBankWPF
{
    public class SignIn
    {
        private readonly string login;
        private readonly string password;

        public SignIn(string login, string password)
        {
            this.login = login ?? throw new ArgumentNullException(nameof(login));
            this.password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public bool IsLoginValid()
        {
            return Regex.IsMatch(login, "^[a-zA-Z0-9]{1,25}$");
        }

        public bool IsPasswordValid()
        {
            return Regex.IsMatch(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).{4,20}$");
        }

        public string CheckLoginNotEmpty()
        {
            if (string.IsNullOrEmpty(login))
            {
                return "Поле Логин не должно быть пустым!";
            }
            return "Успешно!";
        }

        public string CheckPasswordNotEmpty()
        {
            if (string.IsNullOrEmpty(password))
            {
                return "Поле Пароль не должно быть пустым!";
            }
            return "Успешно!";
        }

        public string ValidateLogin()
        {
            var loginNotEmptyCheck = CheckLoginNotEmpty();
            if (loginNotEmptyCheck != "Успешно!")
            {
                return loginNotEmptyCheck;
            }

            var lengthCheck = LongShortCheckLogin(login);
            if (lengthCheck != "Успешно!")
            {
                return lengthCheck;
            }

            if (!IsLoginValid())
            {
                return "Некорректный формат логина!";
            }

            return "Успешно!";
        }

        public string LongShortCheckPassword(string password)
        {
            if (password.Length < 5)
            {
                return "Слишком короткий пароль!";
            }
            else
            {
                if (password.Length > 20)
                {
                    return "Слишком длинный пароль!";
                }
            }
            return "Успешно!";
        }

        public string LongShortCheckLogin(string login)
        {
            if (login.Length < 5)
            {
                return "Слишком короткий логин!";
            }
            else
            {
                if (login.Length > 24)
                {
                    return "Слишком длинный логин!";
                }
            }
            return "Успешно!";
        }

        public string ValidatePassword()
        {
            var passwordNotEmptyCheck = CheckPasswordNotEmpty();
            if (passwordNotEmptyCheck != "Успешно!")
            {
                return passwordNotEmptyCheck;
            }

            var lengthCheck = LongShortCheckPassword(password);
            if (lengthCheck != "Успешно!")
            {
                return lengthCheck;
            }

            if (!IsPasswordValid())
            {
                return "Некорректный формат пароля!";
            }

            return "Успешно!";
        }
    }
}