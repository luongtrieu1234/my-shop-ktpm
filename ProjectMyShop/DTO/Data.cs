using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.DTO
{
    public class Data:ICloneable
    {
        public static Dictionary<string, object> Attributes = new Dictionary<string, object>();
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
                return string.Empty;
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
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
