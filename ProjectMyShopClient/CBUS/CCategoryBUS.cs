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
            dynamic cat = this.ExecuteMethod("GetByID", new {id});
            Category rs = (Category)cat;
            return rs;
        }

        public List<Category> GetAll()
        {
            dynamic datas = this.ExecuteMethod("GetAll", null);
            List<Category> rs = ConvertJArrayToList<Category>(datas);
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
