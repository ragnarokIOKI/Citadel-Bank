using BankWpf.Models;
using CsvHelper;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BankWpf
{
    public partial class AdminWindow : Window
    {
        // Списки объектов для отображения в окне администратора
        public static List<Role> Roles = new List<Role>();
        public static List<Transaction> Transactions = new List<Transaction>();
        public static List<TransactionType> TransactionTypes = new List<TransactionType>();
        public static List<User> Users = new List<User>();
        public static List<Account> Accounts = new List<Account>();
        public static List<CreditApplication> CreditApplications = new List<CreditApplication>();
        public static List<CreditAgreement> CreditAgreements = new List<CreditAgreement>();
        public static List<AccountApplication> AccountApplications = new List<AccountApplication>();
        public static List<AccountApplicationHistory> AccountApplicationHistories = new List<AccountApplicationHistory>();
        public static List<CreditApplicationHistory> CreditApplicationHistories = new List<CreditApplicationHistory>();
        public static List<CreditAgreementHistory> CreditAgreementHistories = new List<CreditAgreementHistory>();
        public static List<Card> Cards = new List<Card>();
        public static List<AccountType> AccountTypes = new List<AccountType>();
        public static List<ApplicationStatus> ApplicationStatuses = new List<ApplicationStatus>();

        // Переменные для хранения ID и других данных
        private int UserID = 0;
        private int TransactionID = 0;
        private int cmbTypeTransactionINT;
        private int cmbRoleUserINT;
        private string CardID = "";
        private string AccID = "";
        private string CredAgID = "";
        private string CredAppID = "";
        private string AccAppID = "";

        private string search = null;
        private string orderTransaction = "";
        private int? orderTransactionType = null;

        public AdminWindow()
        {
            InitializeComponent();
            // Заполнение различных списков и отображение данных при инициализации окна
            fillUsers(null, null);
            fillAccounts(null);
            fillUsers(null, null);
            fillAccounts(null);
            fillRoles();
            fillTransactionType();
            fillCreditApplications(null);
            fillCreditAgreements(null, null);
            fillAccountApplications(null);
            fillAccountApplicationHistory(null);
            fillCreditAgreementHistory(null);
            fillCreditApplicationHistory(null);
            fillAccountApplicationHistory(null);
            fillCards(null);
            fillAccountTypes();
            fillApplicationStatus();
            fillTransaction();
        }

        // Методы для заполнения списков данных, выпадающих списков и полей
        public async Task fillCards(string orderBy)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"Cards?orderBy={orderBy}"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();

                            var products = JsonConvert.DeserializeObject<List<Card>>(apiResponse);

                            CardsListView.ItemsSource = products;

                        }
                    }
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.ToString()); }
        }
        private async void fillApplicationStatus()
        {
            try
            {
                List<ApplicationStatus> AppStatusesList2 = new List<ApplicationStatus>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + "ApplicationStatus"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ApplicationStatuses = JsonConvert.DeserializeObject<List<ApplicationStatus>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(ApplicationStatuses, 1);
                        AppStatusesList2 = DataGridHandler.ConvertDataTable<ApplicationStatus>(dataTable);

                        cmbCredAppStatus.ItemsSource = AppStatusesList2;
                        cmbCredAppStatus.DisplayMemberPath = "NameStatus";
                        cmbCredAppStatus.SelectedValuePath = "IdApplicationStatus";

                        cmbCredAppStatusSort.ItemsSource = AppStatusesList2;
                        cmbCredAppStatusSort.DisplayMemberPath = "NameStatus";
                        cmbCredAppStatusSort.SelectedValuePath = "IdApplicationStatus";

                        cmbAccAppStatusSort.ItemsSource = AppStatusesList2;
                        cmbAccAppStatusSort.DisplayMemberPath = "NameStatus";
                        cmbAccAppStatusSort.SelectedValuePath = "IdApplicationStatus";

                        cmbAccAppStatus.ItemsSource = AppStatusesList2;
                        cmbAccAppStatus.DisplayMemberPath = "NameStatus";
                        cmbAccAppStatus.SelectedValuePath = "IdApplicationStatus";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillCreditAgreementHistory(int? orderBy)
        {
            try
            {
                List<CreditAgreementHistory> CreditAgreementHistoryList2 = new List<CreditAgreementHistory>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"CreditAgreementHistories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        CreditAgreementHistories = JsonConvert.DeserializeObject<List<CreditAgreementHistory>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(CreditAgreementHistories, 2);
                        CreditAgreementHistoryList2 = DataGridHandler.ConvertDataTable<CreditAgreementHistory>(dataTable);

                        dgCreditAgreementHistory.ItemsSource = CreditAgreementHistories;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillCreditApplicationHistory(int? orderBy)
        {
            try
            {
                List<CreditApplicationHistory> CreditApplicationHistoryList2 = new List<CreditApplicationHistory>();

                using (var httpClient = new HttpClient())
                {
                    //using (var response = await httpClient.GetAsync(App.hostString + $"CreditApplicationHistorys?orderBy={orderBy}"))
                    using (var response = await httpClient.GetAsync(App.hostString + $"CreditApplicationHistories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        CreditApplicationHistories = JsonConvert.DeserializeObject<List<CreditApplicationHistory>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(CreditApplicationHistories, 2);
                        CreditApplicationHistoryList2 = DataGridHandler.ConvertDataTable<CreditApplicationHistory>(dataTable);

                        dgCreditApplicationHistory.ItemsSource = CreditApplicationHistories;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillAccountApplicationHistory(int? orderBy)
        {
            try
            {
                List<AccountApplicationHistory> AccountApplicationHistoryList2 = new List<AccountApplicationHistory>();

                using (var httpClient = new HttpClient())
                {
                    //using (var response = await httpClient.GetAsync(App.hostString + $"AccountApplicationHistorys?orderBy={orderBy}"))
                    using (var response = await httpClient.GetAsync(App.hostString + $"AccountApplicationHistories"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        AccountApplicationHistories = JsonConvert.DeserializeObject<List<AccountApplicationHistory>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(AccountApplicationHistories, 2);
                        AccountApplicationHistoryList2 = DataGridHandler.ConvertDataTable<AccountApplicationHistory>(dataTable);

                        dgAccountApplicationHistory.ItemsSource = AccountApplicationHistories;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillAccountApplications(int? orderBy)
        {
            try
            {
                List<AccountApplication> AccountApplicationList2 = new List<AccountApplication>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"AccountApplications?orderBy={orderBy}"))
                    //using (var response = await httpClient.GetAsync(App.hostString + $"AccountApplications"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        AccountApplications = JsonConvert.DeserializeObject<List<AccountApplication>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(AccountApplications, 2);
                        AccountApplicationList2 = DataGridHandler.ConvertDataTable<AccountApplication>(dataTable);

                        dgAccApps.ItemsSource = AccountApplications;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillCreditAgreements(string orderBy, int? searchUser)
        {
            try
            {
                List<CreditAgreement> CreditAgreementList2 = new List<CreditAgreement>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"CreditAgreements?orderBy={orderBy}&searchuser={searchUser}"))
                    //using (var response = await httpClient.GetAsync(App.hostString + $"CreditAgreements"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        CreditAgreements = JsonConvert.DeserializeObject<List<CreditAgreement>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(CreditAgreements, 2);
                        CreditAgreementList2 = DataGridHandler.ConvertDataTable<CreditAgreement>(dataTable);

                        dgCredAgs.ItemsSource = CreditAgreements;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillCreditApplications(int? orderBy)
        {
            try
            {
                List<CreditApplication> CreditApplicationList2 = new List<CreditApplication>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"CreditApplications?orderBy={orderBy}"))
                    //using (var response = await httpClient.GetAsync(App.hostString + $"CreditApplications"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        CreditApplications = JsonConvert.DeserializeObject<List<CreditApplication>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(CreditApplications, 2);
                        CreditApplicationList2 = DataGridHandler.ConvertDataTable<CreditApplication>(dataTable);

                        dgCredApps.ItemsSource = CreditApplications;

                        cmbCredAgAppID.ItemsSource = CreditApplicationList2;
                        cmbCredAgAppID.DisplayMemberPath = "IdCreditApplication";
                        cmbCredAgAppID.SelectedValuePath = "IdCreditApplication";

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillAccountTypes()
        {
            try
            {
                List<AccountType> AccountTypeList2 = new List<AccountType>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + "AccountTypes"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        AccountTypes = JsonConvert.DeserializeObject<List<AccountType>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(AccountTypes, 2);
                        AccountTypeList2 = DataGridHandler.ConvertDataTable<AccountType>(dataTable);

                        cmbAccType.ItemsSource = AccountTypeList2;
                        cmbAccType.DisplayMemberPath = "NameAccountType";
                        cmbAccType.SelectedValuePath = "IdAccountType";

                        cmbTypeAccApp.ItemsSource = AccountTypeList2;
                        cmbTypeAccApp.DisplayMemberPath = "NameAccountType";
                        cmbTypeAccApp.SelectedValuePath = "IdAccountType";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillAccounts(string orderBy)
        {
            try
            {
                List<Account> AccountList2 = new List<Account>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"Accounts?orderBy={orderBy}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Accounts = JsonConvert.DeserializeObject<List<Account>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(Accounts, 2);
                        AccountList2 = DataGridHandler.ConvertDataTable<Account>(dataTable);

                        dgAccs.ItemsSource = Accounts;

                        cmbAccountTransaction.ItemsSource = AccountList2;
                        cmbAccountTransaction.DisplayMemberPath = "IdAccount";
                        cmbAccountTransaction.SelectedValuePath = "IdAccount";

                        tbCardAccount.ItemsSource = AccountList2;
                        tbCardAccount.DisplayMemberPath = "IdAccount";
                        tbCardAccount.SelectedValuePath = "IdAccount";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillUsers(string orderBy, string searchF)
        {
            try
            {
                List<User> userList2 = new List<User>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + $"Users?orderBy={orderBy}&search={searchF}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Users = JsonConvert.DeserializeObject<List<User>>(apiResponse);

                        dgUser.ItemsSource = Users;

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(Users, 2);
                        userList2 = DataGridHandler.ConvertDataTable<User>(dataTable);

                        cmbAccUser.ItemsSource = userList2;
                        cmbAccUser.SelectedValuePath = "IdUser";

                        cmbCredAgUser.ItemsSource = userList2;
                        cmbCredAgUser.SelectedValuePath = "IdUser";

                        cmbCredAppUser.ItemsSource = userList2;
                        cmbCredAppUser.SelectedValuePath = "IdUser";

                        cmbAccAppUser.ItemsSource = userList2;
                        cmbAccAppUser.SelectedValuePath = "IdUser";

                        cmbCredAgUserSort.ItemsSource = userList2;
                        cmbCredAgUserSort.SelectedValuePath = "IdUser";

                        cmbAccUser.DisplayMemberPath = "FullName";
                        cmbCredAgUser.DisplayMemberPath = "FullName";
                        cmbCredAppUser.DisplayMemberPath = "FullName";
                        cmbAccAppUser.DisplayMemberPath = "FullName";
                        cmbCredAgUserSort.DisplayMemberPath = "FullName";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillRoles()
        {
            try
            {
                List<Role> Roles1 = new List<Role>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + "Roles"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Roles = JsonConvert.DeserializeObject<List<Role>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(Roles, 2);
                        Roles1 = DataGridHandler.ConvertDataTable<Role>(dataTable);

                        cmbRoleUser.ItemsSource = Roles1;
                        cmbRoleUser.DisplayMemberPath = "NameRole";
                        cmbRoleUser.SelectedValuePath = "IdRole";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillTransaction()
        {
            try
            {
                List<Transaction> Transactions1 = new List<Transaction>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + "Transactions" + $"?orderBy={orderTransaction}&orderbyTranType={orderTransactionType}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Transactions = JsonConvert.DeserializeObject<List<Transaction>>(apiResponse);

                        dgTransactions.ItemsSource = Transactions;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        private async void fillTransactionType()
        {
            try
            {
                List<TransactionType> TransactionTypes1 = new List<TransactionType>();

                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(App.hostString + "TransactionTypes"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        TransactionTypes = JsonConvert.DeserializeObject<List<TransactionType>>(apiResponse);

                        DataTable dataTable = new DataTable();
                        dataTable = DataGridHandler.CreateDataTable(TransactionTypes, 2);
                        TransactionTypes1 = DataGridHandler.ConvertDataTable<TransactionType>(dataTable);

                        cmbTypeTransaction.ItemsSource = TransactionTypes1;
                        cmbTypeTransaction.DisplayMemberPath = "NameTransactionType";
                        cmbTypeTransaction.SelectedValuePath = "IdTransactionType";

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
        }

        // Обработчик события при изменении выбранного элемента в комбобоксе для сортировки пользователей в списке кредитных договоров
        private void cmbCredAgUserSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillCreditAgreements("true", (int?)cmbCredAgUserSort.SelectedValue);
        }

        // Обработчики событий изменения состояния чекбокса и текстбокса для фильтрации пользователей
        private void chArchive_Unchecked(object sender, RoutedEventArgs e)
        {
            fillUsers("false", search);
        }

        private void chArchive_Checked(object sender, RoutedEventArgs e)
        {
            fillUsers("true", search);
        }

        private void tbSearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            search = tbSearchUser.Text;
            fillUsers(chArchive.IsChecked.ToString(), search);
        }

        // Обработчик выбора пользователя в таблице
        private void dgUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgUser.Items.Count != 0 && dgUser.SelectedItems.Count != 0)
                {
                    User dataRow = (User)dgUser.SelectedItems[0];
                    UserID = dataRow.IdUser;
                    cmbRoleUser.SelectedValue = dataRow.RoleId;
                    cmbRoleUserINT = dataRow.RoleId;
                    FUser.Text = dataRow.FirstName;
                    IUser.Text = dataRow.SecondName;
                    OUser.Text = dataRow.MiddleName;
                    BirthdayUser.SelectedDate = dataRow.Birthday;
                    SerPassUser.Text = dataRow.PassportSeries;
                    NumPassUser.Text = dataRow.PassportNumber;
                    LoginUser.Text = dataRow.Login;
                    PasswordUser.Text = dataRow.Password;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Обработчик нажатия кнопки "Добавить пользователя"
        private async void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(FUser.Text))
                {
                    if (!string.IsNullOrEmpty(IUser.Text))
                    {
                        if (!string.IsNullOrEmpty(OUser.Text))
                        {
                            if (!string.IsNullOrEmpty(BirthdayUser.Text))
                            {
                                if (!string.IsNullOrEmpty(SerPassUser.Text))
                                {
                                    if (!string.IsNullOrEmpty(NumPassUser.Text))
                                    {
                                        if (!string.IsNullOrEmpty(LoginUser.Text))
                                        {
                                            if (!string.IsNullOrEmpty(PasswordUser.Text))
                                            {
                                                User us = new User();
                                                User us1 = new User();
                                                us.FirstName = FUser.Text;
                                                us.SecondName = IUser.Text;
                                                us.MiddleName = OUser.Text;
                                                us.Birthday = BirthdayUser.SelectedDate;
                                                us.PassportNumber = NumPassUser.Text;
                                                us.PassportSeries = SerPassUser.Text;
                                                us.Login = LoginUser.Text;
                                                us.Password = PasswordUser.Text;
                                                us.RoleId = cmbRoleUser.SelectedIndex + 1;
                                                us.Salt = "";
                                                us.UserDeleted = true;

                                                using (var httpClient = new HttpClient())
                                                {
                                                    StringContent content = new StringContent(JsonConvert.SerializeObject(us), Encoding.UTF8, "application/json");
                                                    using (var response = await httpClient.PostAsync("https://localhost:7154/Authorize", content))
                                                    {
                                                        string apiResponse = await response.Content.ReadAsStringAsync();
                                                        us1 = JsonConvert.DeserializeObject<User>(apiResponse);
                                                    }
                                                }
                                                fillUsers("true", null);
                                                UserID = 0;
                                                FUser.Text = null;
                                                IUser.Text = null;
                                                OUser.Text = null;
                                                BirthdayUser.SelectedDate = null;
                                                NumPassUser.Text = null;
                                                SerPassUser.Text = null;
                                                LoginUser.Text = null;
                                                PasswordUser.Text = null;
                                                cmbRoleUser.SelectedIndex = 0;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Все поля должны быть заполнено!");
                                                PasswordUser.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Все поля должны быть заполнено!");
                                            LoginUser.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Все поля должны быть заполнено!");
                                        NumPassUser.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Все поля должны быть заполнено!");
                                    SerPassUser.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Все поля должны быть заполнено!");
                                BirthdayUser.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Все поля должны быть заполнено!");
                            OUser.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнено!");
                        IUser.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнено!");
                    FUser.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Обработчик нажатия кнопки "Изменить пользователя"
        private async void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(FUser.Text))
                {
                    if (!string.IsNullOrEmpty(IUser.Text))
                    {
                        if (!string.IsNullOrEmpty(OUser.Text))
                        {
                            if (!string.IsNullOrEmpty(BirthdayUser.Text))
                            {
                                if (!string.IsNullOrEmpty(SerPassUser.Text))
                                {
                                    if (!string.IsNullOrEmpty(NumPassUser.Text))
                                    {
                                        if (!string.IsNullOrEmpty(LoginUser.Text))
                                        {
                                            if (!string.IsNullOrEmpty(PasswordUser.Text))
                                            {
                                                if (UserID != 0)
                                                {
                                                    User us = new User();
                                                    User us1 = new User();
                                                    us.IdUser = UserID;
                                                    us.FirstName = FUser.Text;
                                                    us.SecondName = IUser.Text;
                                                    us.MiddleName = OUser.Text;
                                                    us.Birthday = BirthdayUser.SelectedDate;
                                                    us.PassportNumber = NumPassUser.Text;
                                                    us.PassportSeries = SerPassUser.Text;
                                                    us.Login = LoginUser.Text;
                                                    us.Password = PasswordUser.Text;
                                                    us.RoleId = cmbRoleUser.SelectedIndex + 1;
                                                    us.Salt = "";
                                                    us.UserDeleted = true;

                                                    using (var httpClient = new HttpClient())
                                                    {
                                                        StringContent content = new StringContent(JsonConvert.SerializeObject(us), Encoding.UTF8, "application/json");
                                                        using (var response = await httpClient.PutAsync(App.hostString + $"Users/{UserID}", content))
                                                        {
                                                            string apiResponse = await response.Content.ReadAsStringAsync();
                                                            us1 = JsonConvert.DeserializeObject<User>(apiResponse);
                                                        }
                                                    }
                                                    fillUsers("true", null);
                                                    UserID = 0;
                                                    FUser.Text = null;
                                                    IUser.Text = null;
                                                    OUser.Text = null;
                                                    BirthdayUser.SelectedDate = null;
                                                    NumPassUser.Text = null;
                                                    SerPassUser.Text = null;
                                                    LoginUser.Text = null;
                                                    PasswordUser.Text = null;
                                                    cmbRoleUser.SelectedIndex = 0;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Все поля должны быть заполнено!");
                                                PasswordUser.Focus();
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Все поля должны быть заполнено!");
                                            LoginUser.Focus();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Все поля должны быть заполнено!");
                                        NumPassUser.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Все поля должны быть заполнено!");
                                    SerPassUser.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Все поля должны быть заполнено!");
                                BirthdayUser.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Все поля должны быть заполнено!");
                            OUser.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнено!");
                        IUser.Focus();
                    }

                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнено!");
                    FUser.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Обработчик нажатия кнопки "Удалить пользователя"
        private async void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserID != 0)
            {
                int[] userIdArray = new int[] { UserID };

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(userIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Users/UsersIDDelete", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                fillUsers("true", null);
                UserID = 0;
                FUser.Text = null;
                IUser.Text = null;
                OUser.Text = null;
                BirthdayUser.SelectedDate = null;
                NumPassUser.Text = null;
                SerPassUser.Text = null;
                LoginUser.Text = null;
                PasswordUser.Text = null;
                cmbRoleUser.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Выберите пользователя для архивирования!");
            }
        }

        // Обработчик нажатия кнопки "Восстановить пользователя из архива"
        private async void btnUnArchiveUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserID != 0)
            {
                int[] userIdArray = new int[] { UserID };

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(userIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Users/UsersIDReturn", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                fillUsers("true", null);
                UserID = 0;
                FUser.Text = null;
                IUser.Text = null;
                OUser.Text = null;
                BirthdayUser.SelectedDate = null;
                NumPassUser.Text = null;
                SerPassUser.Text = null;
                LoginUser.Text = null;
                PasswordUser.Text = null;
                cmbRoleUser.SelectedIndex = 0;
                chArchive.IsChecked = true;
            }
            else
            {
                MessageBox.Show("Выберите пользователя для архивирования!");
            }
        }

        // Обработчики кнопок типов транзакций
        private void btnTransactionMinus_Click(object sender, RoutedEventArgs e)
        {
            orderTransactionType = 2;
            fillTransaction();
        }

        private void btnTransactionPlus_Click(object sender, RoutedEventArgs e)
        {
            orderTransactionType = 1;
            fillTransaction();
        }

        private void btnTransactionAll_Click(object sender, RoutedEventArgs e)
        {
            orderTransactionType = null;
            orderTransaction = "";
            fillTransaction();
        }

        // Обработчик выбора транзакции в таблице
        private void dgTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgTransactions.Items.Count != 0 && dgTransactions.SelectedItems.Count != 0)
                {
                    Transaction dataRow = (Transaction)dgTransactions.SelectedItems[0];
                    TransactionID = dataRow.IdTransaction;
                    cmbTypeTransaction.SelectedIndex = dataRow.TransactionTypeId;
                    cmbTypeTransactionINT = dataRow.TransactionTypeId;
                    cmbAccountTransaction.SelectedValue = dataRow.TransactionAccountId;
                    NameTransaction.Text = dataRow.NameTransaction;
                    DateTransaction.SelectedDate = dataRow.DateTransaction;
                    SumTransaction.Text = dataRow.SummTransaction.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Обработчики чекбокса "Архивированные транзакции"
        private void chArchiveTransaction_Checked(object sender, RoutedEventArgs e)
        {
            orderTransaction = "true";
            fillTransaction();
        }

        private void chArchiveTransaction_Unchecked(object sender, RoutedEventArgs e)
        {
            orderTransaction = "false";
            fillTransaction();
        }

        // Добавление новой транзакции
        private async void btnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед созданием транзакции
                if (!string.IsNullOrEmpty(NameTransaction.Text) && cmbTypeTransaction.SelectedIndex >= 0 && cmbAccountTransaction.SelectedIndex >= 0 &&
                    !string.IsNullOrEmpty(SumTransaction.Text) && DateTransaction.SelectedDate != null)
                {
                    Transaction tr = new Transaction();
                    Transaction tr1 = new Transaction();

                    // Заполнение данных транзакции
                    tr.NameTransaction = NameTransaction.Text;
                    tr.TransactionTypeId = cmbTypeTransactionINT;
                    tr.TransactionAccountId = cmbAccountTransaction.SelectedValue.ToString();
                    tr.SummTransaction = SumTransaction.Text;
                    tr.DateTransaction = DateTransaction.SelectedDate.Value;
                    tr.TransactionDeleted = false;

                    // Отправка запроса на создание транзакции
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(tr), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("https://localhost:7154/api/Transactions", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            tr1 = JsonConvert.DeserializeObject<Transaction>(apiResponse);
                        }
                    }

                    // Обновление списка транзакций и очистка полей ввода
                    fillTransaction();
                    TransactionID = 0;
                    NameTransaction.Text = null;
                    cmbTypeTransaction.SelectedIndex = 0;
                    cmbAccountTransaction.SelectedIndex = 0;
                    SumTransaction.Text = null;
                    DateTransaction.SelectedDate = null;
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    NameTransaction.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Изменение выбранной транзакции
        private async void btnChangeTransaction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед изменением транзакции
                if (!string.IsNullOrEmpty(NameTransaction.Text))
                {
                    if (TransactionID != 0)
                    {
                        Transaction tr = new Transaction();
                        Transaction tr1 = new Transaction();

                        // Заполнение данных измененной транзакции
                        tr.IdTransaction = TransactionID;
                        tr.NameTransaction = NameTransaction.Text;
                        tr.TransactionTypeId = cmbTypeTransactionINT;
                        tr.TransactionAccountId = cmbAccountTransaction.SelectedValue.ToString();
                        tr.SummTransaction = SumTransaction.Text;
                        tr.DateTransaction = DateTransaction.SelectedDate.Value;
                        tr.TransactionDeleted = false;

                        // Отправка запроса на изменение транзакции
                        using (var httpClient = new HttpClient())
                        {
                            StringContent content = new StringContent(JsonConvert.SerializeObject(tr), Encoding.UTF8, "application/json");
                            using (var response = await httpClient.PutAsync(App.hostString + $"Transactions/{TransactionID}", content))
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                tr1 = JsonConvert.DeserializeObject<Transaction>(apiResponse);
                            }
                        }

                        // Обновление списка транзакций
                        fillTransaction();
                    }
                    else
                    {
                        MessageBox.Show("Выберите транзакцию для изменения!");
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    NameTransaction.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Удаление выбранной транзакции
        private async void btnDeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionID != 0)
            {
                int[] TransactionIdArray = new int[] { TransactionID };

                // Отправка запроса на удаление транзакции
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(TransactionIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Transactions/TransactionsIDDelete", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка транзакций и очистка полей
                fillTransaction();
                TransactionID = 0;
                NameTransaction.Text = null;
                cmbTypeTransaction.SelectedIndex = 0;
                cmbAccountTransaction.SelectedIndex = 0;
                SumTransaction.Text = null;
                DateTransaction.SelectedDate = null;
            }
            else
            {
                MessageBox.Show("Выберите транзакцию для удаления!");
            }
        }

        // Восстановление архивированной транзакции
        private async void btnUnArchiveTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionID != 0)
            {
                int[] TransactionIdArray = new int[] { TransactionID };

                // Отправка запроса на восстановление архивированной транзакции
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(TransactionIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Transactions/TransactionsIDReturn", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка транзакций и очистка полей
                fillTransaction();
                TransactionID = 0;
                NameTransaction.Text = null;
                cmbTypeTransaction.SelectedIndex = 0;
                cmbAccountTransaction.SelectedIndex = 0;
                SumTransaction.Text = null;
                DateTransaction.SelectedDate = null;
            }
            else
            {
                MessageBox.Show("Выберите транзакцию для восстановления!");
            }
        }

        // Создание резервной копии базы данных
        private async void doBackUp_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            var result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string backupPath = dialog.SelectedPath;

                // Отправка запроса на создание резервной копии базы данных
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{App.hostString}BackUp/BackupDatabase?backupPath={backupPath}");
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(apiResponse);
                }
            }
        }

        // Восстановление базы данных из резервной копии
        private async void doRestore_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "BAK Files|*.bak",
                Title = $"Сохранение {"BackUpDatabase"}",
            };
            bool? result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                string backupFilePath = dialog.FileName;

                // Отправка запроса на восстановление базы данных из резервной копии
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"{App.hostString}BackUp/RestoreDatabase?backupFilePath={backupFilePath}");
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(apiResponse);
                }
            }
        }

        // Завершение работы приложения
        private void exitSys_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        // Обработка события закрытия окна
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        // Обработка события нажатия на элемент Border для отображения данных карты
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Card card)
            {
                tbCardNum.Text = card.CardNumber;
                CardID = tbCardNum.Text;
                tbCardHolder.Text = card.CardHolder;
                tbCardVal.Text = card.Validity;
                tbCardCCV.Text = card.Ccv;
                tbCardAccount.SelectedValue = card.CardAccountId;
                tbCardDelReason.Text = card.Card_Deletion_Reason;
            }
        }

        // Добавление новой карты
        private async void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед созданием карты
                if (!string.IsNullOrEmpty(tbCardNum.Text) && !string.IsNullOrEmpty(tbCardHolder.Text) && !string.IsNullOrEmpty(tbCardVal.Text) &&
                    !string.IsNullOrEmpty(tbCardCCV.Text) && tbCardAccount.SelectedIndex >= 0)
                {
                    Card card = new Card();
                    Card c1 = new Card();

                    // Заполнение данных карты
                    card.CardNumber = tbCardNum.Text;
                    card.CardHolder = tbCardHolder.Text;
                    card.Validity = tbCardVal.Text;
                    card.Ccv = tbCardCCV.Text;
                    card.CardAccountId = tbCardAccount.SelectedValue.ToString();
                    card.CardDeleted = true;
                    card.Card_Deletion_Reason = "-";

                    // Отправка запроса на создание карты
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync(App.hostString + "Cards", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            c1 = JsonConvert.DeserializeObject<Card>(apiResponse);
                        }
                    }

                    // Обновление списка карт и очистка полей ввода
                    fillCards("true");
                    chArchiveCards.IsChecked = true;
                    tbCardNum.Text = null;
                    tbCardHolder.Text = null;
                    tbCardVal.Text = null;
                    tbCardCCV.Text = null;
                    tbCardAccount.SelectedIndex = 0;
                    tbCardDelReason.Text = null;
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    tbCardNum.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Изменение выбранной карты
        private async void btnChangeCard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед изменением карты
                if (!string.IsNullOrEmpty(tbCardNum.Text))
                {
                    Card card = new Card();
                    Card c1 = new Card();

                    // Заполнение данных измененной карты
                    card.CardNumber = CardID;
                    card.CardHolder = tbCardHolder.Text;
                    card.Validity = tbCardVal.Text;
                    card.Ccv = tbCardCCV.Text;
                    card.CardAccountId = tbCardAccount.SelectedValue.ToString();
                    card.CardDeleted = true;
                    card.Card_Deletion_Reason = "-";

                    // Отправка запроса на изменение карты
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync(App.hostString + $"Cards/{CardID}", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            c1 = JsonConvert.DeserializeObject<Card>(apiResponse);
                        }
                    }

                    // Обновление списка карт
                    fillCards("true");
                    chArchiveCards.IsChecked = true;
                    tbCardNum.Text = null;
                    tbCardHolder.Text = null;
                    tbCardVal.Text = null;
                    tbCardCCV.Text = null;
                    tbCardAccount.SelectedIndex = 0;
                    tbCardDelReason.Text = null;
                }
                else
                {
                    MessageBox.Show("Выберите карту для изменения!");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }
        // Удаление выбранной карты с указанием причины
        private async void btnDeleteCard_Click(object sender, RoutedEventArgs e)
        {
            if (CardID != "" && tbCardDelReason.Text != null)
            {
                string[] CardIdArray = new string[] { CardID };

                // Отправка запроса на удаление карты с указанием причины
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(CardIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + $"Cards/CardsIDDelete?deletionReason={tbCardDelReason.Text}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка карт и очистка полей
                fillCards("true");
                chArchiveCards.IsChecked = true;
                tbCardNum.Text = null;
                tbCardHolder.Text = null;
                tbCardVal.Text = null;
                tbCardCCV.Text = null;
                tbCardAccount.SelectedIndex = 0;
                tbCardDelReason.Text = null;
            }
            else
            {
                MessageBox.Show("Выберите карту для архивирования и укажите причину!");
            }
        }

        // Восстановление архивированной карты
        private async void btnUnArchiveCard_Click(object sender, RoutedEventArgs e)
        {
            if (CardID != "")
            {
                string[] CardIdArray = new string[] { CardID };

                // Отправка запроса на восстановление архивированной карты
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(CardIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Cards/CardsIDReturn", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка карт и очистка полей
                fillCards("true");
                chArchiveCards.IsChecked = true;
                tbCardNum.Text = null;
                tbCardHolder.Text = null;
                tbCardVal.Text = null;
                tbCardCCV.Text = null;
                tbCardAccount.SelectedIndex = 0;
                tbCardDelReason.Text = null;
            }
            else
            {
                MessageBox.Show("Выберите карту для восстановления!");
            }
        }

        // Обработка события изменения состояния флажка архивации карт
        private void chArchiveCards_Checked(object sender, RoutedEventArgs e)
        {
            // Обновление списка карт (архивированных или неархивированных) в зависимости от состояния флажка
            fillCards("true");
        }

        // Обработка события изменения состояния флажка архивации карт
        private void chArchiveCards_Unchecked(object sender, RoutedEventArgs e)
        {
            // Обновление списка карт (архивированных или неархивированных) в зависимости от состояния флажка
            fillCards("false");
        }

        // Обработка события изменения состояния флажка архивации счетов
        private void chArchiveAccs_Checked(object sender, RoutedEventArgs e)
        {
            // Обновление списка счетов (архивированных или неархивированных) в зависимости от состояния флажка
            fillAccounts("true");
        }

        // Обработка события изменения состояния флажка архивации счетов
        private void chArchiveAccs_Unchecked(object sender, RoutedEventArgs e)
        {
            // Обновление списка счетов (архивированных или неархивированных) в зависимости от состояния флажка
            fillAccounts("false");
        }

        // Обработка события изменения выделенного элемента в таблице счетов
        private void dgAccs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgAccs.Items.Count != 0 && dgAccs.SelectedItems.Count != 0)
                {
                    Account dataRow = (Account)dgAccs.SelectedItems[0];

                    tbAccNum.Text = dataRow.IdAccount;
                    AccID = dataRow.IdAccount;
                    tbAccBalance.Text = dataRow.Balance.ToString();
                    tbAccPercent.Text = dataRow.Percent.ToString();
                    cmbAccType.SelectedValue = dataRow.TypeId;
                    cmbAccUser.SelectedValue = dataRow.UserAccountId;

                    // Отображение причины удаления счета, если счет архивирован
                    if (dataRow.AccountDeleted)
                    {
                        tbAccDelReason.Text = dataRow.Account_Deletion_Reason;
                    }
                    else
                    {
                        tbAccDelReason.Text = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление нового счета
        private async void btnAddAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед созданием счета
                if (!string.IsNullOrEmpty(tbAccNum.Text) && !string.IsNullOrEmpty(tbAccBalance.Text) &&
                    !string.IsNullOrEmpty(tbAccPercent.Text) && cmbAccType.SelectedIndex >= 0 && cmbAccUser.SelectedIndex >= 0)
                {
                    Account account = new Account();
                    Account a1 = new Account();

                    // Заполнение данных нового счета
                    account.IdAccount = tbAccNum.Text;
                    account.Balance = decimal.Parse(tbAccBalance.Text);
                    account.Percent = int.Parse(tbAccPercent.Text);
                    account.TypeId = (int)cmbAccType.SelectedValue;
                    account.UserAccountId = (int)cmbAccUser.SelectedValue;
                    account.AccountDeleted = true;
                    account.Account_Deletion_Reason = "-";

                    // Отправка запроса на создание счета
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync(App.hostString + "Accounts", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            a1 = JsonConvert.DeserializeObject<Account>(apiResponse);
                        }
                    }

                    // Обновление списка счетов и очистка полей ввода
                    fillAccounts("true");
                    chArchiveAccs.IsChecked = true;
                    tbAccNum.Text = null;
                    tbAccBalance.Text = null;
                    tbAccPercent.Text = null;
                    cmbAccType.SelectedIndex = 0;
                    cmbAccUser.SelectedIndex = 0;
                    tbAccDelReason.Text = null;
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Изменение выбранного счета
        private async void btnChangeAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед изменением счета
                if (!string.IsNullOrEmpty(tbAccNum.Text))
                {
                    Account account = new Account();
                    Account a1 = new Account();

                    // Заполнение данных измененного счета
                    account.IdAccount = AccID;
                    account.Balance = decimal.Parse(tbAccBalance.Text);
                    account.Percent = int.Parse(tbAccPercent.Text);
                    account.TypeId = (int)cmbAccType.SelectedValue;
                    account.UserAccountId = (int)cmbAccUser.SelectedValue;
                    account.AccountDeleted = true;
                    account.Account_Deletion_Reason = "-";

                    // Отправка запроса на изменение счета
                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PutAsync(App.hostString + $"Accounts/{tbAccNum.Text}", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            a1 = JsonConvert.DeserializeObject<Account>(apiResponse);
                        }
                    }

                    // Обновление списка счетов
                    fillAccounts("true");
                    chArchiveAccs.IsChecked = true;
                    tbAccNum.Text = null;
                    tbAccBalance.Text = null;
                    tbAccPercent.Text = null;
                    cmbAccType.SelectedIndex = 0;
                    cmbAccUser.SelectedIndex = 0;
                    tbAccDelReason.Text = null;
                }
                else
                {
                    MessageBox.Show("Выберите счёт для изменения!");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Удаление выбранного счета с указанием причины
        private async void btnDeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbAccNum.Text) && tbAccDelReason.Text != null)
            {
                string[] accountIdArray = new string[] { tbAccNum.Text };

                // Отправка запроса на удаление счета с указанием причины
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(accountIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + $"Accounts/AccountsIDDelete?deletionReason={tbAccDelReason.Text}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка счетов и очистка полей
                fillAccounts("true");
                chArchiveAccs.IsChecked = true;
                tbAccNum.Text = null;
                tbAccBalance.Text = null;
                tbAccPercent.Text = null;
                cmbAccType.SelectedIndex = 0;
                cmbAccUser.SelectedIndex = 0;
                tbAccDelReason.Text = null;
            }
            else
            {
                MessageBox.Show("Выберите счёт для архивирования и укажите причину!");
            }
        }

        // Восстановление архивированного счета
        private async void btnUnArchiveAccount_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbAccNum.Text))
            {
                string[] accountIdArray = new string[] { tbAccNum.Text };

                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(accountIdArray), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "Accounts/AccountsIDReturn", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }
                fillAccounts("true");
                chArchiveAccs.IsChecked = true;
                tbAccNum.Text = null;
                tbAccBalance.Text = null;
                tbAccPercent.Text = null;
                cmbAccType.SelectedIndex = 0;
                cmbAccUser.SelectedIndex = 0;
                tbAccDelReason.Text = null;
            }
            else
            {
                MessageBox.Show("Выберите счёт для архивирования!");
            }
        }

        private async void btnAddCredAgg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbCredAgNum.Text))
                {
                    if (tbCredAgDate1.SelectedDate != null)
                    {
                        if (tbCredAgDate2.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(tbCreditRate.Text))
                            {
                                if (cmbCredAgUser.SelectedIndex >= 0)
                                {
                                    if (cmbCredAgAppID.SelectedIndex >= 0)
                                    {
                                        CreditAgreement cr = new CreditAgreement();
                                        cr.IdCreditAgreement = tbCredAgNum.Text;
                                        cr.DateDrawing = tbCredAgDate1.SelectedDate.Value;
                                        cr.DateTermination = tbCredAgDate2.SelectedDate.Value;
                                        cr.CreditRate = Convert.ToDecimal(tbCreditRate.Text);
                                        cr.CreditAgreementUserId = (cmbCredAgUser.SelectedItem as User).IdUser;
                                        cr.CreditApplicationId = (cmbCredAgAppID.SelectedItem as CreditApplication).IdCreditApplication;

                                        using (var httpClient = new HttpClient())
                                        {
                                            StringContent content = new StringContent(JsonConvert.SerializeObject(cr), Encoding.UTF8, "application/json");
                                            using (var response = await httpClient.PostAsync(App.hostString + "CreditAgreements", content))
                                            {
                                                string apiResponse = await response.Content.ReadAsStringAsync();
                                                cr = JsonConvert.DeserializeObject<CreditAgreement>(apiResponse);
                                            }
                                        }

                                        fillCreditAgreements("true", null);

                                        tbCredAgNum.Clear();
                                        tbCredAgDate1.ClearValue(DatePicker.SelectedDateProperty);
                                        tbCredAgDate2.ClearValue(DatePicker.SelectedDateProperty);
                                        tbCreditRate.Clear();
                                        cmbCredAgUser.SelectedIndex = -1;
                                        cmbCredAgAppID.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Все поля должны быть заполнены!");
                                        cmbCredAgAppID.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Все поля должны быть заполнены!");
                                    cmbCredAgUser.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Все поля должны быть заполнены!");
                                tbCreditRate.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Все поля должны быть заполнены!");
                            tbCredAgDate2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнены!");
                        tbCredAgDate1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    tbCredAgNum.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        private async void btnChangeCredAgg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbCredAgNum.Text))
                {
                    if (tbCredAgDate1.SelectedDate != null)
                    {
                        if (tbCredAgDate2.SelectedDate != null)
                        {
                            if (!string.IsNullOrEmpty(tbCreditRate.Text))
                            {
                                if (cmbCredAgUser.SelectedIndex >= 0)
                                {
                                    if (cmbCredAgAppID.SelectedIndex >= 0)
                                    {
                                        CreditAgreement cr = new CreditAgreement();
                                        cr.IdCreditAgreement = CredAgID;
                                        cr.DateDrawing = tbCredAgDate1.SelectedDate.Value;
                                        cr.DateTermination = tbCredAgDate2.SelectedDate.Value;
                                        cr.CreditRate = Convert.ToDecimal(tbCreditRate.Text);
                                        cr.CreditAgreementUserId = (cmbCredAgUser.SelectedItem as User).IdUser;
                                        cr.CreditApplicationId = (cmbCredAgAppID.SelectedItem as CreditApplication).IdCreditApplication;
                                        cr.CreditAgreementDeleted = true;

                                        using (var httpClient = new HttpClient())
                                        {
                                            StringContent content = new StringContent(JsonConvert.SerializeObject(cr), Encoding.UTF8, "application/json");
                                            using (var response = await httpClient.PutAsync(App.hostString + $"CreditAgreements/{CredAgID}", content))
                                            {
                                                string apiResponse = await response.Content.ReadAsStringAsync();
                                                cr = JsonConvert.DeserializeObject<CreditAgreement>(apiResponse);
                                            }
                                        }

                                        fillCreditAgreements("true", null);

                                        tbCredAgNum.Clear();
                                        tbCredAgDate1.ClearValue(DatePicker.SelectedDateProperty);
                                        tbCredAgDate2.ClearValue(DatePicker.SelectedDateProperty);
                                        tbCreditRate.Clear();
                                        cmbCredAgUser.SelectedIndex = -1;
                                        cmbCredAgAppID.SelectedIndex = -1;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Все поля должны быть заполнены!");
                                        cmbCredAgAppID.Focus();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Все поля должны быть заполнены!");
                                    cmbCredAgUser.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Все поля должны быть заполнены!");
                                tbCreditRate.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Все поля должны быть заполнены!");
                            tbCredAgDate2.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнены!");
                        tbCredAgDate1.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите кредитный договор для изменения!");
                }
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }
        // Удаление выбранного кредитного договора
        private async void btnDeleteCredAgg_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCredAgNum.Text))
            {
                string credAgNum = tbCredAgNum.Text;

                // Отправка запроса на удаление кредитного договора
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(credAgNum), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "CreditAgreements/CreditAgreementsIDDelete", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка кредитных договоров
                fillCreditAgreements("true", null);
            }
            else
            {
                MessageBox.Show("Выберите кредитный договор для удаления!");
            }
        }

        // Восстановление архивированного кредитного договора
        private async void btnUnArchiveCredAgg_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCredAgNum.Text))
            {
                string credAgNum = tbCredAgNum.Text;

                // Отправка запроса на восстановление архивированного кредитного договора
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(credAgNum), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + "CreditAgreements/CreditAgreementsIDReturn", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                // Обновление списка кредитных договоров
                fillCreditAgreements("true", null);
            }
            else
            {
                MessageBox.Show("Выберите кредитный договор для разархивирования!");
            }
        }

        // Обработка события изменения состояния флажка архивации кредитных договоров
        private void chArchiveCredAgs_Checked(object sender, RoutedEventArgs e)
        {
            // Обновление списка кредитных договоров (архивированных или неархивированных) в зависимости от состояния флажка
            fillCreditAgreements("true", null);
        }

        // Обработка события изменения состояния флажка архивации кредитных договоров
        private void chArchiveCredAgs_Unchecked(object sender, RoutedEventArgs e)
        {
            // Обновление списка кредитных договоров (архивированных или неархивированных) в зависимости от состояния флажка
            fillCreditAgreements("false", null);
        }

        // Обработка события изменения выделенного элемента в таблице кредитных договоров
        private void dgCredAgs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCredAgs.SelectedItem != null)
            {
                CreditAgreement selectedCreditAgreement = dgCredAgs.SelectedItem as CreditAgreement;

                // Заполнение полей данными выбранного кредитного договора
                tbCredAgNum.Text = selectedCreditAgreement.IdCreditAgreement;
                CredAgID = selectedCreditAgreement.IdCreditAgreement;
                tbCredAgDate1.SelectedDate = selectedCreditAgreement.DateDrawing;
                tbCredAgDate2.SelectedDate = selectedCreditAgreement.DateTermination;
                tbCreditRate.Text = selectedCreditAgreement.CreditRate.ToString();
                cmbCredAgUser.SelectedValue = selectedCreditAgreement.CreditAgreementUserId;
                cmbCredAgAppID.SelectedValue = selectedCreditAgreement.CreditApplicationId;
            }
        }

        // Добавление новой заявки на кредит
        private async void btnAddCredAppg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед созданием заявки на кредит
                if (string.IsNullOrEmpty(tbCredAppNum.Text))
                {
                    MessageBox.Show("Пожалуйста, введите номер заявки.");
                    return;
                }

                if (string.IsNullOrEmpty(tbCredAppAmount.Text))
                {
                    MessageBox.Show("Пожалуйста, введите сумму заявки.");
                    return;
                }

                if (string.IsNullOrEmpty(tbCredAppPercent.Text))
                {
                    MessageBox.Show("Пожалуйста, укажите процент кредита.");
                    return;
                }

                if (cmbCredAppUser.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя.");
                    return;
                }

                if (cmbCredAppStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, укажите статус.");
                    return;
                }

                CreditApplication cr = new CreditApplication();
                cr.IdCreditApplication = tbCredAppNum.Text;
                cr.ApplicationAmount = Convert.ToDecimal(tbCredAppAmount.Text);
                cr.CreditDesiredPercentage = Convert.ToDecimal(tbCredAppPercent.Text);
                cr.CreditApplicationUserId = (cmbCredAppUser.SelectedItem as User).IdUser;
                cr.StatusId = (cmbCredAppStatus.SelectedItem as ApplicationStatus).IdApplicationStatus;

                // Отправка запроса на создание заявки на кредит
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cr), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(App.hostString + "CreditApplications", content);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cr = JsonConvert.DeserializeObject<CreditApplication>(apiResponse);
                }

                // Обновление списка заявок на кредит
                fillCreditApplications(null);

                // Очистка полей ввода
                tbCredAppNum.Clear();
                tbCredAppAmount.Clear();
                tbCredAppPercent.Clear();
                cmbCredAppUser.SelectedIndex = -1;
                cmbCredAppStatus.SelectedIndex = -1;
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Добавление новой заявки на открытие счета
        private async void btnAddAccApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед созданием заявки на открытие счета
                if (string.IsNullOrEmpty(tbAccAppNum.Text))
                {
                    MessageBox.Show("Пожалуйста, введите номер заявки на счет.");
                    return;
                }

                if (cmbAccAppUser.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя.");
                    return;
                }

                if (cmbAccAppStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, укажите статус.");
                    return;
                }

                if (cmbTypeAccApp.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите тип счета.");
                    return;
                }

                if (string.IsNullOrEmpty(tbAccAppPercent.Text))
                {
                    MessageBox.Show("Пожалуйста, введите желаемый процент по счету.");
                    return;
                }

                AccountApplication ac = new AccountApplication();
                ac.IdAccountApplication = tbAccAppNum.Text;
                ac.AccountApplicationUserId = (cmbAccAppUser.SelectedItem as User).IdUser;
                ac.StatusId = (cmbAccAppStatus.SelectedItem as ApplicationStatus).IdApplicationStatus;
                ac.TypeAccountId = (cmbTypeAccApp.SelectedItem as AccountType).IdAccountType;
                ac.AccountDesiredPercentage = Convert.ToDecimal(tbAccAppPercent.Text);
                ac.Bank_Card_Needed = cmbAccAppCardNeed.IsChecked ?? false;

                // Отправка запроса на создание заявки на открытие счета
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(ac), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync(App.hostString + "AccountApplications", content);
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ac = JsonConvert.DeserializeObject<AccountApplication>(apiResponse);
                }

                // Обновление списка заявок на открытие счета
                fillAccountApplications(null);

                // Очистка полей ввода
                tbAccAppNum.Clear();
                cmbAccAppUser.SelectedIndex = -1;
                cmbAccAppStatus.SelectedIndex = -1;
                cmbTypeAccApp.SelectedIndex = -1;
                tbAccAppPercent.Clear();
                cmbAccAppCardNeed.IsChecked = false;
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Изменение выбранной заявки на кредит
        private async void btnChangeCredAppg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка заполненности полей перед изменением заявки на кредит
                if (string.IsNullOrEmpty(tbCredAppNum.Text))
                {
                    MessageBox.Show("Пожалуйста, введите номер заявки.");
                    return;
                }

                if (string.IsNullOrEmpty(tbCredAppAmount.Text))
                {
                    MessageBox.Show("Пожалуйста, введите сумму заявки.");
                    return;
                }

                if (string.IsNullOrEmpty(tbCredAppPercent.Text))
                {
                    MessageBox.Show("Пожалуйста, укажите процент кредита.");
                    return;
                }

                if (cmbCredAppUser.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя.");
                    return;
                }

                if (cmbCredAppStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, укажите статус.");
                    return;
                }

                CreditApplication cr = new CreditApplication();
                cr.IdCreditApplication = CredAppID;
                cr.ApplicationAmount = Convert.ToDecimal(tbCredAppAmount.Text);
                cr.CreditDesiredPercentage = Convert.ToDecimal(tbCredAppPercent.Text);
                cr.CreditApplicationUserId = (cmbCredAppUser.SelectedItem as User).IdUser;
                cr.StatusId = (cmbCredAppStatus.SelectedItem as ApplicationStatus).IdApplicationStatus;

                // Отправка запроса на изменение заявки на кредит
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(cr), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + $"CreditApplications/{CredAppID}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cr = JsonConvert.DeserializeObject<CreditApplication>(apiResponse);
                    }
                }

                // Обновление списка заявок на кредит
                fillCreditApplications(null);

                // Очистка полей ввода
                tbCredAppNum.Clear();
                tbCredAppAmount.Clear();
                tbCredAppPercent.Clear();
                cmbCredAppUser.SelectedIndex = -1;
                cmbCredAppStatus.SelectedIndex = -1;
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }
        // Изменение заявки на открытие счета
        private async void btnChangeAccApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка наличия номера заявки
                if (string.IsNullOrEmpty(tbAccAppNum.Text))
                {
                    MessageBox.Show("Пожалуйста, введите номер заявки на счет.");
                    return;
                }

                // Проверка выбора пользователя
                if (cmbAccAppUser.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите пользователя.");
                    return;
                }

                // Проверка указания статуса
                if (cmbAccAppStatus.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, укажите статус.");
                    return;
                }

                // Проверка выбора типа счета
                if (cmbTypeAccApp.SelectedIndex < 0)
                {
                    MessageBox.Show("Пожалуйста, выберите тип счета.");
                    return;
                }

                // Проверка наличия процента по счету
                if (string.IsNullOrEmpty(tbAccAppPercent.Text))
                {
                    MessageBox.Show("Пожалуйста, введите желаемый процент по счету.");
                    return;
                }

                // Создание объекта заявки на счет
                AccountApplication ac = new AccountApplication();
                ac.IdAccountApplication = AccAppID;
                ac.AccountApplicationUserId = (cmbAccAppUser.SelectedItem as User).IdUser;
                ac.StatusId = (cmbAccAppStatus.SelectedItem as ApplicationStatus).IdApplicationStatus;
                ac.TypeAccountId = (cmbTypeAccApp.SelectedItem as AccountType).IdAccountType;
                ac.AccountDesiredPercentage = Convert.ToDecimal(tbAccAppPercent.Text);
                ac.Bank_Card_Needed = cmbAccAppCardNeed.IsChecked ?? false;

                // Отправка запроса на изменение заявки на счет
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(ac), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync(App.hostString + $"AccountApplications/{AccAppID}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ac = JsonConvert.DeserializeObject<AccountApplication>(apiResponse);
                    }
                }

                // Обновление списка заявок на счета
                fillAccountApplications(null);

                // Очистка полей ввода
                tbAccAppNum.Clear();
                cmbAccAppUser.SelectedIndex = -1;
                cmbAccAppStatus.SelectedIndex = -1;
                cmbTypeAccApp.SelectedIndex = -1;
                tbAccAppPercent.Clear();
                cmbAccAppCardNeed.IsChecked = false;
            }
            catch
            {
                MessageBox.Show("Не удалось провести операцию.");
            }
        }

        // Удаление заявки на кредит
        private async void btnDeleteCredAppg_Click(object sender, RoutedEventArgs e)
        {
            if (CredAppID != null)
            {
                await ChangeStatusCredit(CredAppID, 4);
            }
        }

        // Восстановление архивированной заявки на кредит
        private async void btnUnArchiveCredAppg_Click(object sender, RoutedEventArgs e)
        {
            if (CredAppID != null)
            {
                await ChangeStatusCredit(CredAppID, 3);
            }
        }

        // Удаление заявки на открытие счета
        private async void btnDeleteAccApp_Click(object sender, RoutedEventArgs e)
        {
            if (AccAppID != null)
            {
                await ChangeStatusAcc(AccAppID, 4);
            }
        }

        // Восстановление архивированной заявки на открытие счета
        private async void btnUnArchiveAccApp_Click(object sender, RoutedEventArgs e)
        {
            if (AccAppID != null)
            {
                await ChangeStatusAcc(AccAppID, 3);
            }
        }

        // Оформление заявки на открытие счета
        private async void btnOformAccApp_Click(object sender, RoutedEventArgs e)
        {
            if (AccAppID != null)
            {
                await ChangeStatusAcc(AccAppID, 1);
            }
        }

        // Оформление заявки на кредит
        private async void btnOformCredApp_Click(object sender, RoutedEventArgs e)
        {
            if (CredAppID != null)
            {
                await ChangeStatusCredit(CredAppID, 1);
            }
        }

        // Изменение статуса заявки на кредит
        private async Task ChangeStatusCredit(string appId, int newStatus)
        {
            using (var httpClient = new HttpClient())
            {
                var updateContent = new
                {
                    StatusId = newStatus,
                    IdAccountApplication = AccAppID,
                    AccountApplicationUserId = (cmbAccAppUser.SelectedItem as User).IdUser,
                    TypeAccountId = (cmbTypeAccApp.SelectedItem as AccountType).IdAccountType,
                    AccountDesiredPercentage = Convert.ToDecimal(tbAccAppPercent.Text),
                    Bank_Card_Needed = cmbAccAppCardNeed.IsChecked ?? false
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateContent), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(App.hostString + $"CreditApplications/{appId}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Не удалось обновить статус заявки: {apiResponse}");
                    }
                }
            }
            fillCreditApplications(null);
        }

        // Изменение статуса заявки на открытие счета
        private async Task ChangeStatusAcc(string appId, int newStatus)
        {
            using (var httpClient = new HttpClient())
            {
                var updateContent = new
                {
                    IdAccountApplication = AccAppID,
                    AccountApplicationUserId = (cmbAccAppUser.SelectedItem as User).IdUser,
                    StatusId = newStatus,
                    TypeAccountId = (cmbTypeAccApp.SelectedItem as AccountType).IdAccountType,
                    AccountDesiredPercentage = Convert.ToDecimal(tbAccAppPercent.Text),
                    Bank_Card_Needed = cmbAccAppCardNeed.IsChecked ?? false,
                };
                StringContent content = new StringContent(JsonConvert.SerializeObject(updateContent), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync(App.hostString + $"AccountApplications/{appId}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Не удалось обновить статус заявки: {apiResponse}");
                    }
                }
            }
            fillAccountApplications(null);
        }

        // Обработчик изменения выделенной строки в таблице заявок на кредит
        private void dgCredApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCredApps.SelectedItem != null)
            {
                CreditApplication selectedCreditApplication = dgCredApps.SelectedItem as CreditApplication;

                tbCredAppNum.Text = selectedCreditApplication.IdCreditApplication;
                CredAppID = selectedCreditApplication.IdCreditApplication;
                tbCredAppAmount.Text = selectedCreditApplication.ApplicationAmount.ToString();
                tbCredAppPercent.Text = selectedCreditApplication.CreditDesiredPercentage.ToString();
                cmbCredAppUser.SelectedValue = selectedCreditApplication.CreditApplicationUserId;
                cmbCredAppStatus.SelectedValue = selectedCreditApplication.StatusId;
            }
        }

        // Обработчик изменения выбранного статуса в фильтре заявок на кредит
        private void cmbCredAppStatusSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillCreditApplications((int)cmbCredAppStatusSort.SelectedValue);
        }

        // Обработчик изменения выбранного статуса в фильтре заявок на открытие счета
        private void cmbAccAppStatusSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillAccountApplications((int)cmbAccAppStatusSort.SelectedValue);
        }

        // Обработчик изменения выделенной строки в таблице заявок на открытие счета
        private void dgAccApps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgAccApps.SelectedItem != null)
            {
                AccountApplication selectedAccountApplication = dgAccApps.SelectedItem as AccountApplication;

                tbAccAppNum.Text = selectedAccountApplication.IdAccountApplication;
                AccAppID = selectedAccountApplication.IdAccountApplication;
                cmbTypeAccApp.SelectedValue = selectedAccountApplication.TypeAccountId;
                tbAccAppPercent.Text = selectedAccountApplication.AccountDesiredPercentage.ToString();
                cmbAccAppUser.SelectedValue = selectedAccountApplication.AccountApplicationUserId;
                cmbAccAppCardNeed.IsChecked = selectedAccountApplication.Bank_Card_Needed;
                cmbAccAppStatus.SelectedValue = selectedAccountApplication.StatusId;
            }
        }

        // Экспорт данных в CSV
        private void ExportToCsv<T>(IEnumerable<T> data, string title)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV Files|*.csv",
                Title = $"Сохранение {title}",
            };

            if (sfd.ShowDialog() == true)
            {
                using (var writer = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(data);
                }

                Process.Start("excel.exe", sfd.FileName);
            }
        }

        // Экспорт транзакций в CSV
        private void ExportTransactions_Click(object sender, RoutedEventArgs e)
        {
            ExportToCsv(Transactions, "транзакций");
        }

        // Экспорт аккаунтов в CSV
        private void ExportAccounts_Click(object sender, RoutedEventArgs e)
        {
            ExportToCsv(Accounts, "аккаунтов");
        }

        // Экспорт кредитов в CSV
        private void ExportCredits_Click(object sender, RoutedEventArgs e)
        {
            ExportToCsv(CreditAgreements, "кредитов");
        }

        // Экспорт заявок на открытие счета в CSV
        private void ExportAccApplications_Click(object sender, RoutedEventArgs e)
        {
            ExportToCsv(AccountApplications, "заявок на счета");
        }

        // Экспорт заявок на кредит в CSV
        private void ExportCredApplications_Click(object sender, RoutedEventArgs e)
        {
            ExportToCsv(CreditApplications, "заявок на кредиты");
        }

        // Импорт транзакций из CSV
        private async void ImportTransactions_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = GetCsvFilePath();
            if (string.IsNullOrEmpty(csvFilePath))
                return;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{App.hostString}Import/ImportTransactionsFromCsv?csvPath={csvFilePath}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                MessageBox.Show(apiResponse);
            }
        }

        // Импорт аккаунтов из CSV
        private async void ImportAccounts_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = GetCsvFilePath();
            if (string.IsNullOrEmpty(csvFilePath))
                return;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{App.hostString}Import/ImportAccountsFromCsv?csvPath={csvFilePath}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                MessageBox.Show(apiResponse);
            }
        }

        // Импорт заявок на открытие счета из CSV
        private async void ImportAccApplications_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = GetCsvFilePath();
            if (string.IsNullOrEmpty(csvFilePath))
                return;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{App.hostString}Import/ImportAccountApplicationsFromCsv?csvPath={csvFilePath}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                MessageBox.Show(apiResponse);
            }
        }

        // Импорт заявок на кредит из CSV
        private async void ImportCredApplications_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = GetCsvFilePath();
            if (string.IsNullOrEmpty(csvFilePath))
                return;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{App.hostString}Import/ImportCreditApplicationsFromCsv?csvPath={csvFilePath}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                MessageBox.Show(apiResponse);
            }
        }

        // Получение пути к выбранному CSV-файлу
        private string GetCsvFilePath()
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "CSV Files|*.csv",
                Title = "Выберите CSV-файл"
            };

            bool? result = dialog.ShowDialog();
            return result.HasValue && result.Value ? dialog.FileName : null;
        }

        // Импорт кредитов из CSV
        private async void ImportCredits_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = GetCsvFilePath();
            if (string.IsNullOrEmpty(csvFilePath))
                return;

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync($"{App.hostString}Import/ImportCreditAgreementsFromCsv?csvPath={csvFilePath}", null);
                string apiResponse = await response.Content.ReadAsStringAsync();

                MessageBox.Show(apiResponse);
            }
        }
    }
}
