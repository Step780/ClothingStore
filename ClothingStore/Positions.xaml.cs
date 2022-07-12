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
using OxyPlot;
using OxyPlot.Series;
using Syncfusion.SfSkinManager;

namespace ClothingStore
{
    public partial class Positions : Page
    {
        List<PositionClass> PositionItem;
        List<PositionClass> ResetItems = new List<PositionClass>();

        //Метод для работы с диаграммой
        public class ViewModel
        {

            public List<PositionClass> Data { get; set; }

            public void UpdatePosition()
            {
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions"));

                WebReq.Method = "GET";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                Console.WriteLine(WebResp.StatusCode);
                Console.WriteLine(WebResp.Server);

                string jsonString;
                using (Stream stream = WebResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                    jsonString = reader.ReadToEnd();
                }

                Data = JsonConvert.DeserializeObject<List<PositionClass>>(jsonString);
            }

            public ViewModel()
            {
                UpdatePosition();
            }

        }

        public Positions()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTQ3OTk2QDMxMzkyZTMzMmUzMERKaHE3eGxpeUkrK0JwWis1bmNFUUUxWXRDVDQ3TG5PNncxMmo0aXphc009");

            InitializeComponent();

            UpdatePosition();

            SfSkinManager.SetTheme(this, new Theme("MaterialDark"));
            SfSkinManager.ApplyStylesOnApplication = true;

            filterBox.Items.Add("Все");
            foreach (PositionClass item in PositionItem)
            {
                if (filterBox.Items.Contains(item.Salary))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Salary);
                }
            }

            this.DataContext = new ViewModel();

        }

        //Класс с данными
        public class PositionClass : INotifyPropertyChanged
        {
            private string nameofposition;
            private int salary;

            public int? idPosition { get; set; }
            public string NameOfPosition
            {
                get => nameofposition; set
                {
                    nameofposition = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nameofposition)));
                }
            }
            public int Salary
            {
                get => salary; set
                {
                    salary = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(salary)));
                }
            }

            public override string ToString()
            {
                return NameOfPosition.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdatePosition()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            PositionItem = JsonConvert.DeserializeObject<List<PositionClass>>(jsonString);
            positionsGrid.ItemsSource = PositionItem;

            foreach (var item in PositionItem)
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
            int yourId = PositionItem[positionsGrid.SelectedIndex].idPosition.Value;
            ResetItems.Add(PositionItem[positionsGrid.SelectedIndex]);

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions/" + yourId));

            WebReq.Method = "DELETE";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            UpdatePosition();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == "" || salaryBox.Text == "" || nameBox.Text == "Название" || salaryBox.Text == "Зарплата")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";


                PositionClass position = new PositionClass
                {
                    idPosition = null,
                    NameOfPosition = nameBox.Text,
                    Salary = Convert.ToInt32(salaryBox.Text)
                };
                PositionItem.Add(position);


                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(position);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdatePosition();
            }
        }

        //Метод для редактирования данных в БД
        private void PUT(PositionClass position)
        {
            try
            {
                int id = PositionItem[positionsGrid.SelectedIndex].idPosition.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(position);
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
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {



                foreach (PositionClass item in PositionItem)
                {
                    try
                    {
                        if (item.Salary == Convert.ToInt32(choice))
                        {
                            positionsGrid.DataContext = null;
                            positionsGrid.ItemsSource = null;
                            List<PositionClass> resultAcc = PositionItem.Where(x => x.Salary == item.Salary).ToList();
                            positionsGrid.ItemsSource = resultAcc;
                        }
                    }
                    catch
                    {

                        if (filterBox.SelectedValue.Equals("Все"))
                        {
                            positionsGrid.DataContext = null;
                            positionsGrid.ItemsSource = null;

                            UpdatePosition();
                        }
                    }
                }


            }
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        private void salaryBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    PositionClass position = new PositionClass
                    {
                        idPosition = null,
                        NameOfPosition = ResetItems[i].NameOfPosition,
                        Salary = ResetItems[i].Salary
                    };
                    PositionItem.Add(position);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(position);
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

            if (ResetItems.Count == 0)
            {
                warningBox.Text = "Элементы для восстановления отсутствуют";

            }
            UpdatePosition();
        }
    }
}
