using ProjectMyShopClient.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShopClient.CBUS
{
    internal class CCategoryBUS : CBUS
    {
        private SCategoryDAO _categoryDAO;

        public CCategoryBUS()
        {
            this.ID = CObjectManager.CreateRemoteObject("SCategoryBUS");
        }
        public string GetObjectType()
        {
            return "CCategoryBUS";
        }

        public CObject Clone()
        {
            return new CCategoryBUS();
        }

        public Data GetByID(int id)
        {
            //Category result = _categoryDAO.ExecuteMethod("GetByID", new { ID = id });
            //return result;
            return this.ExecuteMethod("GetByID", id);
        }

        public List<Data> GetAll()
        {
            return this.ExecuteMethod("GetAll", null);
        }
        public void Add(Data data)
        {
            this.ExecuteMethod("Add", data);
        }

        public void Remove(int id)
        {
            this.ExecuteMethod("Remove", id);
        }

        public void Update(int id, Data data)
        {
            this.ExecuteMethod("Update", new { ID = id, Data = data });
        }
    }
}
