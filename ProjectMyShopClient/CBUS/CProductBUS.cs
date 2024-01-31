using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShopClient.CBUS
{
    internal class CProductBUS : CBUS
    {
        public CProductBUS()
        {
            ID = CObjectManager.CreateRemoteObject("SProductBUS");
        }

        public int GetTotalProduct()
        {
            dynamic data = this.ExecuteMethod("GetTotalProduct", null);
            int rs = (int)data;
            return rs;
        }
        public List<Product> GetTop5OutStock()
        {
            dynamic datas = this.ExecuteMethod("GetTop5OutStock", null);
            List<Product> rs = ConvertJArrayToList<Product>(datas);
            return rs;
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            dynamic datas= this.ExecuteMethod("getProductsAccordingToSpecificCategory", new {  srcCategoryID });
            List<Product> products = ConvertJArrayToList<Product>(datas);
            return products;
        }

        public void Add(Data data)
        {
            Product product = (Product)data;
            product.Avatar = null;
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
                this.ExecuteMethod("Add", new { data = product });
            }
        }
        public void Remove(int ID)
        {
            this.ExecuteMethod("Remove", new { productid = ID });
        }
        public void Update(int ID, Data data)
        {
            Product product = (Product)data;
            product.Avatar = null;
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
                this.ExecuteMethod("Update", new { id = ID, data = product });
            }
        }

        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            dynamic datas = this.ExecuteMethod("getBestSellingProductsInWeek", new { src });
            List<BestSellingProduct> rs = ConvertJArrayToList<BestSellingProduct>(datas);
            return rs;

        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            dynamic datas = this.ExecuteMethod("getBestSellingProductsInMonth", new { src });
            List<BestSellingProduct> rs = ConvertJArrayToList<BestSellingProduct>(datas);
            return rs;
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            dynamic datas = this.ExecuteMethod("getBestSellingProductsInYear", new { src });
            List<BestSellingProduct> rs = ConvertJArrayToList<BestSellingProduct>(datas);
            return rs;
        }
        public Product GetByID(int ProductID)
        {

            dynamic data = this.ExecuteMethod("GetByID", new { productID = ProductID });
            Product rs = (Product)data;
            return rs;
        }
    }
}
