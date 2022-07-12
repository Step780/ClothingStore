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
using static ClothingStore.AdverstiningCalcs;
using static ClothingStore.SalaryCalculations;
using static ClothingStore.Stocks;

namespace ClothingStore
{
    public partial class AccountingAdverstining : Page
    {
        List<Accounting> AdverstItems;
        List<StockClass> StockItems;
        List<AdverstiningCalc> CalculationItems;
        List<Accounting> ResetItems = new List<Accounting>();

        public AccountingAdverstining()
        {
            InitializeComponent();

            UpdateAccounting();

            BoxStock();

            BoxCalculation();

            filterBox.Items.Add("Все");
            foreach (Accounting item in AdverstItems)
            {

                if (filterBox.Items.Contains(item.DateofAdverstining))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.DateofAdverstining);
                }
            }
        }

        //Класс с данными
        public class Accounting : INotifyPropertyChanged
        {
            private DateTime date;

            public int? IDAccounting { get; set; }
            public DateTime DateofAdverstining {
                get => date; set
                {
                    date = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(date)));
                }
            }
            public int StockId { get; set; }
            public int CalculationId { get; set; }

            public StockClass Stock { get; set; }
           
            public AdverstiningCalc Calculation { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для заполнения ComboBox со скидками
        public void BoxStock()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Stocks"));

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

            StockItems = JsonConvert.DeserializeObject<List<StockClass>>(jsonString1);

            foreach (StockClass item in StockItems)
            {

                boxStock.Items.Add(item.discountValue);


            }
            boxStock.SelectedIndex = 0;

        }

        //Метод для заполнения ComboBox со стоимостью
        public void BoxCalculation()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Calculation"));

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

            CalculationItems = JsonConvert.DeserializeObject<List<AdverstiningCalc>>(jsonString1);

            foreach (AdverstiningCalc item in CalculationItems)
            {
                boxCalculation.Items.Add(item.Cost);
            }
            boxCalculation.SelectedIndex = 0;
        }


        //Метод для обновления и вывода данных в DataGrid
        public void UpdateAccounting()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/AccountingAdverstinings"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }
            AdverstItems = JsonConvert.DeserializeObject<List<Accounting>>(jsonString);

            accountingGrid.ItemsSource = AdverstItems;
            foreach (var item in AdverstItems)
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
                int yourId = AdverstItems[accountingGrid.SelectedIndex].IDAccounting.Value;

                ResetItems.Add(AdverstItems[accountingGrid.SelectedIndex]);
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/AccountingAdverstinings/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateAccounting();
        }

        //Метод для добавления новых данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (datePick.SelectedDate == null || boxStock.SelectedItem == null || boxCalculation.SelectedItem == null)
            {
                warningBox.Text = "Вы не ввели дату или не выбрали пункты из выпадающих списков";
            }

            else
            {
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/AccountingAdverstinings"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";

                string selectedIdStock = boxStock.SelectedItem.ToString();
                int IdStock = 0;
                foreach (var item in StockItems)
                {
                    if (item.discountValue == Convert.ToInt32(selectedIdStock))
                    {
                        IdStock = item.StockId.Value;
                    }
                }

                string selectedIdSalary = boxCalculation.SelectedItem.ToString();
                int IdSalary = 0;
                foreach (var item in CalculationItems)
                {
                    if (item.Cost == Convert.ToInt32(selectedIdSalary))
                    {
                        IdSalary = item.CalculationId.Value;
                    }
                }

                Accounting accounting = new Accounting
                {
                    IDAccounting = null,
                    DateofAdverstining = (DateTime)datePick.SelectedDate,
                    CalculationId = IdSalary,
                    StockId = IdStock
                };
                AdverstItems.Add(accounting);


                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(accounting);
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
                UpdateAccounting();
            }
        }

        //Метод для фильтрации. Получает данные из ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {
                foreach (Accounting item in AdverstItems)
                {
                    try
                    {
                        if (item.DateofAdverstining == Convert.ToDateTime(choice))
                        {
                            accountingGrid.DataContext = null;
                            accountingGrid.ItemsSource = null;
                            List<Accounting> resultAcc = AdverstItems.Where(x => x.DateofAdverstining == item.DateofAdverstining).ToList();
                            accountingGrid.ItemsSource = resultAcc;
                        }


                    }
                    catch
                    {
                    }
                }

                if (filterBox.SelectedValue.Equals("Все"))
                {
                    accountingGrid.DataContext = null;
                    accountingGrid.ItemsSource = null;

                    UpdateAccounting();
                }
            }
            
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }

        }

        //Метод для редактирования БД. Данные редактируются через ячейки в DataGrid
        private void PUT(Accounting accounting)
        {
            try
            {
                int id = AdverstItems[accountingGrid.SelectedIndex].IDAccounting.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/AccountingAdverstinings/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(accounting);
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/AccountingAdverstinings"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    Accounting accounting = new Accounting
                    {
                        IDAccounting = null,
                        DateofAdverstining = ResetItems[i].DateofAdverstining,
                        CalculationId = ResetItems[i].CalculationId,
                        StockId = ResetItems[i].StockId
                    };
                    AdverstItems.Add(accounting);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(accounting);
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

            UpdateAccounting();
        }
    }
    
}
