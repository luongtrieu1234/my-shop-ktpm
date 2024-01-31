using Newtonsoft.Json.Linq;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShop.SBUS
{
    internal class SProductBUS : SBUS
    {
        private SProductDAO _productDAO;

        public SProductBUS()
        {
            _productDAO = (SProductDAO)SOjectManager.Prototypes["SProductDAO"];
            if (_productDAO.CanConnect())
            {
                _productDAO.Connect();
            }
        }

        public int GetTotalProduct()
        {
            return _productDAO.getTotalProduct();
        }
        public List<Product> GetTop5OutStock()
        {
            return _productDAO.GetTop5OutStock();
        }

        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            return _productDAO.getProductsAccordingToSpecificCategory(srcCategoryID);
        }

        public override void Add(Data data)
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
                _productDAO.Add(product);
                product.ID = _productDAO.GetLastestInsertID();
            }
        }
        public override void Remove(int ID)
        {
            _productDAO.Remove(ID);
        }
        public override void Update(int ID, Data data)
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
                _productDAO.Update(ID, product);
            }
        }

        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            return _productDAO.getBestSellingProductsInWeek(src);
        }

        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            return _productDAO.getBestSellingProductsInMonth(src);
        }

        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            return _productDAO.getBestSellingProductsInYear(src);
        }
        public override Data GetByID(int ProductID)
        {
            return _productDAO.GetByID(ProductID);
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    Debug.WriteLine("GetObjectType Product called");
                    return GetObjectType();
                case "Clone":
                    Debug.WriteLine("Clone Product called");
                    return Clone();
                case "GetByID":
                    Debug.WriteLine("GetByID Product called");
                    return GetByID((int)inputParams.productID);
                case "Add":
                    Debug.WriteLine("Add Product called");
                    Add(((JObject)inputParams.data).ToObject<Product>());
                    return true;
                case "Update":
                    Debug.WriteLine("Update Product called");
                    Update((int)inputParams.id, ((JObject)inputParams.data).ToObject<Product>());
                    return true;
                case "Remove":
                    Debug.WriteLine("Remove Product called");
                    Remove((int)inputParams.productid);
                    return true;
                case "GetObjects":
                    Debug.WriteLine("GetObjects Product called");
                    return GetObjects((int)inputParams.offset, (int)inputParams.size);
                case "GetTotalProduct":
                    Debug.WriteLine("GetTotalProduct Product called");
                    return GetTotalProduct();
                case "GetTop5OutStock":
                    Debug.WriteLine("GetTop5OutStock Product called");
                    return GetTop5OutStock();
                case "getProductsAccordingToSpecificCategory":
                    Debug.WriteLine("getProductsAccordingToSpecificCategory Product called");
                    return getProductsAccordingToSpecificCategory((int)inputParams.srcCategoryID);
                case "getBestSellingProductsInWeek":
                    Debug.WriteLine("getBestSellingProductsInWeek Product called");
                    return getBestSellingProductsInWeek((DateTime)inputParams.src);
                case "getBestSellingProductsInMonth":
                    Debug.WriteLine("getBestSellingProductsInMonth Product called");
                    return getBestSellingProductsInMonth((DateTime)inputParams.src);
                case "getBestSellingProductsInYear":
                    Debug.WriteLine("getBestSellingProductsInYear Product called");
                    return getBestSellingProductsInYear((DateTime)inputParams.src);
            }
            return false;
        }
    }
}
