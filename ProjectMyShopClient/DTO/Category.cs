using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShopClient.DTO
{
    public class Category : Data
    {
        public int ID { get; set; }
        public string? CatName { get; set; }
        public BitmapImage Avatar { get; set; }
        public BindingList<Product>? Products { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
