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
    public partial class Specifications : Page
    {
        List<SpecificationClass> SpecificationItem;
        List<SpecificationClass> ResetItems = new List<SpecificationClass>();

        public Specifications()
        {
            InitializeComponent();

            UpdateSpecification();

            filterBox.Items.Add("Все");
            foreach (SpecificationClass item in SpecificationItem)
            {
                if (filterBox.Items.Contains(item.Cut))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Cut);
                }
            }
        }

        //Класс с данными
        public class SpecificationClass : INotifyPropertyChanged
        {
            private string composition;
            private int density;
            private string cut;
            private string size;

            public int? specificationId { get; set; }
            public string Composition {
                get => composition; set
                {
                    composition = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(composition)));
                }
            }
            public int Density {
                get => density; set
                {
                    density = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(density)));
                }
            }
            public string Cut {
                get => cut; set
                {
                    cut = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(cut)));
                }
            }
            public string Size {
                get => size; set
                {
                    size = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(size)));
                }
            }
            public override string ToString()
            {
                return Size.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateSpecification()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            SpecificationItem = JsonConvert.DeserializeObject<List<SpecificationClass>>(jsonString);
            specificationGrid.ItemsSource = SpecificationItem;

            foreach (var item in SpecificationItem)
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
                int yourId = SpecificationItem[specificationGrid.SelectedIndex].specificationId.Value;
                ResetItems.Add(SpecificationItem[specificationGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateSpecification();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((compositionBox.Text == "" || densityBox.Text == "" || cutBox.Text == "" || sizeBox.Text == "") || (compositionBox.Text == "Состав" || densityBox.Text == "Плотность" || cutBox.Text == "Крой" || sizeBox.Text == "Размеры"))
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }


            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";

                SpecificationClass specification = new SpecificationClass
                {
                    specificationId = null,
                    Composition = compositionBox.Text,
                    Density = Convert.ToInt32(densityBox.Text),
                    Cut = cutBox.Text,
                    Size = sizeBox.Text
                };
                SpecificationItem.Add(specification);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(specification);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdateSpecification();
            }
        }

        //Метод для редактировния данных в БД
        private void PUT(SpecificationClass specification)
        {
            try
            {
                warningBox.Text = "";
                int id = SpecificationItem[specificationGrid.SelectedIndex].specificationId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(specification);
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

        //Метод для фильтрации при помощи ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            foreach (SpecificationClass item in SpecificationItem)
            {
                if (item.Cut == choice)
                {
                    specificationGrid.DataContext = null;
                    specificationGrid.ItemsSource = null;
                    List<SpecificationClass> resultAcc = SpecificationItem.Where(x => x.Cut == item.Cut).ToList();
                    specificationGrid.ItemsSource = resultAcc;
                }
            }

            if (filterBox.SelectedValue.Equals("Все"))
            {
                specificationGrid.DataContext = null;
                specificationGrid.ItemsSource = null;

                UpdateSpecification();
            }
        }

        private void densityBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Specifications"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    SpecificationClass specification = new SpecificationClass
                    {
                        specificationId = null,
                        Composition = ResetItems[i].Composition,
                        Density = ResetItems[i].Density,
                        Cut = ResetItems[i].Cut,
                        Size = ResetItems[i].Size
                    };
                    SpecificationItem.Add(specification);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(specification);
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
            UpdateSpecification();
        }
    }
}
