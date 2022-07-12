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
using static ClothingStore.Employees;
using static ClothingStore.Packages;
using static ClothingStore.Products;

namespace ClothingStore
{
    public partial class Deliveries : Page
    {
        List<DeliveryClass> DeliveryItem;
        List<PackageClass> PackageItem;
        List<ProductClass> ProductItem;
        List<EmployeesClass> EmployeeItem;
        List<DeliveryClass> ResetItems = new List<DeliveryClass>();

        public Deliveries()
        {
            InitializeComponent();

            UpdateDeliveries();
            BoxPackage();
            BoxProduct();
            BoxEmployee();

            filterBox.Items.Add("Все");
            foreach (DeliveryClass item in DeliveryItem)
            {
                if (filterBox.Items.Contains(item.DateOfDelivery))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.DateOfDelivery);
                }
            }
        }

        public class DeliveryClass : INotifyPropertyChanged
        {
            private int cost;
            private DateTime time;
            private string address;
            private int count;

            public int? idDelivery { get; set; }
            public int CostOfDelivery {
                get => cost; set
                {
                    cost = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(cost)));
                }
            }
            public DateTime DateOfDelivery {
                get => time; set
                {
                    time = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(time)));
                }
            }
            public string addressOfDelivery {
                get => address; set
                {
                    address = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(address)));
                }
            }
            public int Count {
                get => count; set
                {
                    count = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(count)));
                }
            }
            public int PackageId { get; set; }
            public int ProductId { get; set; }
            public int EmployeeId { get; set; }

            public PackageClass package { get; set; }
            public ProductClass product { get; set; }
            public EmployeesClass employee { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для вывода данных в ComboBox
        public void BoxPackage()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages"));

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

            PackageItem = JsonConvert.DeserializeObject<List<PackageClass>>(jsonString1);

            foreach (PackageClass item in PackageItem)
            {
                packageBox.Items.Add(item.SizeOfPackage);
            }
            packageBox.SelectedIndex = 0;
        }

        //Метод для вывода данных в ComboBox
        public void BoxProduct()
        {

            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products"));

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


            ProductItem = JsonConvert.DeserializeObject<List<ProductClass>>(jsonString1);

            foreach (ProductClass item in ProductItem)
            {
                productBox.Items.Add(item.Size);
            }
            productBox.SelectedIndex = 0;

        }

        //Метод для вывода данных в ComboBox
        public void BoxEmployee()
        {

            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees"));

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

            EmployeeItem = JsonConvert.DeserializeObject<List<EmployeesClass>>(jsonString1);

            foreach (EmployeesClass item in EmployeeItem)
            {
                employeeBox.Items.Add(item.Surname);
            }
            employeeBox.SelectedIndex = 0;

        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateDeliveries()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Deliveries"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            DeliveryItem = JsonConvert.DeserializeObject<List<DeliveryClass>>(jsonString);
            deliveryGrid.ItemsSource = DeliveryItem;

            foreach (var item in DeliveryItem)
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
                int yourId = DeliveryItem[deliveryGrid.SelectedIndex].idDelivery.Value;
                ResetItems.Add(DeliveryItem[deliveryGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Deliveries/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateDeliveries();

        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtCost.Text == "" || datePick.SelectedDate == null || txtAddress.Text == "" || txtCount.Text == "" || packageBox.SelectedItem == null || productBox.SelectedItem == null || employeeBox.SelectedItem == null || txtCost.Text == "Стоимость доставки" || txtAddress.Text == "Адрес доставки" || txtCount.Text == "Количество")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Deliveries"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";

                string selectedIdPackage = packageBox.SelectedItem.ToString();
                int IdPackage = 0;
                foreach (var item in PackageItem)
                {
                    if (item.SizeOfPackage == selectedIdPackage)
                    {
                        IdPackage = item.PackageId.Value;
                    }
                }

                string selectedIdProduct = productBox.SelectedItem.ToString();
                int IdProduct = 0;
                foreach (var item in ProductItem)
                {
                    if (item.Count == Convert.ToInt32(selectedIdProduct))
                    {
                        IdProduct = item.ProductId.Value;
                    }
                }

                string selectedIdEmployee = employeeBox.SelectedItem.ToString();
                int IdEmployee = 0;
                foreach (var item in EmployeeItem)
                {
                    if (item.Surname == selectedIdEmployee)
                    {
                        IdEmployee = item.EmployeeId.Value;
                    }
                }

                DeliveryClass delivery = new DeliveryClass
                {
                    idDelivery = null,
                    CostOfDelivery = Convert.ToInt32(txtCost.Text),
                    DateOfDelivery = (DateTime)datePick.SelectedDate,
                    addressOfDelivery = txtAddress.Text,
                    Count = Convert.ToInt32(txtCount.Text),
                    PackageId = IdPackage,
                    ProductId = IdProduct,
                    EmployeeId = IdEmployee
                };
                DeliveryItem.Add(delivery);


                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(delivery);
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
                UpdateDeliveries();
            }
        }

        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {
                warningBox.Text = "";

                foreach (DeliveryClass item in DeliveryItem)
                {
                    try
                    {

                        if (item.DateOfDelivery == Convert.ToDateTime(choice))
                        {
                            deliveryGrid.DataContext = null;
                            deliveryGrid.ItemsSource = null;
                            List<DeliveryClass> resultAcc = DeliveryItem.Where(x => x.DateOfDelivery == item.DateOfDelivery).ToList();
                            deliveryGrid.ItemsSource = resultAcc;
                        }
                    }
                    catch
                    {

                    }
                }

                if (filterBox.SelectedValue.Equals("Все"))
                {
                    deliveryGrid.DataContext = null;
                    deliveryGrid.ItemsSource = null;

                    UpdateDeliveries();
                }
            }
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        private void txtCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        private void txtCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;
        }

        //Метод для редактирования данных в БД
        private void PUT(DeliveryClass delivery)
        {
            try
            {
                int id = DeliveryItem[deliveryGrid.SelectedIndex].idDelivery.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Deliveries/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(delivery);
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
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Deliveries"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    DeliveryClass delivery = new DeliveryClass
                    {
                        idDelivery = null,
                        CostOfDelivery = ResetItems[i].CostOfDelivery,
                        DateOfDelivery = ResetItems[i].DateOfDelivery,
                        addressOfDelivery = ResetItems[i].addressOfDelivery,
                        Count = ResetItems[i].Count,
                        PackageId = ResetItems[i].PackageId,
                        ProductId = ResetItems[i].ProductId,
                        EmployeeId = ResetItems[i].EmployeeId
                    };
                    DeliveryItem.Add(delivery);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(delivery);
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
            UpdateDeliveries();
        }
    }
}
