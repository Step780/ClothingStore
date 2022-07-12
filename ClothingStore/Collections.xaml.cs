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
using static ClothingStore.Specifications;

namespace ClothingStore
{
    public partial class Collections : Page
    {
        List<CollectionClass> CollectionItem;
        List<SpecificationClass> SpecificationItem { get; set; }
        List<CollectionClass> ResetItems = new List<CollectionClass>();

        public Collections()
        {
            InitializeComponent();

            UpdateCollection();

            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications"));

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

            SpecificationItem = JsonConvert.DeserializeObject<List<SpecificationClass>>(jsonString1);

            foreach (SpecificationClass item in SpecificationItem)
            {
                idBox.Items.Add(item.Size);
            }
            idBox.SelectedIndex = 0;


            filterBox.Items.Add("Все");
            foreach (CollectionClass item in CollectionItem)
            {
                if (filterBox.Items.Contains(item.TypeOfProduct))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.TypeOfProduct);
                }
            }

        }

        //Класс с данными
        public class CollectionClass : INotifyPropertyChanged
        {
            private string name;
            private int count;
            private string typeofproduct;

            public int? CollectionId { get; set; }
            public string Name {
                get => name; set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
            public int Count {
                get => count; set
                {
                    count = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(count)));
                }
            }
            public string TypeOfProduct {
                get => typeofproduct; set
                {
                    typeofproduct = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(typeofproduct)));
                }
            }
            public int specificationId { get; set; }

            public SpecificationClass Specification { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString()
            {
                return Name.ToString();
            }

        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateCollection()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp2 = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp2.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            CollectionItem = JsonConvert.DeserializeObject<List<CollectionClass>>(jsonString);
            collectionGrid.ItemsSource = CollectionItem;

            foreach (var item in CollectionItem)
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
                int yourId = CollectionItem[collectionGrid.SelectedIndex].CollectionId.Value;
                ResetItems.Add(CollectionItem[collectionGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp1 = (HttpWebResponse)WebReq.GetResponse();

                UpdateCollection();
        }

        //Метод для добавления данных в БД
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "" || countBox.Text == "" || typeBox.Text == "" || idBox.SelectedItem == null || nameBox.Text == "Название" || countBox.Text == "Количество" || typeBox.Text == "Тип товара")
            {
                warningBox.Text = "Вы не заполнили одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";


                string selectedId = idBox.SelectedItem.ToString();
                int IdSpecification = 0;
                foreach (var item in SpecificationItem)
                {
                    if (item.Size == selectedId)
                    {
                        IdSpecification = item.specificationId.Value;
                    }
                }

                CollectionClass collection = new CollectionClass
                {
                    CollectionId = null,
                    Name = nameBox.Text,
                    Count = Convert.ToInt32(countBox.Text),
                    TypeOfProduct = typeBox.Text,
                    specificationId = IdSpecification
                };
                CollectionItem.Add(collection);

                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(collection);
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
                UpdateCollection();
            }
        }

        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {
                warningBox.Text = "";
                foreach (CollectionClass item in CollectionItem)
                {
                    if (item.TypeOfProduct == choice)
                    {
                        collectionGrid.DataContext = null;
                        collectionGrid.ItemsSource = null;
                        List<CollectionClass> resultAcc = CollectionItem.Where(x => x.TypeOfProduct == item.TypeOfProduct).ToList();
                        collectionGrid.ItemsSource = resultAcc;
                    }
                }

                if (filterBox.SelectedValue.Equals("Все"))
                {
                    collectionGrid.DataContext = null;
                    collectionGrid.ItemsSource = null;
                    UpdateCollection();
                }
            }
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        //Метод для редактирования данных
        private void PUT(CollectionClass collection)
        {
            try
            {
                int id = CollectionItem[collectionGrid.SelectedIndex].CollectionId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(collection);
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

        private void countBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Collections"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    CollectionClass collection = new CollectionClass
                    {
                        CollectionId = null,
                        Name = ResetItems[i].Name,
                        Count = ResetItems[i].Count,
                        TypeOfProduct = ResetItems[i].TypeOfProduct,
                        specificationId = ResetItems[i].specificationId
                    };
                    CollectionItem.Add(collection);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(collection);
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
            UpdateCollection();
        }
    }
}
