using Microsoft.Win32;
using ProjectMyShop.DTO;
using ProjectMyShopClient.CBUS;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for AddCategoryScreen.xaml
    /// </summary>
    public partial class AddCategoryScreen : Window
    {
        public AddCategoryScreen()
        {
            InitializeComponent();
            newCategory = new Category();
            this.DataContext = newCategory;
        }

        public Category newCategory { get; set; }
        public int catIndex { get; set; } = -1;
        CCategoryBUS _categoryBUS { get; set; }

        private void chooseImageButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                newCategory.Avatar = new BitmapImage(new Uri(screen.FileName, UriKind.Absolute));
                avatar.Source = newCategory.Avatar;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }


}
