using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static ClothingStore.PlaceOfResidences;

namespace ClothingStore
{
    public partial class UserData : Page
    {
        List<PlacesClass> PlacesItem;
        List<UserDataClass> UserItems;
        List<UserDataClass> ResetItems = new List<UserDataClass>();

        public UserData()
        {
            InitializeComponent();

            UpdateUserData();
            BoxPlaces();
        }

        //Класс с данными
        public class UserDataClass : INotifyPropertyChanged
        {
            private int series;
            private int number;
            private string mail;
            private string phone;

            public event PropertyChangedEventHandler PropertyChanged;

            public int? userDatumId { get; set; }
            public int passportSeries {
                get => series; set
                {
                    series = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(series)));
                }
            }
            public int passportNumber {
                get => number; set
                {
                    number = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(number)));
                }
            }
            public string Mail {
                get => mail; set
                {
                    mail = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(mail)));
                }
            }
            public string phoneNumber {
                get => phone; set
                {
                    phone = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(phone)));
                }
            }
            public int PlaceOfResidenceId { get; set; }

            public override string ToString()
            {
                return Mail.ToString();
            }

            public PlacesClass placeOfResidence { get; set; }
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateUserData()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            UserItems = JsonConvert.DeserializeObject<List<UserDataClass>>(jsonString);
            userdataGrid.ItemsSource = UserItems;

            foreach (var item in UserItems)
            {
                item.PropertyChanged += delegate
                {
                    PUT(item);
                };
            }
        }

        //Метод для заполнения ComboBox
        public void BoxPlaces()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences"));

            WebReq1.Method = "GET";

            HttpWebResponse WebResp1 = (HttpWebResponse)WebReq1.GetResponse();

            string jsonString1;
            using (Stream stream1 = WebResp1.GetResponseStream())
            {
                StreamReader reader1 = new StreamReader(stream1, Encoding.UTF8);
                jsonString1 = reader1.ReadToEnd();
                reader1.Close();
            }

            WebResp1.Close();

            PlacesItem = JsonConvert.DeserializeObject<List<PlacesClass>>(jsonString1);

            foreach (PlacesClass item in PlacesItem)
            {

                comboPlaces.Items.Add(item.index);


            }
            comboPlaces.SelectedIndex = 0;
        }

        //Метод для удаления данных из БД
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
                int yourId = UserItems[userdataGrid.SelectedIndex].userDatumId.Value;
                ResetItems.Add(UserItems[userdataGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateUserData();
        }

        //Метод для добавления данных в БД
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (seriesBox.Text == "" || numberBox.Text == "" || mailBox.Text == "" || phoneBox.Text == "" || seriesBox.Text == "Серия паспорта" || numberBox.Text == "Номер паспорта" || mailBox.Text == "Почта" || phoneBox.Text == "Номер телефона")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";

            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";


                string selectedIdPlaces = comboPlaces.SelectedItem.ToString();
                int IdPlaces = 0;
                foreach (var item in PlacesItem)
                {
                    if (item.index == Convert.ToInt32(selectedIdPlaces))
                    {
                        IdPlaces = item.PlaceOfResidenceId.Value;
                    }
                }

                UserDataClass userData = new UserDataClass
                {
                    userDatumId = null,
                    passportSeries = Convert.ToInt32(seriesBox.Text),
                    passportNumber = Convert.ToInt32(numberBox.Text),
                    Mail = mailBox.Text,
                    phoneNumber = phoneBox.Text,
                    PlaceOfResidenceId = IdPlaces
                };
                UserItems.Add(userData);

                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(userData);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                try
                {
                    var httpResponse4 = (HttpWebResponse)WebReq5.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse4.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }
                }
                catch (WebException webex)
                {
                    WebResponse errResp = webex.Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        string text = reader.ReadToEnd();
                        Console.WriteLine(text);
                    }

                }
                UpdateUserData();
            }


        }


        //Метод для редактирования данных в БД
        private void PUT(UserDataClass user)
        {
            try
            {
                int id = UserItems[userdataGrid.SelectedIndex].userDatumId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(user);
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

        private void seriesBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;

        }

        private void numberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    UserDataClass userData = new UserDataClass
                    {
                        userDatumId = null,
                        passportSeries = ResetItems[i].passportSeries,
                        passportNumber = ResetItems[i].passportNumber,
                        Mail = ResetItems[i].Mail,
                        phoneNumber = ResetItems[i].phoneNumber,
                        PlaceOfResidenceId = ResetItems[i].PlaceOfResidenceId
                    };
                    UserItems.Add(userData);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(userData);
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

            UpdateUserData();
        }
    }
}
