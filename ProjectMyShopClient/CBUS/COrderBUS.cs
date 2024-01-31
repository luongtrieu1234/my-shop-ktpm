using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;

namespace ProjectMyShopClient.CBUS
{
    internal class COrderBUS : CBUS
    {

        public COrderBUS()
        {
            this.ID = CObjectManager.CreateRemoteObject("SOrderBUS");
        }
        public List<Order> GetAll()
        {
            dynamic datas = this.ExecuteMethod("GetAll", null);
            List<Order> rs = ConvertJArrayToList<Order>(datas);
            return rs;
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            dynamic datas = this.ExecuteMethod("GetAllOrdersByDate", new { fromDate = FromDate, toDate = ToDate });
            List<Order> rs = ConvertJArrayToList<Order>(datas);
            return rs;
        }
        public List<Order> GetObjects(int offset, int size)
        {
            dynamic datas = this.ExecuteMethod("GetObjects", new { offset,size });
            List<Order> rs = ConvertJArrayToList<Order>(datas);
            return rs;
        }

        public static string StatusOpen = "Open";
        public static string StatusClose = "Close";
        public static string StatusProgess = "Progress";


        public static string GetStatus(int status)
        {
            switch (status)
            {
                case 0: return COrderBUS.StatusOpen;
                case 1: return COrderBUS.StatusClose;
                default: return COrderBUS.StatusProgess;
            }
        }

        public void Add(Order data)
        {
            foreach(DetailOrder detailOrder in data.DetailOrderList) {
                detailOrder.Product.Avatar = null;
            }
            this.ExecuteMethod("Add", new { data });
        }
        public void Update(int orderID, Order data)
        {
            foreach (DetailOrder detailOrder in data.DetailOrderList)
            {
                detailOrder.Product.Avatar = null;
            }
            this.ExecuteMethod("Update", new { ID = orderID, data });
        }
        public void Remove(int orderID)
        {
            if (orderID > -1)
            {
                this.ExecuteMethod("Remove", new { ID = orderID });
            }
        }

        public int CountOrders()
        {
            dynamic data= this.ExecuteMethod("CountOrders", null);
            int rs = (int)data;
            return rs;
        }
        public int CountOrderByWeek()
        {
            dynamic data = this.ExecuteMethod("CountOrderByWeek", null);
            int rs = (int)data;
            return rs;
        }
        public int CountOrderByMonth()
        {
            dynamic data = this.ExecuteMethod("CountOrderByMonth", null);
            int rs = (int)data;
            return rs;
        }

        public void AddDetailOrder(DetailOrder detail)
        {
            detail.Product.Avatar = null;
            this.ExecuteMethod("AddDetailOrder", new { detail });
        }

        public void UpdateDetailOrder(int oldProductID, DetailOrder detail)
        {
            detail.Product.Avatar = null;
            if (detail.Quantity >= 0)
            {
                this.ExecuteMethod("UpdateDetailOrder", new { oldProductID, detail });
            }
            else
            {
                throw new Exception("Invalid Quantity");
            }
        }
        public void DeleteDetailOrder(DetailOrder detail)
        {
            detail.Product.Avatar = null;
            this.ExecuteMethod("DeleteDetailOrder", new { detail });
        }
    }
}
