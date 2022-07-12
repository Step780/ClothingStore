using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame MainFrameInstance;
        public static string role;
        public MainWindow()
        {
            InitializeComponent();
            
            MainFrameInstance = MainFrame;
            MainFrame.Navigate(new LoginPage());
            
            if (role == "Бухгалтер")
            {
                btnAccounting.Visibility = Visibility.Hidden;
                btnBrand.Visibility = Visibility.Hidden;
                btnCollection.Visibility = Visibility.Hidden;
                btnDelivery.Visibility = Visibility.Hidden;
                btnEmployee.Visibility = Visibility.Hidden;
                btnProduct.Visibility = Visibility.Hidden;
            }
        }

        private void btnAcc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Acc());
        }

        private void btnAccounting_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new AccountingAdverstining());
        }

        private void btnAdverstining_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new AdverstiningCalcs());
        }

        private void btnBrand_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Brands());
        }

        private void btnCollection_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Collections());
        }

        private void btnDelivery_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Deliveries());
        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Employees());
        }

        private void btnPackage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Packages());
        }

        private void btnPlaces_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new PlaceOfResidences());
        }

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Positions());
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Products());
        }

        private void btnProductType_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new ProductTypes());
        }

        private void btnSalary_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new SalaryCalculations());
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Stocks());
        }

        private void btnUserData_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new UserData());
        }

        private void btnSpecification_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new Specifications());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.MainFrameInstance.Navigate(new AllTables(role));

        }
    }
}
