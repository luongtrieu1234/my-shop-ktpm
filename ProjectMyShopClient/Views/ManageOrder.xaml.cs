﻿using ProjectMyShopClient.Config;
using ProjectMyShopClient.DTO;
using ProjectMyShopClient.CBUS;
using ProjectMyShopClient.ViewModels;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for ManageOrder.xaml
    /// </summary>
    public partial class ManageOrder : Page
    {
        private COrderBUS _orderBUS;

        OrderViewModel _vm;
        DateTime FromDate;
        DateTime ToDate;

        public ManageOrder()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = new OrderViewModel();
            _orderBUS = new COrderBUS();

            FromDate = DateTime.Parse("1/1/1970");
            ToDate = DateTime.MaxValue;

            Reload();
            OrderDataGrid.ItemsSource = _vm.SelectedOrders;

            AppConfig.SetValue(AppConfig.LastWindow, "ManageOrder");
        }


        private void OrderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        int _totalItems = 0;
        int _currentPage = 1;
        int _totalPages = 1;
        int _rowsPerPage = 8;

        void Reload()
        {
            _vm.Orders = new BindingList<Order>(_orderBUS.GetAllOrdersByDate(FromDate, ToDate));
            _vm.SelectedOrders = _vm.Orders.Skip((_currentPage - 1) * _rowsPerPage)
                .Take(_rowsPerPage).ToList();

            _totalItems = _vm.Orders.Count();
            _totalPages = _totalItems / _rowsPerPage +
                (_totalItems % _rowsPerPage == 0 ? 0 : 1);

            if (_totalPages <= 0) _totalPages = 1;
            if (_currentPage > _totalPages) _currentPage = _totalPages;

            // control prev & next buttons
            PreviousButton.IsEnabled = FirstButton.IsEnabled = _currentPage > 1;
            NextButton.IsEnabled = LastButton.IsEnabled = _currentPage < _totalPages;

            CurrentPageText.Text = _currentPage.ToString();
            TotalPageText.Text = _totalPages.ToString();

            OrderDataGrid.ItemsSource = _vm.SelectedOrders;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int index = OrderDataGrid.SelectedIndex;
            if (index != -1)
            {
                Order order = _vm.SelectedOrders[index];
                var screen = new ManageDetailOrder(order);
                screen.Owner = this.Parent as Window;
                var result = screen.ShowDialog();

                if (result == true)
                {
                    _orderBUS.Update(_vm.SelectedOrders[index].ID, order);
                    Reload();
                }
                else
                {
                    // Do nothing
                }
            }
            else
            {
                // Do nothing
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            var screen = new ManageDetailOrder(order);
            if (screen.ShowDialog() == true)
            {
                _orderBUS.Add(order);
                Reload();
            }
            else
            {
                // do nothing
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int i = OrderDataGrid.SelectedIndex;

            if (i != -1)
            {
                Order order = _vm.SelectedOrders[i];
                var res = MessageBox.Show($"Are you sure to delete this order: {order.ID} - {order.CustomerName}?", "Delete order", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    _orderBUS.Remove(order.ID);
                    Reload();
                    if (_vm.SelectedOrders.Count == 0)
                    {
                        if (_currentPage > 1)
                        {
                            _currentPage--;
                            Reload();
                        }
                        else
                        {
                            // Empty Orders List -> Do nothing
                        }
                    }
                }
            }
            else
            {
                // do nothing
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private TargetType GetParent<TargetType>(DependencyObject o) where TargetType : DependencyObject
        {
            if (o == null || o is TargetType) return (TargetType)o;
            return GetParent<TargetType>(VisualTreeHelper.GetParent(o));
        }

        private void DetailButton_Click(object sender, RoutedEventArgs e)
        {
            var row = GetParent<DataGridRow>((Button)sender);
            int index = OrderDataGrid.Items.IndexOf(row.Item);
            if (index != -1)
            {
                Order order = _vm.SelectedOrders[index];
                var screen = new ManageDetailOrder(order);
                screen.Owner = this.Parent as Window;
                var result = screen.ShowDialog();


                if (result == true)
                {
                    _orderBUS.Update(_vm.SelectedOrders[index].ID, order);
                    Reload();
                }
                else
                {
                }
            }
            else
            {
                // Do nothing
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage++;
            Reload();
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage -= 1;
            Reload();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromDatePicker.SelectedDate != null)
            {
                FromDate = (DateTime)FromDatePicker.SelectedDate;
            }
            else
            {
                FromDate = DateTime.Parse("1/1/1970");
            }

            if (ToDatePicker.SelectedDate != null)
            {
                ToDate = (DateTime)ToDatePicker.SelectedDate;
            }
            else
            {
                ToDate = DateTime.MaxValue;
            }

            if (FromDate <= ToDate)
            {
                Reload();
            }
            else
            {
                MessageBox.Show("Start Date cannot after End Date", "Date Filter", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void FirstButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = 1;
            Reload();
        }

        private void LastButton_Click(object sender, RoutedEventArgs e)
        {
            _currentPage = _totalPages;
            Reload();
        }
    }
}