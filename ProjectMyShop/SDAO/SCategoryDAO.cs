using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.SDAO
{
    public class SCategoryDAO : SDAO
    {
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    return GetObjectType();
                case "Clone":
                    return Clone();
                case "GetByID":
                    return GetByID(inputParams.ID);
                case "GetAll":
                    return GetAll();
                case "Add":
                    return Add(inputParams);
                case "Update":
                    Update(inputParams.ID, inputParams.data);
                    return true;
                case "Remove":
                    Remove(inputParams.ID);
                    return true;
                case "GetLastestInsertID":
                    return GetLastestInsertID();
                case "isExisted":
                    return isExisted(inputParams.isExisted);
                default:
                    return false;
            }
        }
        public override string GetObjectType()
        {
            return "SCategoryDAO";
        }

        public override SObject Clone()
        {
            return new SCategoryDAO();
        }

        public override Data GetByID(int id)
        {
            var sql = "select * from Category where ID=@CatId";
            var command = new SqlCommand(sql, _connection);

            command.Parameters.Add("CatId", SqlDbType.Int).Value = id;

            var reader = command.ExecuteReader();

            Category? result = null;

            var dataTable = new DataTable();
            dataTable.Load(reader);
            var resultList = DataMappingHelper.MappingDataTableToObjectList<Category>(dataTable);

            result = resultList.FirstOrDefault();

            reader.Close();
            return result;
        }

        public override List<Data> GetAll()
        {
            var sql = "select * from Category;";

            var command = new SqlCommand(sql, _connection);

            var reader = command.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            var resultList = DataMappingHelper.MappingDataTableToObjectList<Category>(dataTable);

            return new List<Data>(resultList);
        }

        public override void Add(Data data)
        {
            Category cat = (Category)data;
            var sql = "";
            if(cat.Avatar != null)
            { 
                sql = "insert into Category(CatName, Avatar) " +
                    "values (@CatName, @Avatar)"; //
            }
            else
            {
                sql = "insert into Category(CatName) " +
                    "values (@CatName)";
            }
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);

            sqlCommand.Parameters.AddWithValue("@CatName", cat.CatName);

            if (cat.Avatar != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(cat.Avatar));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    sqlCommand.Parameters.AddWithValue("@Avatar", stream.ToArray());
                }
            }
            
            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {cat.ID} OK");
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Inserted {cat.ID} Fail: " + ex.Message);
            }

        }

        public override void Update(int ID, Data data)
        {
            Category category = (Category)data;
            string sql;
            if (category.Avatar != null)
            {
                sql = "update Category set CatName = @CatName, Avatar = @Avatar where ID = @ID";
            }
            else
            {
                sql = "update Category set CatName = @CatName where ID = @ID";
            }
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            sqlCommand.Parameters.AddWithValue("@CatName", category.CatName);


            if (category.Avatar != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(category.Avatar));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    sqlCommand.Parameters.AddWithValue("@Avatar", stream.ToArray());
                }
            }

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {category.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {category.ID} Fail: " + ex.Message);
            }
        }

        public override void Remove(int ID)
        {
            string sql = "delete from Category where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            sqlCommand.Parameters.AddWithValue("@ID", ID);
            try
            {
                sqlCommand.ExecuteNonQuery();
                sql = "delete from Product where CatID = @ID";
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.ExecuteNonQuery();

                System.Diagnostics.Debug.WriteLine($"Deleted {ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {ID} Fail: " + ex.Message);
            }
        }
        
        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Category')";
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            var resutl = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(resutl);
            return System.Convert.ToInt32(sqlCommand.ExecuteScalar());
        }
       
        public int isExisted(Category cat)
        {
            string sql = "select ID from Category where CatName = @CatName";
            SqlCommand command = new SqlCommand(sql, _connection);
            command.Parameters.AddWithValue("@CatName", cat.CatName);

            var reader = command.ExecuteReader();
            int ID = 0;
            while (reader.Read())
            {
                ID = (int)reader["ID"];
            }
            reader.Close();
            return ID;
        }


    }
}
