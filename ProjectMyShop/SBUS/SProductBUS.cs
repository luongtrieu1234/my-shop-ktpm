using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.SBUS
{
    internal class SProductBUS
    {
        private SProductDAO _productDAO;

        public SProductBUS()
        {
            _productDAO = new SProductDAO();
            if (_productDAO.CanConnect())
            {
                _productDAO.Connect();
            }
        }

        public int GetTotalProduct()
        {
            return _productDAO.ExecuteMethod("getTotalProduct", null);
        }
        public List<Product> Top5OutStock()
        {
            return _productDAO.ExecuteMethod("GetTop5OutStock", null);
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            return _productDAO.ExecuteMethod("getProductsAccordingToSpecificCategory", new { srcCategoryID = srcCategoryID });
        }

        public void addProduct(Product product)
        {
            if (product.Stock < 0)
            {
                throw new Exception("Invalid stock");
            }
            else if (product.BoughtPrice < 0 || product.SoldPrice < 0)
            {
                throw new Exception("Invalid price");
            }
            else
            {
                product.UploadDate = DateTime.Now.Date;
                _productDAO.ExecuteMethod("Add", new { data = product });
                product.ID = _productDAO.GetLastestInsertID();
            }
        }
        public void removeProduct(Product product)
        {
            _productDAO.ExecuteMethod("Remove", new { productid = product.ID });
        }
        public void updateProduct(int ID, Product product)
        {
            Debug.WriteLine(product.Stock);
            if (product.Stock < 0)
            {
                throw new Exception("Invalid stock");
            }
            else if (product.BoughtPrice < 0 || product.SoldPrice < 0)
            {
                throw new Exception("Invalid price");
            }
            else
            {
                _productDAO.ExecuteMethod("Update", new { id = ID, data = product });
            }
        }

        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            return _productDAO.ExecuteMethod("getBestSellingProductsInWeek", new { src = src });
        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            return _productDAO.ExecuteMethod("getBestSellingProductsInMonth", new { src = src });
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            return _productDAO.ExecuteMethod("getBestSellingProductsInYear", new { src = src });
        }
        public Product? getProductByID(int ProductID)
        {
            return _productDAO.ExecuteMethod("GetByID", new { productID = ProductID });
        }
    }
}
