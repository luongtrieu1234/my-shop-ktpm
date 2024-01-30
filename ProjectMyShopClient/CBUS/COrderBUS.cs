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
            List<Data> datas = this.ExecuteMethod("GetAll", null);
            List<Order> rs = CObject.ConvertData<Order>(datas);
            return rs;
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            List<Data> datas = this.ExecuteMethod("GetAllOrdersByDate", new { fromDate = FromDate, toDate = ToDate });
            List<Order> rs = CObject.ConvertData<Order>(datas);
            return rs;
        }
        public List<Order> GetObjects(int offset, int size)
        {
            List<Data> datas = this.ExecuteMethod("GetObjects", new { offset = offset, size = size });
            List<Order> rs = CObject.ConvertData<Order>(datas);
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
            this.ExecuteMethod("Add", new { data });
        }
        public void Update(int orderID, Order data)
        {
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
            return this.ExecuteMethod("CountOrders", null);
        }
        public int CountOrderByWeek()
        {
            return this.ExecuteMethod("CountOrderByWeek", null);
        }
        public int CountOrderByMonth()
        {
            return this.ExecuteMethod("CountOrderByMonth", null);
        }

        public void AddDetailOrder(DetailOrder detail)
        {
            this.ExecuteMethod("AddDetailOrder", new { detail });
        }

        public void UpdateDetailOrder(int oldProductID, DetailOrder detail)
        {
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
            this.ExecuteMethod("DeleteDetailOrder", new { detail });
        }
    }
}
