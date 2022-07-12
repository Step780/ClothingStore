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
    public partial class SalaryCalculations : Page
    {
        List<SalaryClass> SalaryItem;
        List<SalaryClass> ResetItems = new List<SalaryClass>();

        public SalaryCalculations()
        {
            InitializeComponent();

            UpdateSalary();

            filterBox.Items.Add("Все");
            foreach (SalaryClass item in SalaryItem)
            {
                if (filterBox.Items.Contains(item.Schelude))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.Schelude);
                }
            }
        }

        //Класс с данными
        public class SalaryClass : INotifyPropertyChanged
        {
            private int hours;
            private string schelude;
            private int cost;

            public int? salaryCalculationId { get; set; }
            public int HoursWorked {
                get => hours; set
                {
                    hours = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(hours)));
                }
            }
            public string Schelude {
                get => schelude; set
                {
                    schelude = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(schelude)));
                }
            }
            public int CostOfHour {
                get => cost; set
                {
                    cost = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(cost)));
                }
            }

            public override string ToString()
            {
                return Schelude.ToString();
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateSalary()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            SalaryItem = JsonConvert.DeserializeObject<List<SalaryClass>>(jsonString);
            salaryGrid.ItemsSource = SalaryItem;

            foreach (var item in SalaryItem)
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
                int yourId = SalaryItem[salaryGrid.SelectedIndex].salaryCalculationId.Value;
                ResetItems.Add(SalaryItem[salaryGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateSalary();
        }

        //Метод для добавления данных в БД
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (hoursBox.Text == "" || scheludeBox.Text == "" || costBox.Text == "" || hoursBox.Text == "Часов отработано" || scheludeBox.Text == "Расписание" || costBox.Text == "Стоимость часа")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations"));
                WebReq.ContentType = "application/json; charset=utf-8";
                WebReq.Accept = "application/json; charset=utf-8";
                WebReq.Method = "POST";

                SalaryClass salary = new SalaryClass
                {
                    salaryCalculationId = null,
                    HoursWorked = Convert.ToInt32(hoursBox.Text),
                    Schelude = scheludeBox.Text,
                    CostOfHour = Convert.ToInt32(costBox.Text)
                };
                SalaryItem.Add(salary);

                using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(salary);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                };

                var httpResponse = (HttpWebResponse)WebReq.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                }

                UpdateSalary();
            }
        }

        //Метод для редактирования данных в БД
        private void PUT(SalaryClass salary)
        {
            try
            {
                warningBox.Text = "";
                int id = SalaryItem[salaryGrid.SelectedIndex].salaryCalculationId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(salary);
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

            foreach (SalaryClass item in SalaryItem)
            {
                if (item.Schelude == choice)
                {
                    salaryGrid.DataContext = null;
                    salaryGrid.ItemsSource = null;
                    List<SalaryClass> resultAcc = SalaryItem.Where(x => x.Schelude == item.Schelude).ToList();
                    salaryGrid.ItemsSource = resultAcc;
                }
            }

            if (filterBox.SelectedValue.Equals("Все"))
            {
                salaryGrid.DataContext = null;
                salaryGrid.ItemsSource = null;

                UpdateSalary();
            }
        }

        private void hoursBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;

        }

        private void costBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789".IndexOf(e.Text) < 0;

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    SalaryClass salary = new SalaryClass
                    {
                        salaryCalculationId = null,
                        HoursWorked = ResetItems[i].HoursWorked,
                        Schelude = ResetItems[i].Schelude,
                        CostOfHour = ResetItems[i].CostOfHour
                    };
                    SalaryItem.Add(salary);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(salary);
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
            UpdateSalary();
        }
    }
}
