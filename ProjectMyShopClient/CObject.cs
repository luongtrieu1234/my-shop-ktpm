using Microsoft.Graph;
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

        protected int ID;

        //public CObject()
        //{
        //    ID = COjectManager.Register(this);
        //}

        public bool SetAttributeValue(string attributeName, object attributeValue)
        {
            return CObjectManager.SetAttributeValue(ID, attributeName, attributeValue);
        }
        public object GetAttributeValue(string attributeName)
        {
            return CObjectManager.GetAttributeValue(ID, attributeName);
        }

        public object this[string attributeName]
        {
            get
            {
                return GetAttributeValue(attributeName);
            }
            set
            {
                SetAttributeValue(attributeName, value);
            }
        }

        public dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            return CObjectManager.ExecuteRemoteMethod(ID, methodName,
                inputParams);
        }
        //public virtual string GetObjectType()
        //{
        //    return String.Empty;
        //}

        //public virtual CObject Clone()
        //{
        //    return new CObject();
        //}

        //public virtual dynamic ExecuteMethod(string methodName, dynamic inputParams)
        //{
        //    return false;
        //}

        //public virtual Data GetByID(int id)
        //{
        //    return new Data();
        //}
        //public virtual List<Data> GetAll()
        //{
        //    return new List<Data>();
        //}
        //public virtual List<Data> GetObjects(int offset, int size)
        //{
        //    return new List<Data>();
        //}
        //public virtual void Add(Data data)
        //{
        //}
        //public virtual void Remove(int ID)
        //{

        //}
        //public virtual void Update(int ID, Data data)
        //{

        //}
        public static List<T> ConvertData<T>(List<Data> dataList) where T : Data
        {
            List<T> list = dataList.ConvertAll(item => (T)item);
            return list;
        }

    }
}