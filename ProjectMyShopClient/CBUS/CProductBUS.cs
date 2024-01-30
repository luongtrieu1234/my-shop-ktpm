﻿using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShopClient.CBUS
{
    internal class CProductBUS : CBUS
    {
        public CProductBUS()
        {
            this.ID = CObjectManager.CreateRemoteObject("SProductBUS");
        }

        public int GetTotalProduct()
        {
            return this.ExecuteMethod("getTotalProduct", null);
        }
        public List<Product> GetTop5OutStock()
        {
            return this.ExecuteMethod("GetTop5OutStock", null);
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            dynamic datas= this.ExecuteMethod("getProductsAccordingToSpecificCategory", new { srcCategoryID = srcCategoryID });
            List<Product> products = ConvertJArrayToList<Product>(datas);
            return products;
        }

        public void Add(Data data)
        {
            Product product = (Product)data;
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
            //return this.ExecuteMethod("getBestSellingProductsInWeek", new { src = src });
            return new List<BestSellingProduct>();
        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            //return this.ExecuteMethod("getBestSellingProductsInMonth", new { src = src });
            return new List<BestSellingProduct>();
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            //return this.ExecuteMethod("getBestSellingProductsInYear", new { src = src });
            return new List<BestSellingProduct>();
        }
        public Data GetByID(int ProductID)
        {
            return this.ExecuteMethod("GetByID", new { productID = ProductID });
        }
    }
}
