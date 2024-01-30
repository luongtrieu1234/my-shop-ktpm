using ProjectMyShopClient.DTO;
using ProjectMyShopClient.CBUS;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for AddProductOrder.xaml
    /// </summary>
    public partial class AddProductOrder : Window
    {
        CProductBUS _ProductBus;
        CCategoryBUS _categoryBus;
        List<Category> _categories;
        List<Product> _selectedProducts;
        public DetailOrder detailOrder;


        public AddProductOrder(DetailOrder detailOrder)
        {
            InitializeComponent();
            this.detailOrder = (DetailOrder)detailOrder.Clone();
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int i = ProductListView.SelectedIndex;
            if (i != -1)
            {
                detailOrder.Product = _selectedProducts[i];
            }
            detailOrder.Quantity = int.Parse(QuantityTextBox.Text);
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _ProductBus = new CProductBUS();
            _categoryBus = new CCategoryBUS();

            _categories = _categoryBus.GetAll();

            categoryCombobox.ItemsSource = _categories;

            categoryCombobox.SelectedIndex = 0;

            if (categoryCombobox.SelectedIndex >= 0)
            {
                _selectedProducts = _ProductBus.getProductsAccordingToSpecificCategory(_categories[categoryCombobox.SelectedIndex].ID);
                ProductListView.ItemsSource = _selectedProducts;
            }

            DataContext = detailOrder;
        }

        private void categoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryCombobox.SelectedIndex >= 0)
            {
                _selectedProducts = _ProductBus.getProductsAccordingToSpecificCategory(_categories[categoryCombobox.SelectedIndex].ID);
                ProductListView.ItemsSource = _selectedProducts;
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            var QuantityTextBox = e.OriginalSource as TextBox;

            if (QuantityTextBox != null)
            {
                if (QuantityTextBox.Text == "")
                {
                    QuantityTextBox.Text = "0";
                }
                else if ((int.Parse(QuantityTextBox.Text)
                    > detailOrder.Product.Stock))
                {
                    QuantityTextBox.Text = QuantityTextBox.Text.Remove(QuantityTextBox.Text.Length - 1);

                    if (int.Parse(QuantityTextBox.Text)
                        > detailOrder.Product.Stock)
                        QuantityTextBox.Text = detailOrder.Product.Stock.ToString();
                }
            }

        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            Regex _regex = new Regex("[^0-9]+$"); //regex that matches disallowed text
            return _regex.IsMatch(text);
        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = ProductListView.SelectedIndex;
            if (i != -1)
            {
                detailOrder.Product = _selectedProducts[(int)i];
                detailOrder.Quantity = 0;

                ProductTextBox.Text = detailOrder.Product.ProductName;
                QuantityTextBox.Text = detailOrder.Quantity.ToString();
            }
        }
    }
}
