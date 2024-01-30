﻿using Microsoft.Graph;
using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ProjectMyShopClient
{
    public class CObject
    {
        //public Dictionary<string, object> Attributes = new Dictionary<string, object>();
        //public static int NextID = 1;
        //public int ID { get; set; }

        public int ID { get; set; }

        public dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            return CObjectManager.ExecuteRemoteMethod(ID, methodName,
                inputParams);
        }
        
        public static List<T> ConvertData<T>(List<Data> dataList) where T : Data
        {
            List<T> list = dataList.ConvertAll(item => (T)item);
            return list;
        }

    }
}