using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.SBUS
{
    internal class SCategoryBUS
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

        public Category GetCategoryById(int id)
        {
            Category result = (Category)_categoryDAO.GetByID(id);

            return result;
        }

        public List<Category> getCategoryList()
        {
            Console.WriteLine("getCategoryList:");
            List<Data> datas = _categoryDAO.GetAll();
            List<Category> result = new List<Category>();
            foreach (Data data in datas)
            {
                result.Add((Category)data);
            }
            return result;
        }
        public void AddCategory(Category cat)
        {
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

        public void removeCategory(Category cat)
        {
            _categoryDAO.Remove(cat.ID);
        }

        public void updateCategory(int ID, Category category)
        {

            _categoryDAO.Update(ID, category);

        }
    }
}
