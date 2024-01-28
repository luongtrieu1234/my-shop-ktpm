using ProjectMyShopClient.SBUS;
using ProjectMyShopClient.SDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ProjectMyShopClient
{
    public class SOjectManager
    {
        public static Dictionary<int, SObject> Objects = new Dictionary<int, SObject>();
        public static Dictionary<string, SObject> Prototypes = new Dictionary<string, SObject>();
        static SOjectManager()
        {
            InitPrototypes();
        }


        private static void InitPrototypes()
        {
            Prototypes.Add("SAccountDAO", new SAccountDAO());
            Prototypes.Add("SCategoryDAO", new SCategoryDAO());
            Prototypes.Add("SOrderDAO", new SOrderDAO());
            Prototypes.Add("SProductDAO", new SProductDAO());
            Prototypes.Add("SStatisticsDAO", new SStatisticsDAO());
            Prototypes.Add("SAccountBUS", new SAccountBUS());
            Prototypes.Add("SCategoryBUS", new SCategoryBUS());
            Prototypes.Add("SOrderBUS", new SOrderBUS());
            Prototypes.Add("SProductBUS", new SProductBUS());
            Prototypes.Add("SStatisticsBUS", new SStatisticsBUS());
        }

        public static SObject FindObjectByID(int ID)
        {
            if (Objects.ContainsKey(ID))
            {
                return Objects[ID];
            }
            return null;
        }
        public static bool SetAttributeValue(int ID, string attributeName, object attributeValue)
        {
            SObject o = FindObjectByID(ID);
            if (o == null) return false;
            return o.SetAttributeValue(attributeName, attributeValue);
        }
        public static object GetAttributeValue(int ID, string attributeName)
        {
            SObject o = FindObjectByID(ID);
            if (o == null) return false;
            object value = o.GetAttributeValue(attributeName);
            return value;
        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, string inputParams)
        {
            SObject o = FindObjectByID(ID);
            if (o == null) return false;
            return o.ExecuteMethod(methodName, inputParams);
        }
        public static int Register(SObject sObject)
        {
            int ID = SObject.NextID++;
            Objects.Add(ID, sObject);
            return ID;
        }
        public static int CreateRemoteObject(string typeName)
        {
            switch (typeName)
            {
                case "SAccountDAO":
                    return new SAccountDAO().ID;
                case "SCategoryDAO":
                    return new SCategoryDAO().ID;
                case "SOrderDAO":
                    return new SOrderDAO().ID;
                case "SProductDAO":
                    return new SProductDAO().ID;
                case "SStatisticsDAO":
                    return new SStatisticsDAO().ID;
                case "SAccountBUS":
                    return new SAccountBUS().ID;
                case "SCategoryBUS":
                    return new SCategoryBUS().ID;
                case "SOrderBUS":
                    return new SOrderBUS().ID;
                case "SProductBUS":
                    return new SProductBUS().ID;
                case "SStatisticsBUS":
                    return new SStatisticsBUS().ID;


            }
            return 0;
        }
    }
}