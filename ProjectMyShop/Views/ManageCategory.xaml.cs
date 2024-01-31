using System;
using System.Collections.Generic;
using System.Windows;
using ProjectMyShop.DTO;
using ProjectMyShop.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using ProjectMyShop.Config;
using ProjectMyShop.SBUS;

namespace ProjectMyShop.Views
{
    /// <summary>
    /// Interaction logic for ManageCategory.xaml
    /// </summary>
    public partial class ManageCategory : Page
    {

        List<Category>? _categories = null;        
        CategoryViewModel CategoryViewModel = new CategoryViewModel();
        private SCategoryBUS _categoryBUS = new SCategoryBUS();
        //Server server = new Server();
        Server server = Server.Instance;

        ProductViewModel _vm = new ProductViewModel();
        public ManageCategory()
        {

            this.CategoryViewModel.Attach(_vm);
            InitializeComponent();
            SCategoryBUS catBUS = new SCategoryBUS();
            List<Category> categories = SObject.ConvertData<Category>(catBUS.ExecuteMethod("GetAll", null)) ;
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
                    _categoryBUS.ExecuteMethod("Add",new {data = newCategory });           
                    loadCategory();
                    server.SendMessage("New category added: " + newCategory.CatName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(screen, ex.Message);
                }

            }
            
        }

        void loadCategory()
        {
            _categories = SObject.ConvertData<Category>(_categoryBUS.ExecuteMethod("GetAll", null));
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
                    _categoryBUS.ExecuteMethod("Update", new { id = p.ID, data = p });
                    loadCategory();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception here");
                    MessageBox.Show(screen, ex.Message);
                }
            }
            Debug.WriteLine("Selection changed");
            this.CategoryViewModel.SomeBusinessLogic(1);
            server.SendMessage("Category edited: " + p.CatName);
            Debug.WriteLine(CategoryViewModel._observers);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var p = (Category)categoriesListView.SelectedItem;
            var result = MessageBox.Show($"Bạn thật sự muốn xóa danh mục {p.CatName}?",
                "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (MessageBoxResult.Yes == result)
            {

                _categoryBUS.ExecuteMethod("Remove",new { id = p.ID });
                loadCategory();
                server.SendMessage("Category deleted: " + p.CatName);
                this.CategoryViewModel.SomeBusinessLogic(2);

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
