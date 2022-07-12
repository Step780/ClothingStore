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
    /// <summary>
    /// Логика взаимодействия для Stocks.xaml
    /// </summary>
    public partial class Stocks : Page
    {
        List<StockClass> StockItem;
        List<StockClass> ResetItems = new List<StockClass>();

        public Stocks()
        {
            InitializeComponent();
            UpdateStock();
        }

        //Класс с данными
        public class StockClass : INotifyPropertyChanged
        {
            private int discount;

            public int? StockId { get; set; }
            public int discountValue {
                get => discount; set
                {
                    discount = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(discount)));
                }
            }

            public override string ToString()
            {
                return discountValue.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateStock()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            StockItem = JsonConvert.DeserializeObject<List<StockClass>>(jsonString);
            stockGrid.ItemsSource = StockItem;

            foreach (var item in StockItem)
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
                int yourId = StockItem[stockGrid.SelectedIndex].StockId.Value;
                ResetItems.Add(StockItem[stockGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateStock();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (discountBox.Text == "" || discountBox.Text == "Скидка в процентах")
            {
                warningBox.Text = "Вы не ввели значение скидки!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";

                StockClass stock = new StockClass
                {
                    StockId = null,
                    discountValue = Convert.ToInt32(discountBox.Text)
                };
                StockItem.Add(stock);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(stock);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdateStock();
            }
        }

        //Метод для редактирования данных в БД
        private void PUT(StockClass stock)
        {
            try
            {
                int id = StockItem[stockGrid.SelectedIndex].StockId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(stock);
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

        //Метод для поиска данных
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string choice = searchDisc.Text;

            try
            {
                warningBox.Text = "";
                foreach (StockClass item in StockItem)
                {
                    if (item.discountValue == Convert.ToInt32(choice))
                    {
                        stockGrid.DataContext = null;
                        stockGrid.ItemsSource = null;
                        List<StockClass> resultAcc = StockItem.Where(x => x.discountValue == item.discountValue).ToList();
                        stockGrid.ItemsSource = resultAcc;
                    }
                }
            }
            catch
            {
                warningBox.Text = "Введите число!";
            }
            
        }

        private void allBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateStock();
        }

        private void discountBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;

        }

        private void searchDisc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    StockClass stock = new StockClass
                    {
                        StockId = null,
                        discountValue = ResetItems[i].discountValue
                    };
                    StockItem.Add(stock);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(stock);
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

            UpdateStock();
        }
    }
}
