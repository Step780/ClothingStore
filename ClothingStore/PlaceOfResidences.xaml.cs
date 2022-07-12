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

namespace ClothingStore
{
    public partial class PlaceOfResidences : Page
    {
        List<PlacesClass> PlacesItem;
        List<PlacesClass> ResetItems = new List<PlacesClass>();

        public PlaceOfResidences()
        {
            InitializeComponent();

            UpdatePlace();

            filterBox.Items.Add("Все");
            foreach (PlacesClass item in PlacesItem)
            {
                if (filterBox.Items.Contains(item.Country))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Country);
                }
            }
        }

        //Класс с данными
        public class PlacesClass : INotifyPropertyChanged
        {
            private string country;
            private string region;
            private string city;
            private string street;
            private int indexprv;

            public int? PlaceOfResidenceId { get; set; }
            public string Country {
                get => country; set
                {
                    country = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(country)));
                }
            }
            public string Region {
                get => region; set
                {
                    region = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(region)));
                }
            }
            public string City {
                get => city; set
                {
                    city = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(city)));
                }
            }
            public string Street {
                get => street; set
                {
                    street = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(street)));
                }
            }
            public int index {
                get => indexprv; set
                {
                    indexprv = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(indexprv)));
                }
            }

            public override string ToString()
            {
                return Street;
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdatePlace()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            PlacesItem = JsonConvert.DeserializeObject<List<PlacesClass>>(jsonString);

            placesGrid.ItemsSource = PlacesItem;

            foreach (var item in PlacesItem)
            {
                item.PropertyChanged += delegate
                {
                    PUT(item);
                };
            }

        }

        //Метод для удаления данных из БД
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
                int yourId = PlacesItem[placesGrid.SelectedIndex].PlaceOfResidenceId.Value;
                ResetItems.Add(PlacesItem[placesGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdatePlace();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (countryBox.Text == "" || regionBox.Text == "" || cityBox.Text == "" || streetBox.Text == "" || indexBox.Text == "" || countryBox.Text == "Страна" || regionBox.Text == "Область" || cityBox.Text == "Город" || streetBox.Text == "Улица" || indexBox.Text == "Индекс")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";


                PlacesClass places = new PlacesClass
                {
                    PlaceOfResidenceId = null,
                    Country = countryBox.Text,
                    Region = regionBox.Text,
                    City = cityBox.Text,
                    Street = streetBox.Text,
                    index = Convert.ToInt32(indexBox.Text),
                };
                PlacesItem.Add(places);


                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(places);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdatePlace();
            }
        }

        //Метод для редактирования данных в БД
        private void PUT(PlacesClass places)
        {
            try
            {
                warningBox.Text = "";
                int id = PlacesItem[placesGrid.SelectedIndex].PlaceOfResidenceId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(places);
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

        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            foreach (PlacesClass item in PlacesItem)
            {
                if (item.Country == choice)
                {
                    placesGrid.DataContext = null;
                    placesGrid.ItemsSource = null;
                    List<PlacesClass> resultAcc = PlacesItem.Where(x => x.Country == item.Country).ToList();
                    placesGrid.ItemsSource = resultAcc;
                }
            }

            if (filterBox.SelectedValue.Equals("Все"))
            {
                placesGrid.DataContext = null;
                placesGrid.ItemsSource = null;

                UpdatePlace();
            }

            else
            {
                warningBox.Text = "Что-то пошло не так";

            }
        }

        private void indexBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/PlaceOfResidences"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    PlacesClass places = new PlacesClass
                    {
                        PlaceOfResidenceId = null,
                        Country = ResetItems[i].Country,
                        Region = ResetItems[i].Region,
                        City = ResetItems[i].City,
                        Street = ResetItems[i].Street,
                        index = ResetItems[i].index,

                    };
                    PlacesItem.Add(places);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(places);
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
            UpdatePlace();
        }
    }
}
