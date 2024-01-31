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
            if ((subject as CategoryViewModel).State == 1)
            {
                MessageBox.Show($"Product: Reacted to the event\nProducts with this category have been changed", 
                    "Message", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
                Debug.WriteLine("Product: Reacted to the event.");
            } else if ((subject as CategoryViewModel).State == 2)
            {
                MessageBox.Show($"Product: Reacted to the event\nProducts with this category have been deleted",
                    "Message",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                Debug.WriteLine("Product: Reacted to the event.");
            }
        }
    }
}