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
using static ClothingStore.Brands;
using static ClothingStore.Collections;

namespace ClothingStore
{
    public partial class ProductTypes : Page
    {
        List<TypesClass> TypesItem;
        List<BrandClass> BrandItem;
        List<CollectionClass> CollectionItem;
        List<TypesClass> ResetItems = new List<TypesClass>();

        public ProductTypes()
        {
            InitializeComponent();

            UpdateType();

            BoxBrandUpdate();

            BoxCollectionUpdate();

            filterBox.Items.Add("Все");
            foreach (TypesClass item in TypesItem)
            {
                if (filterBox.Items.Contains(item.Model))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Model);
                }
            }
        }

        //Класс с данными
        public class TypesClass : INotifyPropertyChanged
        {
            private int cost;
            private string color;
            private string model;
            private string name;

            public int? ProductTypeid { get; set; }
            public int Cost {
                get => cost; set
                {
                    cost = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(cost)));
                }
            }
            public string Color {
                get => color; set
                {
                    color = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(color)));
                }
            }
            public string Model {
                get => model; set
                {
                    model = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(model)));
                }
            }
            public string Name {
                get => name; set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
            public int BrandId { get; set; }
            
            public int CollectionId { get; set; }

            public BrandClass brand { get; set; }
            public CollectionClass collection { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString()
            {
                return Name.ToString();
            }

        }

        //Метод для заполнения ComboBox
        public void BoxBrandUpdate()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Brands"));

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
            BrandItem = JsonConvert.DeserializeObject<List<BrandClass>>(jsonString1);

            foreach (BrandClass item in BrandItem)
            {
                boxBrand.Items.Add(item.Name);
            }
            boxBrand.SelectedIndex = 0;


        }

        //Метод для заполнения ComboBox
        public void BoxCollectionUpdate()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections"));

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
            CollectionItem = JsonConvert.DeserializeObject<List<CollectionClass>>(jsonString1);

            foreach (CollectionClass item in CollectionItem)
            {
                boxCollection.Items.Add(item.Name);
            }
            boxCollection.SelectedIndex = 0;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateType()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            TypesItem = JsonConvert.DeserializeObject<List<TypesClass>>(jsonString);
            typesGrid.ItemsSource = TypesItem;

            foreach (var item in TypesItem)
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
                int yourId = TypesItem[typesGrid.SelectedIndex].ProductTypeid.Value;
                ResetItems.Add(TypesItem[typesGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateType();
        }

        //Метод для добавления данных в БД
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtCost.Text == "" || txtColor.Text == "" || txtModel.Text == "" || txtName.Text == "" || boxBrand.SelectedItem == null || boxCollection.SelectedItem == null || txtCost.Text == "Стоимость" || txtColor.Text == "Цвета" || txtModel.Text == "Модель" || txtName.Text == "Название")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";

                string selectedIdBrand = boxBrand.SelectedItem.ToString();
                int IdBrand = 0;
                foreach (var item in BrandItem)
                {
                    if (item.Name == selectedIdBrand)
                    {
                        IdBrand = item.BrandId.Value;
                    }
                }

                string selectedIdCollection = boxCollection.SelectedItem.ToString();
                int IdCollection = 0;
                foreach (var item in CollectionItem)
                {
                    if (item.Name == selectedIdCollection)
                    {
                        IdCollection = item.CollectionId.Value;
                    }
                }

                TypesClass types = new TypesClass
                {
                    ProductTypeid = null,
                    Cost = Convert.ToInt32(txtCost.Text),
                    Color = txtColor.Text,
                    Model = txtModel.Text,
                    Name = txtName.Text,
                    BrandId = IdBrand,
                    CollectionId = IdCollection
                };
                TypesItem.Add(types);

                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(types);
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
                UpdateType();
            }
        }

        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            foreach (TypesClass item in TypesItem)
            {
                try
                {
                    if (item.Model == choice)
                    {
                        typesGrid.DataContext = null;
                        typesGrid.ItemsSource = null;
                        List<TypesClass> resultAcc = TypesItem.Where(x => x.Model == item.Model).ToList();
                        typesGrid.ItemsSource = resultAcc;
                    }
                }
                catch
                {

                }
            }

            if (filterBox.SelectedValue.Equals("Все"))
            {
                typesGrid.DataContext = null;
                typesGrid.ItemsSource = null;

                UpdateType();
            }
        }

        private void PUT(TypesClass types)
        {
            try
            {
                int id = TypesItem[typesGrid.SelectedIndex].ProductTypeid.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(types);
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

        private void txtCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/ProductTypes"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    TypesClass types = new TypesClass
                    {
                        ProductTypeid = null,
                        Cost = ResetItems[i].Cost,
                        Color = ResetItems[i].Color,
                        Model = ResetItems[i].Model,
                        Name = ResetItems[i].Name,
                        BrandId = ResetItems[i].BrandId,
                        CollectionId = ResetItems[i].CollectionId
                    };
                    TypesItem.Add(types);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(types);
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

            UpdateType();
        }
    }
}
