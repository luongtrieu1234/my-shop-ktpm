using ProjectMyShop;
using ProjectMyShop.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjectMyShopClient.ViewModels
{
    internal class CategoryViewModel : INotifyPropertyChanged
    {
        public BindingList<Category> Categories { get; set; } = new BindingList<Category>();
        public List<Category> SelectedCategories { get; set; } = new List<Category>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public CategoryViewModel()
        {
            Debug.WriteLine("CategoryViewModel");
            CommonClass.NotifyEvent += Common_NotifyEvent;
        }
        public void Common_NotifyEvent(object sender, EventArgs e)
        {
            // Code to handle the event
            Debug.WriteLine("NotifyEvent");
        }
    }
}
