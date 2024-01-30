using ProjectMyShopClient.DTO;
using System.Collections.Generic;

namespace ProjectMyShopClient.CBUS
{
    internal class CCategoryBUS : CBUS
    {

        public CCategoryBUS()
        {
            ID = CObjectManager.CreateRemoteObject("SCategoryBUS");
        }
        public string GetObjectType()
        {
            return "CCategoryBUS";
        }

        public CObject Clone()
        {
            return new CCategoryBUS();
        }

        public Category GetByID(int id)
        {
            Data rs = this.ExecuteMethod("GetByID", new {id});
            Category cat = (Category)rs;
            return cat;
        }

        public List<Category> GetAll()
        {
            List<Data> datas=  this.ExecuteMethod("GetAll", null);
            List<Category> rs = CObject.ConvertData<Category>(datas);
            return rs;
        }
        public void Add(Category cat)
        {
            this.ExecuteMethod("Add", new {data=cat});
        }

        public void Remove(int id)
        {
            this.ExecuteMethod("Remove", new {id});
        }

        public void Update(int id, Category cat)
        {
            this.ExecuteMethod("Update", new { id, data= cat });
        }
    }
}
