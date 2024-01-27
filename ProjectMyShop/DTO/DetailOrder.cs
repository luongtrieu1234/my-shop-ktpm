using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DTO
{
    public class DetailOrder : Data
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; } // Thay đổi này
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public object Clone()
        {
            return new DetailOrder() { OrderID = OrderID, ProductID = ProductID, Product = (Product)Product?.Clone(), Quantity = Quantity };
        }
    }
}
