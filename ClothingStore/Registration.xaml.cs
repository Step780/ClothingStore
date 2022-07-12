using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
using static ClothingStore.Acc;

namespace ClothingStore
{
    
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();

            roleBox.Items.Add("Администратор");
            roleBox.Items.Add("Бухгалтер");
            roleBox.Items.Add("Продавец");
            roleBox.Items.Add("Кладовщик");
            roleBox.Items.Add("СММ");
            roleBox.Items.Add("Отдел кадров");
        }

        //Регистрация по нажатию кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "" && passwordBox.Text == "" && roleBox.SelectedItem == null)
            {
                warningBox.Text = "Вы не ввели логин или парль, либо не выбрали роль!";
            }

            //POST запрос для записи нового аккаунта
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

            MainWindow.MainFrameInstance.Navigate(new LoginPage());
        }
    }
}
