using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using ProjectMyShop.SDAO;

namespace ProjectMyShop.SBUS
{
    internal class SStatisticsBUS:SBUS
    {
        private SStatisticsDAO _statisticsDAO;

        public SStatisticsBUS()
        {
            _statisticsDAO = new SStatisticsDAO();
            //_statisticsDAO = (SStatisticsDAO)SOjectManager.Prototypes["SStatisticsDAO"];
            if (_statisticsDAO.CanConnect())
            {
                _statisticsDAO.Connect();
            }
        }
        static IEnumerable<string> GetDynamicObjectKeys(dynamic dynamicObject)
        {
            // Use reflection to get the keys of the dynamic object
            var dynamicObjectDictionary = dynamicObject as IDictionary<string, object>;
            if (dynamicObjectDictionary != null)
            {
                return dynamicObjectDictionary.Keys;
            }

            // If it's not a dictionary, you can use reflection to get the properties
            return ((IEnumerable<PropertyInfo>)dynamicObject.GetType().GetProperties()).Select(property => property.Name);

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
            IEnumerable<string> keys = GetDynamicObjectKeys(inputParams);
            Debug.WriteLine("debug " + (object)inputParams.GetType());
            Debug.WriteLine("debug 2 " + methodName.GetType());
            object value = GetPropertyValue(inputParams, "src");
            Debug.WriteLine("Value of MyDate property: " + value);
            Debug.WriteLine("Keys of the dynamic object:");
            foreach (string key in keys)
            {
                Debug.WriteLine("key " + key);
            }
            //int srcProductID = GetPropertyValue(inputParams, "srcProductID");
            //int srcCategoryID = GetPropertyValue(inputParams, "srcCategoryID");
            //DateTime srcDate = GetPropertyValue(inputParams, "srcDate");
            switch (methodName)
            {
                case "GetObjectType":
                    return GetObjectType();
                case "Clone":
                    return Clone();
                case "getTotalRevenueUntilDate":
                    string str = inputParams.ToString();
                    Debug.WriteLine("SStatisticsBUS.ExecuteMethod " + methodName + " " + str);
                    return getTotalRevenueUntilDate(GetPropertyValue(inputParams, "src"));
                case "getTotalProfitUntilDate":
                    return getTotalProfitUntilDate(GetPropertyValue(inputParams, "src"));
                case "getTotalOrdersUntilDate":
                    return getTotalOrdersUntilDate(GetPropertyValue(inputParams, "src"));
                case "getDailyRevenue":
                    return getDailyRevenue(GetPropertyValue(inputParams, "src"));
                case "getWeeklyRevenue":
                    return getWeeklyRevenue(GetPropertyValue(inputParams, "src"));
                case "getMonthlyRevenue":
                    return getMonthlyRevenue(GetPropertyValue(inputParams, "src"));
                case "getYearlyRevenue":
                    return getYearlyRevenue();
                case "getDailyProfit":
                    return getDailyProfit(GetPropertyValue(inputParams, "src"));
                case "getWeeklyProfit":
                    return getWeeklyProfit(GetPropertyValue(inputParams, "src"));
                case "getMonthlyProfit":
                    return getMonthlyProfit(GetPropertyValue(inputParams, "src"));
                case "getYearlyProfit":
                    return getYearlyProfit();
                case "getDailyQuantityOfSpecificProduct":
                    return getDailyQuantityOfSpecificProduct(GetPropertyValue(inputParams, "srcProductID"),
                        GetPropertyValue(inputParams, "srcCategoryID"),
                        GetPropertyValue(inputParams, "srcDate"));
                case "getWeeklyQuantityOfSpecificProduct":
                    return getWeeklyQuantityOfSpecificProduct(GetPropertyValue(inputParams, "srcProductID"),
                        GetPropertyValue(inputParams, "srcCategoryID"),
                        GetPropertyValue(inputParams, "srcDate"));
                case "getMonthlyQuantityOfSpecificProduct":
                    return getMonthlyQuantityOfSpecificProduct(GetPropertyValue(inputParams, "srcProductID"),
                        GetPropertyValue(inputParams, "srcCategoryID"),
                        GetPropertyValue(inputParams, "srcDate"));
                case "getYearlyQuantityOfSpecificProduct":
                    return getYearlyQuantityOfSpecificProduct(GetPropertyValue(inputParams, "srcProductID"),
                        GetPropertyValue(inputParams, "srcCategoryID"));
                case "getProductQuantityInCategory":
                    return getProductQuantityInCategory(GetPropertyValue(inputParams, "srcCategoryID"));
                default:
                    return false;
            }
        }
        public override string GetObjectType()
        {
            return "SStatisticsBUS";
        }

        public override SObject Clone()
        {
            return new SStatisticsBUS();
        }
        public string getTotalRevenueUntilDate(DateTime src)
        {
            Debug.WriteLine("SStatisticsBUS.getTotalRevenueUntilDate " + src.ToString());
            return _statisticsDAO.getTotalRevenueUntilDate(src);
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            return _statisticsDAO.getTotalProfitUntilDate(src);
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            return _statisticsDAO.getTotalOrdersUntilDate(src);
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            return _statisticsDAO.getDailyRevenue(src);
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            return _statisticsDAO.getWeeklyRevenue(src);
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            return _statisticsDAO.getMonthlyRevenue(src);
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            return _statisticsDAO.getYearlyRevenue();
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            return _statisticsDAO.getDailyProfit(src);
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            return _statisticsDAO.getWeeklyProfit(src);
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            return _statisticsDAO.getMonthlyProfit(src);
        }

        public List<Tuple<string, decimal>> getYearlyProfit()
        {
            return _statisticsDAO.getYearlyProfit();
        }

        public List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return _statisticsDAO.getDailyQuantityOfSpecificProduct(srcProductID, srcCategoryID, srcDate);
        }

        public List<Tuple<string, int>> getWeeklyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return _statisticsDAO.getWeeklyQuantityOfSpecificProduct(srcProductID, srcCategoryID, srcDate);
        }

        public List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return _statisticsDAO.getMonthlyQuantityOfSpecificProduct(srcProductID, srcCategoryID, srcDate);
        }

        public List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID)
        {
            return _statisticsDAO.getYearlyQuantityOfSpecificProduct(srcProductID, srcCategoryID);
        }

        public List<Tuple<string, int>> getProductQuantityInCategory(int srcCategoryID)
        {
            return _statisticsDAO.getProductQuantityInCategory(srcCategoryID);
        }
    }
}
