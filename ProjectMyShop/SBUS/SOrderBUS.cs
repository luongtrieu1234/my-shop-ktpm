using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;

namespace ProjectMyShop.SBUS
{
    internal class SOrderBUS:SBUS
    {
        private SOrderDAO _orderDAO;

        public SOrderBUS()
        {
            _orderDAO = new SOrderDAO();
            if (_orderDAO.CanConnect())
            {
                _orderDAO.Connect();
            }
        }
        public List<Order> GetAllOrders()
        {
            return _orderDAO.ExecuteMethod("GetAll", null);
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            return _orderDAO.ExecuteMethod("GetAllOrdersByDate", new { fromDate = FromDate , toDate = ToDate });
        }
        public List<Order> GetOrders(int offset, int size)
        {
            return _orderDAO.ExecuteMethod("GetObjects", new { offset = offset, size = size });
        }

        public static string StatusOpen = "Open";
        public static string StatusClose = "Close";
        public static string StatusProgess = "Progress";


        public static string GetStatus(int status)
        {
            switch (status)
            {
                case 0: return SOrderBUS.StatusOpen;
                case 1: return SOrderBUS.StatusClose;
                default: return SOrderBUS.StatusProgess;
            }
        }

        public void AddOrder(Order order)
        {
            _orderDAO.ExecuteMethod("Add", new { data = order });
            order.ID = _orderDAO.ExecuteMethod("GetLastestInsertID", null);
        }
        public void UpdateOrder(int orderID, Order order)
        {
            _orderDAO.ExecuteMethod("Update", new { ID = orderID, data = order });
        }
        public void DeleteOrder(int orderID)
        {
            if (orderID > -1)
            {
                _orderDAO.ExecuteMethod("Remove", new { ID = orderID });
            }
        }

        public int CountOrders()
        {
            return _orderDAO.ExecuteMethod("CountOrders", null);
        }
        public int CountOrderByWeek()
        {
            return _orderDAO.ExecuteMethod("CountOrderByWeek", null);
        }
        public int CountOrderByMonth()
        {
            return _orderDAO.ExecuteMethod("CountOrderByMonth", null);
        }

        public void AddDetailOrder(DetailOrder detail)
        {
            _orderDAO.ExecuteMethod("AddDetailOrder", new { detail = detail });
        }

        public void UpdateDetailOrder(int oldProductID, DetailOrder detail)
        {
            if(detail.Quantity >= 0)
            {
                _orderDAO.ExecuteMethod("UpdateDetailOrder", new { oldProductID, detail });
            }
            else
            {
                throw new Exception("Invalid Quantity");
            }
        }
        public void DeleteDetailOrder(DetailOrder detail)
        {
            _orderDAO.ExecuteMethod("DeleteDetailOrder", new { detail });
        }
    }
}
