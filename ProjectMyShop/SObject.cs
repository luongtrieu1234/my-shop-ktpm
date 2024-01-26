using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectMyShop
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

        public virtual bool ExecuteMethod(string methodName, string inputParams, ref string outputParams)
        {
            outputParams = String.Empty;
            return false;
        }

        public virtual Data GetByID()
        {
            return new Data();
        }
        public virtual Data GetAll()
        {
            return new Data();
        }
        public virtual Data GetObjects(int offset, int size)
        {
            return new Data();
        }
        public virtual Data Add()
        {
            return new Data();
        }
        public virtual Data Remove()
        {
            return new Data();
        }
        public virtual Data Update()
        {
            return new Data();
        }

    }
}