using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

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
        public override List<Data> GetAll()
        {
            return _orderDAO.ExecuteMethod("GetAll", null);
            //List<Data> datas = _orderDAO.ExecuteMethod("GetAll", null);
            //List<Order> result = new List<Order>();
            //foreach (Data data in datas)
            //{
            //    result.Add((Order)data);
            //}
            //return new List<Data>(result);
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            return _orderDAO.ExecuteMethod("GetAllOrdersByDate", new { fromDate = FromDate , toDate = ToDate });
        }
        public override List<Data> GetObjects(int offset, int size)
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

        public override void Add(Data data)
        {
            Order order = (Order)data;
            _orderDAO.ExecuteMethod("Add", new { data = order });
            order.ID = _orderDAO.ExecuteMethod("GetLastestInsertID", null);
        }
        public override void Update(int orderID, Data data)
        {
            Order order = (Order)data;
            _orderDAO.ExecuteMethod("Update", new { ID = orderID, data = order });
        }
        public override void Remove(int orderID)
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
        static object GetPropertyValue(object obj, string propertyName)
        {
            // Use reflection to get the value of the property
            PropertyInfo property = obj.GetType().GetProperty(propertyName);

            if (property != null)
            {
                return property.GetValue(obj);
            }

            return null;
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    Debug.WriteLine("GetObjectType Order called");
                    return GetObjectType();
                case "Clone":
                    Debug.WriteLine("Clone Order called");
                    return Clone();
                case "GetByID":
                    Debug.WriteLine("GetByID Order called");
                    return GetByID(inputParams.ID);
                case "GetAll":
                    Debug.WriteLine("GetAll Order called");
                    return GetAll();
                case "Add":
                    Debug.WriteLine("Add Order called");
                    Add(inputParams.data);
                    return true;
                case "Update":
                    Debug.WriteLine("Update Order called");
                    Update(inputParams.ID, inputParams.data);
                    return true;
                case "Remove":
                    Debug.WriteLine("Remove Order called");
                    return Remove(inputParams.ID);
                case "GetObjects":
                    Debug.WriteLine("GetObjects Order called");
                    return GetObjects(inputParams.offset, inputParams.size);
                case "AddDetailOrder":
                    Debug.WriteLine("AddDetailOrder Order called");
                    AddDetailOrder(GetPropertyValue(inputParams, "detail"));
                    return true;
                case "UpdateDetailOrder":
                    Debug.WriteLine("UpdateDetailOrder Order called");
                    UpdateDetailOrder(inputParams.oldProductID, inputParams.detail);
                    return true;
                case "DeleteDetailOrder":
                    Debug.WriteLine("DeleteDetailOrder Order called");
                    DeleteDetailOrder(inputParams.detail);
                    return true;
                case "GetAllOrdersByDate":
                    Debug.WriteLine("GetAllOrdersByDate Order called");
                    return GetAllOrdersByDate(GetPropertyValue(inputParams, "fromDate"),
                        GetPropertyValue(inputParams, "toDate"));
                case "GetStatus":
                    Debug.WriteLine("GetStatus Order called");
                    return GetStatus(inputParams.status);
                case "CountOrders":
                    Debug.WriteLine("CountOrders Order called");
                    return CountOrders();
                case "CountOrderByWeek":
                    Debug.WriteLine("CountOrderByWeek Order called");
                    return CountOrderByWeek();
                case "CountOrderByMonth":
                    Debug.WriteLine("CountOrderByMonth Order called");
                    return CountOrderByMonth();

            }
            return false;
        }
    }
}
