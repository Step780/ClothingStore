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
    public partial class Packages : Page
    {
        List<PackageClass> PackagesItem;
        List<PackageClass> ResetItems = new List<PackageClass>();

        public Packages()
        {
            InitializeComponent();

            UpdatePackage();

            filterBox.Items.Add("Все");
            foreach (PackageClass item in PackagesItem)
            {
                if (filterBox.Items.Contains(item.CostOfPackage))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.CostOfPackage);
                }
            }
        }

        //Класс с данными
        public class PackageClass : INotifyPropertyChanged
        {
            private string sizeofpackage;
            private string weightofpackage;
            private int costofpackage;

            public int? PackageId { get; set; }
            public string SizeOfPackage {
                get => sizeofpackage; set
                {
                    sizeofpackage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(sizeofpackage)));
                }
            }
            public string WeightOfPackage {
                get => weightofpackage; set
                {
                    weightofpackage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(weightofpackage)));
                }
            }
            public int CostOfPackage {
                get => costofpackage; set
                {
                    costofpackage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(costofpackage)));
                }
            }

            public override string ToString()
            {
                return SizeOfPackage.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для вывода и обновления данных в DataGrid
        public void UpdatePackage()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            PackagesItem = JsonConvert.DeserializeObject<List<PackageClass>>(jsonString);

            packageGrid.ItemsSource = PackagesItem;

            foreach (var item in PackagesItem)
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
                int yourId = PackagesItem[packageGrid.SelectedIndex].PackageId.Value;
                ResetItems.Add(PackagesItem[packageGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdatePackage();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {

            if (sizeBox.Text == "" || weightBox.Text == "" || costBox.Text == "" || sizeBox.Text == "Размер" || weightBox.Text == "Вес" || costBox.Text == "Стоимость")
            {
                warningBox.Text = "Вы не ввели один или несколько пунктов!";
            }
            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";


                PackageClass package = new PackageClass
                {
                    PackageId = null,
                    SizeOfPackage = sizeBox.Text,
                    WeightOfPackage = weightBox.Text,
                    CostOfPackage = Convert.ToInt32(costBox.Text)

                };
                PackagesItem.Add(package);


                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(package);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();


                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdatePackage();
            }
        }

        //Метод для редактирования данных БД
        private void PUT(PackageClass package)
        {
            try
            {
                int id = PackagesItem[packageGrid.SelectedIndex].PackageId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(package);
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

            }
        }


        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            
            try
            {
                warningBox.Text = "";

                foreach (PackageClass item in PackagesItem)
                {
                    if (item.CostOfPackage == Convert.ToInt32(choice))
                    {

                        packageGrid.DataContext = null;
                        packageGrid.ItemsSource = null;
                        List<PackageClass> resultAcc = PackagesItem.Where(x => x.CostOfPackage == item.CostOfPackage).ToList();
                        packageGrid.ItemsSource = resultAcc;
                    }
                }
            }
            catch
            {

                if (filterBox.SelectedValue.Equals("Все"))
                {
                    packageGrid.DataContext = null;
                    packageGrid.ItemsSource = null;

                    UpdatePackage();
                }
                else
                {
                    warningBox.Text = "Что-то пошло не так";
                }
            }

        }

        private void sizeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void weightBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void costBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Packages"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    PackageClass package = new PackageClass
                    {
                        PackageId = null,
                        SizeOfPackage = ResetItems[i].SizeOfPackage,
                        WeightOfPackage = ResetItems[i].WeightOfPackage,
                        CostOfPackage = ResetItems[i].CostOfPackage

                    };
                    PackagesItem.Add(package);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(package);
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
            UpdatePackage();
        }
    }
}
