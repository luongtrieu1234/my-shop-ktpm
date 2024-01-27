using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ProjectMyShop
{
    public class SOjectManager
    {
        public static Dictionary<int, SObject> Objects = new Dictionary<int, SObject>();
        public static Dictionary<string, SObject> Prototypes = new Dictionary<string, SObject>();
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
        public static object GetAttributeValue(int ID,string attributeName)
        {
            SObject o = FindObjectByID(ID);
            if (o == null) return false;
            object value = o.GetAttributeValue(attributeName);
            return value;
        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, string inputParams)
        {
            SObject o = FindObjectByID(ID);
            if(o == null) return false;
            return o.ExecuteMethod(methodName, inputParams);
        }
        public static int Register(SObject sObject)
        {
            int ID = SObject.NextID++;
            Objects.Add(ID, sObject);
            return ID;
        } 
        public static int CreateObject(string typeName)
        {
            switch (typeName)
            {
                case "BUS":
                    return 1;
                case "DAO":
                    return 2;
            }
            return 0;
        }
    }
}