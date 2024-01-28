using Microsoft.Win32;
using ProjectMyShopClient.DTO;
using ProjectMyShopClient.CBUS;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for AddProductScreen.xaml
    /// </summary>
    public partial class AddProductScreen : Window
    {
        public Product newProduct { get; set; }
        public int catIndex { get; set; } = -1;
        CProductBUS _ProductBUS { get; set; }


        public AddProductScreen(List<Category> category)
        {
            InitializeComponent();
            newProduct = new Product();
            this.DataContext = newProduct;
            categoryCombobox.ItemsSource = category;
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            catIndex = categoryCombobox.SelectedIndex;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            // Check validity

            //Product Product = new Product()
            //{
            //    ProductName = "Galaxy",
            //    Manufacturer = "Samsung",
            //    BoughtPrice = 500,
            //    SoldPrice = 700,
            //    Description = "stronglymanfok@outlook.com"
            //};
            if (catIndex < 0)
            {
                MessageBox.Show(this, "Invalid category");
            }
            else
            {
                DialogResult = true;
            }


        }

        private void chooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                newProduct.Avatar = new BitmapImage(new Uri(screen.FileName, UriKind.Absolute));
                avatar.Source = newProduct.Avatar;
            }
        }
    }
}
