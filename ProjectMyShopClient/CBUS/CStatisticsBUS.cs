using System;
using System.Collections.Generic;
using ProjectMyShop.SDAO;
using ProjectMyShopClient;
using ProjectMyShopClient.DTO;

namespace ProjectMyShopClient.CBUS
{
    internal class CStatisticsBUS : CBUS
    {
        private SStatisticsDAO _statisticsDAO;

        public CStatisticsBUS()
        {
            //_statisticsDAO = new SStatisticsDAO();
            //if (_statisticsDAO.CanConnect())
            //{
            //    _statisticsDAO.Connect();
            //}
            this.ID = CObjectManager.CreateRemoteObject("SStatisticsBUS");
        }
        public string GetObjectType()
        {
            return "CStatisticsBUS";
        }
        public string getTotalRevenueUntilDate(DateTime src)
        {
            return this.ExecuteMethod("getTotalRevenueUntilDate", new { src });
        }

        public string getTotalProfitUntilDate(DateTime src)
        {
            return this.ExecuteMethod("getTotalProfitUntilDate", new { src });
        }

        public int getTotalOrdersUntilDate(DateTime src)
        {
            return this.ExecuteMethod("getTotalOrdersUntilDate", new { src });
        }

        public List<Tuple<string, decimal>> getDailyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getDailyRevenue", new { src });
        }

        public List<Tuple<string, decimal>> getWeeklyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getWeeklyRevenue", new { src });
        }

        public List<Tuple<string, decimal>> getMonthlyRevenue(DateTime src)
        {
            return this.ExecuteMethod("getMonthlyRevenue", new { src });
        }

        public List<Tuple<string, decimal>> getYearlyRevenue()
        {
            return this.ExecuteMethod("getYearlyRevenue", null);
        }

        public List<Tuple<string, decimal>> getDailyProfit(DateTime src)
        {
            return this.ExecuteMethod("getDailyProfit", new { src });
        }

        public List<Tuple<string, decimal>> getWeeklyProfit(DateTime src)
        {
            return this.ExecuteMethod("getWeeklyProfit", new { src });
        }

        public List<Tuple<string, decimal>> getMonthlyProfit(DateTime src)
        {
            return this.ExecuteMethod("getMonthlyProfit", new { src });
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
