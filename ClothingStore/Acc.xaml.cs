using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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

namespace ClothingStore
{

    public partial class Acc : Page
    {
        List<Account> AccItems;
        List<Account> ResetItems = new List<Account>();
        public Acc()
        {
            InitializeComponent();
            UpdateAcc();

            //Заполнение ComboBox для фильтрации
            comboFilter.Items.Add("Все");
            foreach (Account item in AccItems)
            {
                if (comboFilter.Items.Contains(item.Password))
                {
                    continue;
                }
                else
                {
                    comboFilter.Items.Add(item.Password);
                }

            }
            comboFilter.SelectedIndex = 0;

            //Заполнения ComboBox с ролями
            roleBox.Items.Add("Администратор");
            roleBox.Items.Add("Бухгалтер");
            roleBox.Items.Add("Продавец");
            roleBox.Items.Add("Кладовщик");
            roleBox.Items.Add("СММ");
            roleBox.Items.Add("Отдел кадров");
            roleBox.SelectedIndex = 0;
        }


        //Класс с данными
        public class Account : INotifyPropertyChanged
        {
            private string login;
            private string password;
            private string role;

            public int? idAccount { get; set; }
            public string Login
            {
                get => login; set
                {
                    login = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(login)));
                }

            }
            public string Password
            {
                get => password; set
                {
                    password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(password)));
                }
            }

            public string Role
            {
                get => role; set
                {
                    role = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(role)));
                }
            }

            public override string ToString()
            {
                return Login.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }


        //Метод с GET запросом для обновления и вывода данных в DataGrid
        public void UpdateAcc()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            AccItems = JsonConvert.DeserializeObject<List<Account>>(jsonString);
            dataGrid.ItemsSource = AccItems;
            foreach (var item in AccItems)
            {
                item.PropertyChanged += delegate
                {
                    PUT(item);
                };
            }

            WebResp.Close();
        }

        //Метод для удаления данных из DataGrid
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int yourId = AccItems[dataGrid.SelectedIndex].idAccount.Value;

            ResetItems.Add(AccItems[dataGrid.SelectedIndex]);



            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts/" + yourId));

            WebReq.Method = "DELETE";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            UpdateAcc();





        }

        //Метод для добавления новых данных в БД
        private void addAcc_Click(object sender, RoutedEventArgs e)
        {
            //Проверка на наличие данных в полях
            if (loginBox.Text == "" || passwordBox.Text == "" || roleBox.SelectedItem == null)
            {
                warningBox.Text = "Вы не ввели логин или пароль, либо не выбрали роль!";
            }

            else
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";

                Account account = new Account
                {
                    idAccount = null,
                    Login = loginBox.Text,
                    Password = passwordBox.Text,
                    Role = roleBox.SelectedItem.ToString()
                };
                AccItems.Add(account);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(account);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                
                UpdateAcc();
            }
        }

        //Метод для редактирования ячееек данных БД
        private void PUT(Account account)
        {
            try
            {
                int id = AccItems[dataGrid.SelectedIndex].idAccount.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(account);
                    streamWriter.Write(json);
                }

                var httpResponse = (HttpWebResponse)webRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }
            }

            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        //Метод для фильтрации
        private void comboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = comboFilter.SelectedItem.ToString();

            foreach (Account item in AccItems)
            {
                if (item.Password == choice)
                {
                    dataGrid.DataContext = null;
                    dataGrid.ItemsSource = null;
                    List<Account> resultAcc = AccItems.Where(x => x.Password == item.Password).ToList();
                    dataGrid.ItemsSource = resultAcc;
                }
            }

            if (comboFilter.SelectedValue.Equals("Все"))
            {
                dataGrid.DataContext = null;
                dataGrid.ItemsSource = null;

                UpdateAcc();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";

                for (int i = 0; i <= ResetItems.Count; i++)
                {
                    try
                    {
                        Account account = new Account
                        {
                            idAccount = null,
                            Login = ResetItems[i].Login,
                            Password = ResetItems[i].Password,
                            Role = ResetItems[i].Role
                        };
                        AccItems.Add(account);

                        using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                        {
                            string json = JsonConvert.SerializeObject(account);
                            streamWriter.Write(json);
                            streamWriter.Flush();
                            streamWriter.Close();
                        };

                        var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                        }
                    }
                    catch
                    {
                        
                    }
                }
            }
            catch
            {
                warningBox.Text = "Список для удаления пуст!";
            }
            UpdateAcc();

            ResetItems.Clear();
        }
    }
}
