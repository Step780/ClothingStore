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
    public partial class AdverstiningCalcs : Page
    {
        List<AdverstiningCalc> adverstiningItems;
        List<AdverstiningCalc> ResetItems = new List<AdverstiningCalc>();

        public AdverstiningCalcs()
        {
            InitializeComponent();

            UpdateAdverstining();
        }

        //Класс с данными
        public class AdverstiningCalc : INotifyPropertyChanged
        {
            private int cost;

            public int? CalculationId { get; set; }
            public int Cost {
                get => cost; set
                {
                    cost = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(cost)));
                }
            }

            public override string ToString()
            {
                return Cost.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateAdverstining()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            adverstiningItems = JsonConvert.DeserializeObject<List<AdverstiningCalc>>(jsonString);
            calcGrid.ItemsSource = adverstiningItems;

            foreach (var item in adverstiningItems)
            {
                item.PropertyChanged += delegate
                {
                    PUT(item);
                };
            }
        }

        //Метод для удаления данных из БД и DataGrid
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
                int yourId = adverstiningItems[calcGrid.SelectedIndex].CalculationId.Value;

                ResetItems.Add(adverstiningItems[calcGrid.SelectedIndex]);


                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateAdverstining();
        }

        //Метод для добавления данных в БД
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (adverstiningBox.Text == "")
            {
                warningBox.Text = "Вы не ввели стоимость рекламы!";
            }
            if (adverstiningBox.Text == "Стоимость рекламы")
            {
                warningBox.Text = "Вы не ввели стоимость рекламы!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";


                AdverstiningCalc adverstining = new AdverstiningCalc
                {
                    CalculationId = null,
                    Cost = Convert.ToInt32(adverstiningBox.Text)

                };
                adverstiningItems.Add(adverstining);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(adverstining);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();


                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdateAdverstining();
            }
        }

        //Метод для поиска данных в БД
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string choice = search.Text;

            if (choice == "")
            {
                warningBox.Text = "Вы не ввели данные в поиск!";
            }
            if (choice == "Найти по стоимости")
            {
                warningBox.Text = "Вы не ввели данные в поиск!";
            }
            else
            {
                warningBox.Text = "";
                foreach (AdverstiningCalc item in adverstiningItems)
                {
                    if (item.Cost == Convert.ToInt32(choice))
                    {
                        calcGrid.DataContext = null;
                        calcGrid.ItemsSource = null;
                        List<AdverstiningCalc> resultAcc = adverstiningItems.Where(x => x.Cost == item.Cost).ToList();
                        calcGrid.ItemsSource = resultAcc;
                    }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            UpdateAdverstining();
        }

        //Метод для редактирования данных в БД
        private void PUT(AdverstiningCalc adverstining)
        {
            try
            {
                int id = adverstiningItems[calcGrid.SelectedIndex].CalculationId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(adverstining);
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

        //Метод для запрета ввода цифр
        private void adverstiningBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        //Метод для запрета ввода цифр
        private void search_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    AdverstiningCalc adverstining = new AdverstiningCalc
                    {
                        CalculationId = null,
                        Cost = ResetItems[i].Cost

                    };
                    adverstiningItems.Add(adverstining);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(adverstining);
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

            UpdateAdverstining();
        }
    }
}
