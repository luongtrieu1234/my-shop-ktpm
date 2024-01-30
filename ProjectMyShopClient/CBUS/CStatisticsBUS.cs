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
            return this.ExecuteMethod("getTotalRevenueUntilDate", new { src = src });
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            return this.ExecuteMethod("getTotalProfitUntilDate", new { src= src });
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            return this.ExecuteMethod("getTotalOrdersUntilDate", new { src= src });
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getDailyRevenue", new { src= src });
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getWeeklyRevenue", new { src= src });
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getMonthlyRevenue", new { src= src });
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            return this.ExecuteMethod("getYearlyRevenue", null);
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            return this.ExecuteMethod("getDailyProfit", new { src= src });
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            return this.ExecuteMethod("getWeeklyProfit", new { src= src });
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            return this.ExecuteMethod("getMonthlyProfit", new { src= src });
        }

        public List<Tuple<string, decimal>> getYearlyProfit()
        {
            return this.ExecuteMethod("getYearlyProfit", null);
        }

        public List<Tuple<string, int>> getDailyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return this.ExecuteMethod("getDailyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
        }

        public List<Tuple<string, int>> getWeeklyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return this.ExecuteMethod("getWeeklyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
        }

        public List<Tuple<string, int>> getMonthlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID, DateTime srcDate)
        {
            return this.ExecuteMethod("getMonthlyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID, srcDate });
        }

        public List<Tuple<string, int>> getYearlyQuantityOfSpecificProduct(int srcProductID, int srcCategoryID)
        {
            return this.ExecuteMethod("getYearlyQuantityOfSpecificProduct", new { srcProductID, srcCategoryID });
        }

        public List<Tuple<string, int>> getProductQuantityInCategory(int srcCategoryID)
        {
            return this.ExecuteMethod("getProductQuantityInCategory", new { srcCategoryID });
        }
    }
}
