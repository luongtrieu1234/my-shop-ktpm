using ProjectMyShopClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectMyShopClient
{
    public class SObject
    {
        public Dictionary<string, object> Attributes = new Dictionary<string, object>();
        public static int NextID = 1;
        public int ID { get; set; }

        public SObject()
        {
            ID = SOjectManager.Register(this);
        }

        public bool SetAttributeValue(string attributeName, object attributeValue)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                Attributes[attributeName] = attributeValue;
            }
            else
            {
                Attributes.Add(attributeName, attributeValue);
            }
            return true;
        }
        public object GetAttributeValue(string attributeName)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                return Attributes[attributeName];
            }
            else
            {
                return (object)String.Empty;
            }
        }

        public object this[string attributeName]
        {
            get
            {
                return GetAttributeValue(attributeName);
            }
            set
            {
                Attributes[attributeName] = value;
            }
        }
        public virtual string GetObjectType()
        {
            return String.Empty;
        }

        public virtual SObject Clone()
        {
            return new SObject();
        }

        public virtual dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            return false;
        }

        public virtual Data GetByID(int id)
        {
            return new Data();
        }
        public virtual List<Data> GetAll()
        {
            return new List<Data>();
        }
        public virtual List<Data> GetObjects(int offset, int size)
        {
            return new List<Data>();
        }
        public virtual void Add(Data data)
        {
        }
        public virtual void Remove(int ID)
        {

        }
        public virtual void Update(int ID, Data data)
        {

        }
        public static List<T> ConvertData<T>(List<Data> dataList) where T : Data
        {
            List<T> list = dataList.ConvertAll(item => (T)item);
            return list;
        }

    }
}