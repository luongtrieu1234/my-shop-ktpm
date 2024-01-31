using Newtonsoft.Json.Linq;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMyShop.SBUS
{
    internal class SCategoryBUS : SBUS
    {
        private SCategoryDAO _categoryDAO;

        public SCategoryBUS()
        {
            _categoryDAO = (SCategoryDAO)SOjectManager.Prototypes["SCategoryDAO"];
            if (_categoryDAO.CanConnect())
            {
                _categoryDAO.Connect();
            }
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    return GetObjectType();
                case "Clone":
                    return Clone();
                case "GetByID":
                    return GetByID((int)inputParams.id);
                case "GetAll":
                    return GetAll();
                case "Add":
                    Add(((JObject)inputParams.data).ToObject<Category>());
                    return true;
                case "Remove":
                    Remove((int)inputParams.id); 
                    return true;
                case "Update":
                    Update((int)inputParams.id,((JObject)inputParams.data).ToObject<Category>());
                    return true;
                default:
                    return false;
            }
        }
        public override string GetObjectType()
        {
            return "SCategoryBUS";
        }

        public override SObject Clone()
        {
            return new SCategoryBUS();
        }

        public override Data GetByID(int id)
        {
            Data data= _categoryDAO.GetByID(id);
            return data;
        }

        public override List<Data> GetAll()
        {
            List<Data> datas = _categoryDAO.GetAll();
            return datas;
        }
        public override void Add(Data data)
        {
            Category cat = (Category)data;
            int ID = _categoryDAO.isExisted(cat);
            if (ID > 0)
            {
                // existed category
                cat.ID = ID;
            }
            else
            {
                // add new category
                _categoryDAO.Add(cat);
                cat.ID = _categoryDAO.GetLastestInsertID();
            }
        }

        public override void Remove(int id)
        {
            _categoryDAO.Remove(id);
        }

        public override void Update(int id, Data data)
        {
            _categoryDAO.Update(id, data);
        }
    }
}
