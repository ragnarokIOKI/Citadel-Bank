using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectBankWPF
{
    [TestClass]
    public class SignUpUnitTest
    {
        [TestMethod]
        public void Test_InvalidPassword_TooShort()
        {
            SignIn signIn = new SignIn("validUser123", "Pa$1");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Слишком короткий пароль!");
            Assert.IsTrue(signIn.IsPasswordValid());
        }

        [TestMethod]
        public void Test_InvalidLogin_TooShort()
        {
            SignIn signIn = new SignIn("shor", "pa$$Word123");
            string loginCheckAssert = signIn.ValidateLogin();
            Assert.AreEqual(loginCheckAssert, "Слишком короткий логин!");
            Assert.IsTrue(signIn.IsLoginValid());
        }

        [TestMethod]
        public void Test_InvalidPassword_Weak()
        {
            SignIn signIn = new SignIn("validUser123", "weakpassword");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Некорректный формат пароля!");
            Assert.IsFalse(signIn.IsPasswordValid());
        }

        [TestMethod]
        public void Test_InvalidLogin_TooLong()
        {
            SignIn signIn = new SignIn("thisIsALongUsernameThatExceedsMaxLength", "pa$$Word123");
            string loginCheckAssert = signIn.ValidateLogin();
            Assert.AreEqual(loginCheckAssert, "Слишком длинный логин!");
            Assert.IsFalse(signIn.IsLoginValid());
        }

        [TestMethod]
        public void Test_InvalidPassword_TooLong()
        {
            SignIn signIn = new SignIn("validUser123", "ThisIsAVeryLongPasswordThatExceedsMaxLength1");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Слишком длинный пароль!");
            Assert.IsFalse(signIn.IsPasswordValid());
        }

        [TestMethod]
        public void Test_ValidLogin()
        {
            SignIn signIn = new SignIn("validUser123", "pa$$Word123");
            string loginCheckAssert = signIn.ValidateLogin();
            Assert.AreEqual(loginCheckAssert, "Успешно!");
            Assert.IsTrue(signIn.IsLoginValid());
        }

        [TestMethod]
        public void Test_ValidPassword()
        {
            SignIn signIn = new SignIn("validUser123", "StrongP@ssword1");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Успешно!");
            Assert.IsTrue(signIn.IsPasswordValid());
        }

        [TestMethod]
        public void Test_LoginEmpty()
        {
            SignIn signIn = new SignIn("", "pa$$Word123");
            string loginCheckAssert = signIn.ValidateLogin();
            Assert.AreEqual(loginCheckAssert, "Поле Логин не должно быть пустым!");
        }

        [TestMethod]
        public void Test_LoginNotEmpty()
        {
            SignIn signIn = new SignIn("username", "pa$$Word123");
            string loginCheckAssert = signIn.ValidateLogin();
            Assert.AreEqual(loginCheckAssert, "Успешно!");
        }

        [TestMethod]
        public void Test_PasswordEmpty()
        {
            SignIn signIn = new SignIn("username", "");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Поле Пароль не должно быть пустым!");
        }

        [TestMethod]
        public void Test_PasswordNotEmpty()
        {
            SignIn signIn = new SignIn("username", "pa$$Word123");
            string passwordCheckAssert = signIn.ValidatePassword();
            Assert.AreEqual(passwordCheckAssert, "Успешно!");
        }
    }
}