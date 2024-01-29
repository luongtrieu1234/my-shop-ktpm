using System.ComponentModel;
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
