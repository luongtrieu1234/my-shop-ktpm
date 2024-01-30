using System;
using System.Collections.Generic;

namespace ProjectMyShopClient.CBUS
{
    internal class CStatisticsBUS : CBUS
    {
        public CStatisticsBUS()
        {
            ID = CObjectManager.CreateRemoteObject("SStatisticsBUS");
        }
        public string GetObjectType()
        {
            return "CStatisticsBUS";
        }
        public string getTotalRevenueUntilDate(DateTime src)
        {
            //dynamic data = this.ExecuteMethod("getTotalRevenueUntilDate", new { src = src });
            //string rs = (string)data;
            //return rs;
            return "0";
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            //dynamic data = this.ExecuteMethod("getTotalProfitUntilDate", new { src = src });
            //string rs = (string)data;
            //return rs;
            return "0";
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            //dynamic data = this.ExecuteMethod("getTotalOrdersUntilDate", new { src = src });
            //int rs = (int)data;
            //return rs;
            return 0;
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            //dynamic data =  this.ExecuteMethod("getDailyRevenue", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            //return this.ExecuteMethod("getWeeklyRevenue", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            //return this.ExecuteMethod("getMonthlyRevenue", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            //return this.ExecuteMethod("getYearlyRevenue", null);
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            //return this.ExecuteMethod("getDailyProfit", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            //return this.ExecuteMethod("getWeeklyProfit", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            //return this.ExecuteMethod("getMonthlyProfit", new { src = src });
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, decimal>> getYearlyProfit()
        {
            //return this.ExecuteMethod("getYearlyProfit", null);
            return new List<Tuple<string, decimal>>();
        }

        public List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            //return this.ExecuteMethod("getDailyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
            return new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> getWeeklyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            //return this.ExecuteMethod("getWeeklyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
            return new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            //return this.ExecuteMethod("getMonthlyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
            return new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID)
        {
            //return this.ExecuteMethod("getYearlyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID });
            return new List<Tuple<string, int>>();
        }

        public List<Tuple<string, int>> getProductQuantityInCategory(int srcCategoryID)
        {
            //return this.ExecuteMethod("getProductQuantityInCategory", new { srcCategoryID });
            return new List<Tuple<string, int>>();
        }
    }
}
