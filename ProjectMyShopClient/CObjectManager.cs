﻿using Microsoft.Graph;
using ProjectMyShopClient.CBUS;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using ProjectMyShop;

namespace ProjectMyShopClient
{
    public class CObjectManager
    {
        public static Dictionary<int, CObject> Objects = new Dictionary<int, CObject>();
        public static Dictionary<string, CObject> Prototypes = new Dictionary<string, CObject>();

        public static bool SetAttributeValue(int ID, string attributeName, object attributeValue)
        {
            return SOjectManager.SetAttributeValue(ID, attributeName, attributeValue);
        }
        public static object GetAttributeValue(int ID, string attributeName)
        {
            return SOjectManager.GetAttributeValue(ID, attributeName);
        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, dynamic inputParams)
        {
            return SOjectManager.ExecuteRemoteMethod(ID, methodName, inputParams);
        }
        public static int CreateRemoteObject(string typeName)
        {
            return SOjectManager.CreateRemoteObject(typeName);
        }
    }
}