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

    public partial class LoginPage : Page
    {
        List<Account> AccItems;

        public LoginPage()
        {
            InitializeComponent();

            ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnAcc.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnAccounting.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnAdverstining.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnBrand.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnCollection.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnDelivery.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnEmployee.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnPackage.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnPlaces.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnPosition.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnProduct.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnProductType.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnSalary.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnSpecification.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnStock.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnUserData.Visibility = Visibility.Hidden;
            ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Hidden;

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
        }

        //Вход в главное меню
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (loginBox.Text == "" || passwordBox.Text == "")
            {
                warningBox.Text = "Вы не ввели логин или пароль!";
                warningBox.HorizontalAlignment = HorizontalAlignment.Center;
                warningBox.VerticalAlignment = VerticalAlignment.Center;
            }

            else
            {
                try
                {
                    foreach (Account item in AccItems)
                    {
                        if (item.Login == loginBox.Text && item.Password == passwordBox.Text)
                        {
                            MainWindow.role = item.Role;
                            MainWindow.MainFrameInstance.Navigate(new AllTables(item.Role));
                        }
                        else
                        {
                            warningBox.Text = "Вы ввели неправильный логин или пароль, либо пользователь не существует";
                        }
                    }
                }
                catch
                {
                    warningBox.Text = "Что-то пошло не так";
                }
            }
        }

        //Переход к окну регистрации
        private void registrationBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Registration());
        }
    }
}
