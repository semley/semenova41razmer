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

namespace Semenova41raz
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public int vsecurrentProductCount = Семенова41размерEntities.GetContext().Product.Count();
        public ProductPage(User user)
        {
            InitializeComponent();
            if (user != null)
            {
                FIOTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 1:
                        RoleTB.Text = "Администратор"; break;
                    case 2:
                        RoleTB.Text = "Клиент"; break;
                    case 3:
                        RoleTB.Text = "Менеджер"; break;
                }
            }
            else
            {
                FIOTB.Text = "Гость";
                RoleTB.Visibility = Visibility.Hidden;
                Role.Visibility = Visibility.Hidden;
            }
            var currentProduct = Семенова41размерEntities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProduct;
            UpdateProduct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void UpdateProduct()
        {
            var currentProduct = Семенова41размерEntities.GetContext().Product.ToList();
            if (ComboType.SelectedIndex == 0)
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 100)).ToList();
            if (ComboType.SelectedIndex == 1)
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 9.99)).ToList();
            if (ComboType.SelectedIndex == 2)
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount <= 14.99)).ToList();
            if (ComboType.SelectedIndex == 3)
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 15)).ToList();

            currentProduct=currentProduct.Where(p=>p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            ProductListView.ItemsSource= currentProduct;
            int currentProductCount=currentProduct.Count();
            CountTB.Text = "кол-во "+(currentProductCount).ToString() + " из " + (vsecurrentProductCount).ToString();

            if(RButtonUp.IsChecked.Value)
            {
                currentProduct=currentProduct.OrderBy(p => p.ProductCost).ToList();
            }
            if (RButtonDown.IsChecked.Value)
            {
                currentProduct = currentProduct.OrderByDescending(p => p.ProductCost).ToList();
            }
            ProductListView.ItemsSource = currentProduct;
        }


        private void CompoType_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }



        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        
    }
}
