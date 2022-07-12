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
using static ClothingStore.ProductTypes;

namespace ClothingStore
{
    
    public partial class Products : Page
    {
        List<ProductClass> ProductsItem;
        List<TypesClass> TypesItem;
        List<ProductClass> ResetItems = new List<ProductClass>();

        public Products()
        {
            InitializeComponent();

            UpdateProduct();

            BoxUpdate();

            filterBox.Items.Add("Все");
            foreach (ProductClass item in ProductsItem)
            {
                if (filterBox.Items.Contains(item.Size))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Size);
                }
            }
        }

        //Класс с данными
        public class ProductClass : INotifyPropertyChanged
        {
            private int count;
            private string size;

            public event PropertyChangedEventHandler PropertyChanged;

            public int? ProductId { get; set; }
            public int Count {
                get => count; set
                {
                    count = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(count)));
                }
            }
            public string Size {
                get => size; set
                {
                    size = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(size)));
                }
            }
            public int ProductTypeId { get; set; }

            public override string ToString()
            {
                return Size.ToString();
            }

            public TypesClass productType { get; set; }

        }

        //Метод для заполнения ComboBox
        public void BoxUpdate()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes"));

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

            TypesItem = JsonConvert.DeserializeObject<List<TypesClass>>(jsonString1);

            foreach (TypesClass item in TypesItem)
            {
                idBox.Items.Add(item.Name);
            }
            idBox.SelectedIndex = 0;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateProduct()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            ProductsItem = JsonConvert.DeserializeObject<List<ProductClass>>(jsonString);
            productsGrid.ItemsSource = ProductsItem;

            foreach (var item in ProductsItem)
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
                int yourId = ProductsItem[productsGrid.SelectedIndex].ProductId.Value;
                ResetItems.Add(ProductsItem[productsGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateProduct();
        }

        //Метод для добавления данных в БД
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtCount.Text == "" || txtSize.Text == "" || idBox.SelectedItem == null || txtCount.Text == "Количество" || txtSize.Text == "Размеры")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }
          
            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";

                string selectedIdType = idBox.SelectedItem.ToString();
                int IdType = 0;
                foreach (var item in TypesItem)
                {
                    if (item.Name == selectedIdType)
                    {
                        IdType = item.ProductTypeid.Value;
                    }
                }

                ProductClass product = new ProductClass
                {
                    ProductId = null,
                    Count = Convert.ToInt32(txtCount.Text),
                    Size = txtSize.Text,
                    ProductTypeId = IdType
                };
                ProductsItem.Add(product);

                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(product);
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
                UpdateProduct();
            }
        }


        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {
                warningBox.Text = "";
            foreach (ProductClass item in ProductsItem)
            {
                if (item.Size == choice)
                {
                    productsGrid.DataContext = null;
                    productsGrid.ItemsSource = null;
                    List<ProductClass> resultAcc = ProductsItem.Where(x => x.Size == item.Size).ToList();
                    productsGrid.ItemsSource = resultAcc;
                }
            }

            if (filterBox.SelectedValue.Equals("Все"))
            {
                productsGrid.DataContext = null;
                productsGrid.ItemsSource = null;

                UpdateProduct();
            }
            }
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        //Метод для редактирования данных в БД
        private void PUT(ProductClass product)
        {
            try
            {
                int id = ProductsItem[productsGrid.SelectedIndex].ProductId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(product);
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

        private void txtCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Products"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    ProductClass product = new ProductClass
                    {
                        ProductId = null,
                        Count = ResetItems[i].Count,
                        Size = ResetItems[i].Size,
                        ProductTypeId = ResetItems[i].ProductTypeId
                    };
                    ProductsItem.Add(product);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(product);
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
            UpdateProduct();
        }
    }
}
