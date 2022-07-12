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
    
    public partial class AllTables : Page
    {
        public string mainrole;

        public AllTables(string role)
        {
            InitializeComponent();
            mainrole = role;
            //Проверка на роль
            switch (role)
            {
                case "Бухгалтер":
                    
                    btnAccAdverst.Visibility = Visibility.Visible;
                    btnBrands.Visibility = Visibility.Hidden;
                    btnCollections.Visibility = Visibility.Hidden;
                    btnDelivery.Visibility = Visibility.Hidden;
                    btnEmployees.Visibility = Visibility.Hidden;
                    btnProducts.Visibility = Visibility.Hidden;
                    btnPackages.Visibility = Visibility.Hidden;
                    btnPlaces.Visibility = Visibility.Hidden;
                    btnSpecifications.Visibility = Visibility.Hidden;
                    btnStock.Visibility = Visibility.Hidden;
                    btnAccount.Visibility = Visibility.Hidden;
                    
                    break;

                case "Администратор":
                    
                    break;

                case "Продавец":
                    
                    btnAccAdverst.Visibility = Visibility.Hidden;
                    btnAccount.Visibility = Visibility.Hidden;
                    btnAdverstCalc.Visibility = Visibility.Hidden;
                    btnEmployees.Visibility = Visibility.Hidden;
                    btnPosition.Visibility = Visibility.Hidden;
                    btnStock.Visibility = Visibility.Hidden;
                   
                    break;

                case "Кладовщик":
                    
                    btnAccAdverst.Visibility = Visibility.Hidden;
                    btnAccount.Visibility = Visibility.Hidden;
                    btnAdverstCalc.Visibility = Visibility.Hidden;
                    btnCollections.Visibility = Visibility.Hidden;
                    btnEmployees.Visibility = Visibility.Hidden;
                    btnPosition.Visibility = Visibility.Hidden;
                    btnStock.Visibility = Visibility.Hidden;
                    btnSpecifications.Visibility = Visibility.Hidden;
                    btnSalary.Visibility = Visibility.Hidden;
                    
                    break;

                case "СММ":
                    
                    btnAccount.Visibility = Visibility.Hidden;
                    btnAdverstCalc.Visibility = Visibility.Visible;
                    btnCollections.Visibility = Visibility.Hidden;
                    btnEmployees.Visibility = Visibility.Hidden;
                    btnPosition.Visibility = Visibility.Hidden;
                    btnBrands.Visibility = Visibility.Hidden;
                    btnProducts.Visibility = Visibility.Hidden;
                    btnType.Visibility = Visibility.Hidden;
                    btnPosition.Visibility = Visibility.Hidden;
                    btnDelivery.Visibility = Visibility.Hidden;
                    btnSpecifications.Visibility = Visibility.Hidden;
                    btnCollections.Visibility = Visibility.Hidden;

                    break;

                case "Отдел кадров":
                    
                    btnAdverstCalc.Visibility = Visibility.Hidden;
                    btnCollections.Visibility = Visibility.Hidden;
                    btnBrands.Visibility = Visibility.Hidden;
                    btnProducts.Visibility = Visibility.Hidden;
                    btnType.Visibility = Visibility.Hidden;
                    break;

            }

        }

        public void CheckRole()
        {
            switch (mainrole)
            {
                case "Бухгалтер":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAccounting.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAdverstining.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnSalary.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;

                    break;

                case "Администратор":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAcc.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAccounting.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAdverstining.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnBrand.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnCollection.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnDelivery.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnEmployee.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPackage.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPlaces.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPosition.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProduct.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProductType.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnSalary.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnSpecification.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnStock.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnUserData.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;

                    break;

                case "Продавец":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnBrand.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnCollection.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnDelivery.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPackage.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProduct.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProductType.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnSpecification.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;


                    break;

                case "Кладовщик":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnBrand.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProduct.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnProductType.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPackage.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnDelivery.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;


                    break;

                case "СММ":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAccounting.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnStock.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;


                    break;

                case "Отдел кадров":
                    ((MainWindow)Application.Current.MainWindow).rectangle.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnEmployee.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnUserData.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPlaces.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnPosition.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnAcc.Visibility = Visibility.Visible;
                    ((MainWindow)Application.Current.MainWindow).btnMain.Visibility = Visibility.Visible;


                    break;

            }
        }


        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Acc());
        }

        private void btnAccAdverst_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new AccountingAdverstining());
        }

        private void btnBrands_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Brands());
        }

        private void btnCollections_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Collections());
        }

        private void btnAdverstCalc_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new AdverstiningCalcs());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new UserData());
        }

        private void btnDelivery_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Deliveries());
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Employees());
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Products());
        }

        private void btnType_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new ProductTypes());
        }

        private void btnPosition_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Positions());

        }

        private void btnPackages_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Packages());

        }

        private void btnPlaces_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new PlaceOfResidences());

        }

        private void btnSalary_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new SalaryCalculations());

        }

        private void btnSpecifications_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Specifications());

        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new Stocks());

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            CheckRole();
            MainWindow.MainFrameInstance.Navigate(new LoginPage());
        }
    }
}
