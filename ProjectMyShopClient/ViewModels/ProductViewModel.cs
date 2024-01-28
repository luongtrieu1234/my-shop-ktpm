﻿using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShopClient.ViewModels
{
    internal class ProductViewModel : INotifyPropertyChanged
    {
        public BindingList<Product> Products { get; set; } = new BindingList<Product>();
        public List<Product> SelectedProducts { get; set; } = new List<Product>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}