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
using static ClothingStore.Acc;
using static ClothingStore.Positions;
using static ClothingStore.SalaryCalculations;
using static ClothingStore.UserData;

namespace ClothingStore
{
    public partial class Employees : Page
    {
        List<EmployeesClass> EmployeesItems;
        List<PositionClass> PositionItems;
        List<UserDataClass> UserDataItems;
        List<Account> AccountItems;
        List<SalaryClass> SalaryItems;
        List<EmployeesClass> ResetItems = new List<EmployeesClass>();

        public Employees()
        {
            InitializeComponent();

            UpdateEmployees();

            BoxAccount();
            BoxPosition();
            BoxSalary();
            BoxUserData();

            filterBox.Items.Add("Все");
            foreach (EmployeesClass item in EmployeesItems)
            {
                if (filterBox.Items.Contains(item.position.NameOfPosition))
                {
                    continue;
                }
                else
                {
                    filterBox.Items.Add(item.position.NameOfPosition);
                }
            }
        }

        //Класс с данными
        public class EmployeesClass : INotifyPropertyChanged
        {
            private string surname;
            private string name;
            private string patronymic;


            public int? EmployeeId { get; set; }
            public string Surname {
                get => surname; set
                {
                    surname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(surname)));
                }
            }
            public string Name {
                get => name; set
                {
                    name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(name)));
                }
            }
            public string Patronymic {
                get => patronymic; set
                {
                    patronymic = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(patronymic)));
                }
            }
            public int PositionId { get; set; }
            public int UserDatumId { get; set; }
            public int AccountId { get; set; }
            public int SalaryCalculationId { get; set; }

            public PositionClass position { get; set; }
            public UserDataClass userdatum { get; set; }
            public Account account { get; set; }
            public SalaryClass salaryCalculation { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            public override string ToString()
            {
                return Surname.ToString();
            }

        }

        //Метод для заполнения ComboBox
        public void BoxPosition()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Positions"));

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

            PositionItems = JsonConvert.DeserializeObject<List<PositionClass>>(jsonString1);

            foreach (PositionClass item in PositionItems)
            {
                boxPosition.Items.Add(item.NameOfPosition);
            }
            boxPosition.SelectedIndex = 0;
        }

        //Метод для заполнения ComboBox
        public void BoxUserData()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/UserDatums"));

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

            UserDataItems = JsonConvert.DeserializeObject<List<UserDataClass>>(jsonString1);

            foreach (UserDataClass item in UserDataItems)
            {
                boxUser.Items.Add(item.Mail);
            }
            boxUser.SelectedIndex = 0;
        }

        //Метод для заполнения ComboBox
        public void BoxAccount()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Accounts"));

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

            AccountItems = JsonConvert.DeserializeObject<List<Account>>(jsonString1);

            foreach (Account item in AccountItems)
            {
                boxAccount.Items.Add(item.Login);
            }
            boxAccount.SelectedIndex = 0;
        }

        //Метод для заполнения ComboBox
        public void BoxSalary()
        {
            HttpWebRequest WebReq1 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/SalaryCalculations"));

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

            SalaryItems = JsonConvert.DeserializeObject<List<SalaryClass>>(jsonString1);

            foreach (SalaryClass item in SalaryItems)
            {
                boxSalary.Items.Add(item.Schelude);
            }
            boxSalary.SelectedIndex = 0;

        }

        //Метод для обновления и вывода данных в DataGrid
        public void UpdateEmployees()
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees"));

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            EmployeesItems = JsonConvert.DeserializeObject<List<EmployeesClass>>(jsonString);
            employeesGrid.ItemsSource = EmployeesItems;

            foreach (var item in EmployeesItems)
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
                int yourId = EmployeesItems[employeesGrid.SelectedIndex].EmployeeId.Value;
                ResetItems.Add(EmployeesItems[employeesGrid.SelectedIndex]);

                HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees/" + yourId));

                WebReq.Method = "DELETE";

                HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

                UpdateEmployees();
        }

        //Метод для добавления данных в БД
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || txtSurname.Text == "" || txtPatronymic.Text == "" || boxPosition.SelectedItem == null || boxAccount.SelectedItem == null || boxSalary.SelectedItem == null || boxUser.SelectedItem == null || txtName.Text == "Имя" || txtSurname.Text == "Фамилия" || txtPatronymic.Text == "Отчество")
            {
                warningBox.Text = "Вы не ввели одно или несколько полей, либо не выбрали пункты из выпадающих списков!";
            }

            else
            {
                warningBox.Text = "";
                HttpWebRequest WebReq5 = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees"));
                WebReq5.ContentType = "application/json; charset=utf-8";
                WebReq5.Accept = "application/json; charset=utf-8";
                WebReq5.Method = "POST";

                string selectedIdPosition = boxPosition.SelectedItem.ToString();
                int IdPosition = 0;
                foreach (var item in PositionItems)
                {
                    if (item.NameOfPosition == selectedIdPosition)
                    {
                        IdPosition = item.idPosition.Value;
                    }
                }

                string selectedIdAccount = boxAccount.SelectedItem.ToString();
                int IdAccount = 0;
                foreach (var item in AccountItems)
                {
                    if (item.Login == selectedIdAccount)
                    {
                        IdAccount = item.idAccount.Value;
                    }
                }

                string selectedIdSalary = boxSalary.SelectedItem.ToString();
                int IdSalary = 0;
                foreach (var item in SalaryItems)
                {
                    if (item.CostOfHour == Convert.ToInt32(selectedIdSalary))
                    {
                        IdSalary = item.salaryCalculationId.Value;
                    }
                }

                string selectedIdUserData = boxUser.SelectedItem.ToString();
                int IdUserData = 0;
                foreach (var item in UserDataItems)
                {
                    if (item.Mail == selectedIdUserData)
                    {
                        IdUserData = item.userDatumId.Value;
                    }
                }

                EmployeesClass employees = new EmployeesClass
                {
                    EmployeeId = null,
                    Name = txtName.Text,
                    Surname = txtSurname.Text,
                    Patronymic = txtPatronymic.Text,
                    AccountId = IdAccount,
                    PositionId = IdPosition,
                    SalaryCalculationId = IdSalary,
                    UserDatumId = IdUserData
                };
                EmployeesItems.Add(employees);

                using (var streamWriter = new StreamWriter(WebReq5.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(employees);
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
                UpdateEmployees();
            }
        }

        //Метод для фильтрации данных через ComboBox
        private void filterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string choice = filterBox.SelectedItem.ToString();

            try
            {
                warningBox.Text = "";

                foreach (EmployeesClass item in EmployeesItems)
                {
                    try
                    {
                        if (item.position.NameOfPosition == choice)
                        {
                            employeesGrid.DataContext = null;
                            employeesGrid.ItemsSource = null;
                            List<EmployeesClass> resultAcc = EmployeesItems.Where(x => x.position.NameOfPosition == item.position.NameOfPosition).ToList();
                            employeesGrid.ItemsSource = resultAcc;
                        }
                    }
                    catch
                    {

                    }
                }

                if (filterBox.SelectedValue.Equals("Все"))
                {
                    employeesGrid.DataContext = null;
                    employeesGrid.ItemsSource = null;

                    UpdateEmployees();
                }
            }
            catch
            {
                warningBox.Text = "Что-то пошло не так";
            }
        }

        private void PUT(EmployeesClass employees)
        {
            try
            {
                warningBox.Text = "";
                int id = EmployeesItems[employeesGrid.SelectedIndex].EmployeeId.Value;

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees/" + id));
                webRequest.ContentType = "application/json; charset=utf-8";
                webRequest.Accept = "application/json; charset=utf-8";
                webRequest.Method = "PUT";

                using (var streamWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(employees);
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
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://localhost:44329/api/Employees"));
            WebReq.ContentType = "application/json; charset=utf-8";
            WebReq.Accept = "application/json; charset=utf-8";
            WebReq.Method = "POST";

            for (int i = 0; i <= ResetItems.Count; i++)
            {
                try
                {
                    EmployeesClass employees = new EmployeesClass
                    {
                        EmployeeId = null,
                        Name = ResetItems[i].Name,
                        Surname = ResetItems[i].Surname,
                        Patronymic = ResetItems[i].Patronymic,
                        AccountId = ResetItems[i].AccountId,
                        PositionId = ResetItems[i].PositionId,
                        SalaryCalculationId = ResetItems[i].SalaryCalculationId,
                        UserDatumId = ResetItems[i].UserDatumId
                    };
                    EmployeesItems.Add(employees);

                    using (var streamWriter = new StreamWriter(WebReq.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(employees);
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
            UpdateEmployees();
        }
    }
}
