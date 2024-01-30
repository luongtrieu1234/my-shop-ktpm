using ProjectMyShopClient.Config;
using ProjectMyShopClient.DTO;
using ProjectMyShopClient.CBUS;
using ProjectMyShopClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {

        List<Category>? _categories = null;
        CategoryViewModel CategoryViewModel = new CategoryViewModel();
        private CCategoryBUS _categoryBUS = new CCategoryBUS();


        public ManageCategory()
        {
            InitializeComponent();
            CategoryViewModel.Categories = new BindingList<Category>();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new AddCategoryScreen();
            var result = screen.ShowDialog();
            if (result == true)
            {
                var newCategory = screen.newCategory;
                Debug.WriteLine(newCategory.CatName);


                try
                {
                    _categoryBUS.Add(newCategory);
                    loadCategory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(screen, ex.Message);
                }

            }

        }

        void loadCategory()
        {
            _categories = _categoryBUS.GetAll();
            categoriesListView.ItemsSource = _categories;

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var p = (Category)categoriesListView.SelectedItem;
            var screen = new EditCategoryScreen(p);
            var result = screen.ShowDialog();
            if (result == true)
            {
                var info = screen.EditedCategory;
                p.CatName = info.CatName;
                p.Avatar = info.Avatar;
                try
                {
                    _categoryBUS.Update(p.ID,p);
                    loadCategory();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception here");
                    MessageBox.Show(screen, ex.Message);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var p = (Category)categoriesListView.SelectedItem;
            var result = MessageBox.Show($"Bạn thật sự muốn xóa danh mục {p.CatName}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {

                _categoryBUS.Remove(p.ID);
                loadCategory();

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            AppConfig.SetValue(AppConfig.LastWindow, "ManageCategory");
            loadCategory();


        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        private void editMenuItemClick(object sender, RoutedEventArgs e)
        {

        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void categoriesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
