using ProjectMyShopClient.SDAO;
using System;
using System.Collections.Generic;

namespace ProjectMyShopClient.SBUS
{
    internal class SStatisticsBUS : SBUS
    {
        private SStatisticsDAO _statisticsDAO;

        public SStatisticsBUS()
        {
            _statisticsDAO = new SStatisticsDAO();
            if (_statisticsDAO.CanConnect())
            {
                _statisticsDAO.Connect();
            }
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    return GetObjectType();
                case "Clone":
                    return Clone();
                case "getTotalRevenueUntilDate":
                    return getTotalRevenueUntilDate(inputParams.src);
                case "getTotalProfitUntilDate":
                    return getTotalProfitUntilDate(inputParams.src);
                case "getTotalOrdersUntilDate":
                    return getTotalOrdersUntilDate(inputParams.src);
                case "getDailyRevenue":
                    return getDailyRevenue(inputParams.src);
                case "getWeeklyRevenue":
                    return getWeeklyRevenue(inputParams.src);
                case "getMonthlyRevenue":
                    return getMonthlyRevenue(inputParams.src);
                case "getYearlyRevenue":
                    return getYearlyRevenue();
                case "getDailyProfit":
                    return getDailyProfit(inputParams.src);
                case "getWeeklyProfit":
                    return getWeeklyProfit(inputParams.src);
                case "getMonthlyProfit":
                    return getMonthlyProfit(inputParams.src);
                case "getYearlyProfit":
                    return getYearlyProfit();
                case "getDailyQuantityOfSpecificProduct":
                    return getDailyQuantityOfSpecificProduct(inputParams.srcProductID, inputParams.srcCategoryID, inputParams.srcDate);
                case "getWeeklyQuantityOfSpecificProduct":
                    return getWeeklyQuantityOfSpecificProduct(inputParams.srcProductID, inputParams.srcCategoryID, inputParams.srcDate);
                case "getMonthlyQuantityOfSpecificProduct":
                    return getMonthlyQuantityOfSpecificProduct(inputParams.srcProductID, inputParams.srcCategoryID, inputParams.srcDate);
                case "getYearlyQuantityOfSpecificProduct":
                    return getYearlyQuantityOfSpecificProduct(inputParams.srcProductID, inputParams.srcCategoryID);
                case "getProductQuantityInCategory":
                    return getProductQuantityInCategory(inputParams.srcCategoryID);
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
