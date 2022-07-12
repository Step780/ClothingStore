using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Series;
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
    public partial class Brands : Page
    {
        List<BrandClass> BrandsItem;
        List<BrandClass> ResetItems = new List<BrandClass>();

        public Brands()
        {
            InitializeComponent();

            UpdateBrands();

            comboFilter.Items.Add("Все");
            foreach (BrandClass item in BrandsItem)
            {
                if (comboFilter.Items.Contains(item.Country))
                {
                    continue;
                }
                else
                {
                    comboFilter.Items.Add(item.Country);
                }
            }

        }

        //Класс с данными
        public class BrandClass : INotifyPropertyChanged
        {
            private string name;
            private string country;

            public int? BrandId { get; set; }
            public string Name {
                get => name; set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
            public string Country {
                get => country; set
                {
                    country = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(country)));
                }
            }

            public override string ToString()
            {
                return Name.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для вывода и обновления данных в DataGrid
        public void UpdateBrands()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            BrandsItem = JsonConvert.DeserializeObject<List<BrandClass>>(jsonString);
            brandsGrid.ItemsSource = BrandsItem;

            foreach (var item in BrandsItem)
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
                int yourId = BrandsItem[brandsGrid.SelectedIndex].BrandId.Value;
                ResetItems.Add(BrandsItem[brandsGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateBrands();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "" || countryBox.Text == "" || nameBox.Text == "Название" || countryBox.Text == "Страна")
            {
                warningBox.Text = "Вы не ввели название или страну!";
            }

            else {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands/"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";


                BrandClass brand = new BrandClass
                {
                    BrandId = null,
                    Name = nameBox.Text,
                    Country = countryBox.Text
                };
                BrandsItem.Add(brand);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(brand);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdateBrands();
            }
        }

        //Метод для редактирования данных
        private void PUT(BrandClass brandClass)
        {
            try
            {
                warningBox.Text = "";
                int id = BrandsItem[brandsGrid.SelectedIndex].BrandId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(brandClass);
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
        private void comboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                warningBox.Text = "";
                string choice = comboFilter.SelectedItem.ToString();

            foreach (BrandClass item in BrandsItem)
            {
                if (item.Country == choice)
                {
                    brandsGrid.DataContext = null;
                    brandsGrid.ItemsSource = null;
                    List<BrandClass> resultAcc = BrandsItem.Where(x => x.Country == item.Country).ToList();
                    brandsGrid.ItemsSource = resultAcc;
                }
            }

            if (comboFilter.SelectedValue.Equals("Все"))
            {
                brandsGrid.DataContext = null;
                brandsGrid.ItemsSource = null;

                UpdateBrands();
            }
            }

            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    BrandClass brand = new BrandClass
                    {
                        BrandId = null,
                        Name = ResetItems[i].Name,
                        Country = ResetItems[i].Country
                    };
                    BrandsItem.Add(brand);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(brand);
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
            UpdateBrands();
        }
    }
}
