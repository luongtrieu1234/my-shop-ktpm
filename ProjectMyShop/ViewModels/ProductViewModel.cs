using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectMyShop.ViewModels
{
    internal class ProductViewModel : INotifyPropertyChanged, IObserver
    {
        public BindingList<Product> Products { get; set; } = new BindingList<Product>();
        public List<Product> SelectedProducts { get; set; } = new List<Product>();

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Update(ISubject subject)
        {
            //if ((subject as CategoryViewModel).State == 0 || (subject as CategoryViewModel).State >= 2)
            //{
            //foreach (var item in Products)
            //{
            MessageBox.Show("Product: Reacted to the event", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
            Debug.WriteLine("Product: Reacted to the event.");
                    //Debug.WriteLine("Product: " + item.ProductName);
            //}
            //}
        }
    }
}