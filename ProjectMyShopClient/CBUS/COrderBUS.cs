using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShopClient.CBUS
{
    internal class COrderBUS : CBUS
    {
        private SOrderDAO _orderDAO;

        public COrderBUS()
        {
            //_orderDAO = new SOrderDAO();
            //if (_orderDAO.CanConnect())
            //{
            //    _orderDAO.Connect();
            //}
            this.ID = CObjectManager.CreateRemoteObject("SOrderBUS");
        }
        public List<Data> GetAll()
        {
            return this.ExecuteMethod("GetAll", null);
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            return this.ExecuteMethod("GetAllOrdersByDate", new { fromDate = FromDate, toDate = ToDate });
        }
        public List<Data> GetObjects(int offset, int size)
        {
            return this.ExecuteMethod("GetObjects", new { offset = offset, size = size });
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

        public void Add(Data data)
        {
            Order order = (Order)data;
            this.ExecuteMethod("Add", new { data = order });
            order.ID = this.ExecuteMethod("GetLastestInsertID", null);
        }
        public void Update(int orderID, Data data)
        {
            Order order = (Order)data;
            this.ExecuteMethod("Update", new { ID = orderID, data = order });
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
            this.ExecuteMethod("AddDetailOrder", new { detail = detail });
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
