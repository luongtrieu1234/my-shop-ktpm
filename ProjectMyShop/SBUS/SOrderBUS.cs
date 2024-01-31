using Newtonsoft.Json.Linq;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShop.SBUS
{
    internal class SOrderBUS:SBUS
    {
        private SOrderDAO _orderDAO;

        public SOrderBUS()
        {
            _orderDAO = (SOrderDAO)SOjectManager.Prototypes["SOrderDAO"];
            if (_orderDAO.CanConnect())
            {
                _orderDAO.Connect();
            }
        }
        public override void Add(Data data)
        {
            Order order = (Order)data;
            _orderDAO.Add(order);
            order.ID = _orderDAO.GetLastestInsertID();
        }
        public override void Update(int orderID, Data data)
        {
            _orderDAO.Update(orderID, data);
        }
        public override void Remove(int orderID)
        {
            if (orderID > -1)
            {
                _orderDAO.Remove(orderID);
            }
        }
        public override List<Data> GetAll()
        {

            List<Data> datas = _orderDAO.GetAll();
            return datas;
        }
        public override List<Data> GetObjects(int offset, int size)
        {
            List<Data> datas = _orderDAO.GetObjects(offset,  size );
            return datas;
        }
        public List<Order> GetAllOrdersByDate(DateTime FromDate, DateTime ToDate)
        {
            List<Order> datas = _orderDAO.GetAllOrdersByDate(FromDate , ToDate );
            return datas;
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

        

        public int CountOrders()
        {
            int rs = _orderDAO.CountOrders();
            return rs;
        }
        public int CountOrderByWeek()
        {
            return _orderDAO.CountOrderByWeek();
        }
        public int CountOrderByMonth()
        {
            return _orderDAO.CountOrderByMonth();
        }

        public void AddDetailOrder(DetailOrder detail)
        {
            _orderDAO.AddDetailOrder(detail);
        }

        public void UpdateDetailOrder(int oldProductID, DetailOrder detail)
        {
            if(detail.Quantity >= 0)
            {
                _orderDAO.UpdateDetailOrder( oldProductID, detail);
            }
            else
            {
                throw new Exception("Invalid Quantity");
            }
        }
        public void DeleteDetailOrder(DetailOrder detail)
        {
            _orderDAO.DeleteDetailOrder(detail );
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
                    return GetByID((int)inputParams.ID);
                case "GetAll":
                    Debug.WriteLine("GetAll Order called");
                    return GetAll();
                case "Add":
                    Debug.WriteLine("Add Order called");
                    Add(((JObject)inputParams.data).ToObject<Order>());
                    return true;
                case "Update":
                    Debug.WriteLine("Update Order called");
                    Update((int)inputParams.ID, ((JObject)inputParams.data).ToObject<Order>());
                    return true;
                case "Remove":
                    Debug.WriteLine("Remove Order called");
                    Remove((int)inputParams.ID);
                    return true;
                case "GetObjects":
                    Debug.WriteLine("GetObjects Order called");
                    return GetObjects((int)inputParams.offset, (int)inputParams.size);
                case "AddDetailOrder":
                    Debug.WriteLine("AddDetailOrder Order called");
                    AddDetailOrder(((JObject)inputParams.detail).ToObject<DetailOrder>());
                    return true;
                case "UpdateDetailOrder":
                    Debug.WriteLine("UpdateDetailOrder Order called");
                    UpdateDetailOrder((int)inputParams.oldProductID, ((JObject)inputParams.detail).ToObject<DetailOrder>());
                    return true;
                case "DeleteDetailOrder":
                    Debug.WriteLine("DeleteDetailOrder Order called");
                    DeleteDetailOrder(((JObject)inputParams.detail).ToObject<DetailOrder>());
                    return true;
                case "GetAllOrdersByDate":
                    Debug.WriteLine("GetAllOrdersByDate Order called");
                    return GetAllOrdersByDate((DateTime)inputParams.fromDate, (DateTime)inputParams.toDate);
                case "GetStatus":
                    Debug.WriteLine("GetStatus Order called");
                    return GetStatus((int)inputParams.status);
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
