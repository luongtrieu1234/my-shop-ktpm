using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.SBUS
{
    internal class SCategoryBUS : SBUS
    {
        private SCategoryDAO _categoryDAO;

        public SCategoryBUS()
        {
            _categoryDAO = new SCategoryDAO();
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
                    return GetByID(inputParams.id);
                case "GetAll":
                    return GetAll();
                case "Add":
                    Add(inputParams.data);
                    return true;
                case "Remove":
                    Remove(inputParams.id); 
                    return true;
                case "Update":
                    Update(inputParams.id, inputParams.data);
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
            Category result = _categoryDAO.ExecuteMethod("GetByID", new { ID = id });
            return result;
        }

        public override List<Data> GetAll()
        {
            Debug.WriteLine("getCategoryList:");
            List<Data> datas = _categoryDAO.ExecuteMethod("GetAll", null);
            List<Category> result = new List<Category>();
            foreach (Data data in datas)
            {
                result.Add((Category)data);
            }
            return new List<Data>(result);
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
            Category category = (Category)data;
            _categoryDAO.ExecuteMethod("Update", new { ID = id, data = category });
        }
    }
}
