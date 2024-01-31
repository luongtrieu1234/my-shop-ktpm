using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.SDAO
{
    public class SProductDAO : SDAO
    {
        public override string GetObjectType()
        {
            return "SProductDAO";
        }
        public override SObject Clone()
        {
            return new SProductDAO();
        }
        public override Data GetByID(int productID)
        {
            var sql = "select * from Product WHERE ID = @productID";
            var command = new SqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@productID", productID);

            var reader = command.ExecuteReader();

            Product? product = null;
            
            var dataTable = new DataTable();
            dataTable.Load(reader);

            var productList = DataMappingHelper.MappingDataTableToObjectList<Product>(dataTable);
            product = productList.FirstOrDefault();
            reader.Close();
            return product;
        }
        public override void Add(Data data)
        {
            Product product = (Product)data;
            // ID Auto Increment
            var sql = "";
            if (product.Avatar != null)
            {
                sql = "insert into Product(ProductName, Manufacturer, BoughtPrice, SoldPrice, Stock, UploadDate, Description, CatID, Avatar) " +
                    "values (@ProductName, @Manufacturer, @BoughtPrice, @SoldPrice, @Stock, @UploadDate, @Description, @CatID, @Avatar)"; //
            }
            else
            {
                sql = "insert into Product(ProductName, Manufacturer, BoughtPrice, SoldPrice, Stock, UploadDate, Description, CatID) " +
                    "values (@ProductName, @Manufacturer, @BoughtPrice, @SoldPrice, @Stock, @UploadDate, @Description, @CatID)"; //
            }
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);

            sqlCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
            sqlCommand.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
            sqlCommand.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice);
            sqlCommand.Parameters.AddWithValue("@SoldPrice", product.SoldPrice);
            sqlCommand.Parameters.AddWithValue("@Stock", product.Stock);
            sqlCommand.Parameters.AddWithValue("@UploadDate", product.UploadDate);
            sqlCommand.Parameters.AddWithValue("@Description", product.Description);
            sqlCommand.Parameters.AddWithValue("@CatID", product.Category.ID);
            if(product.Avatar != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(product.Avatar));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    sqlCommand.Parameters.AddWithValue("@Avatar", stream.ToArray());
                }
            }

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Inserted {product.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Inserted {product.ID} Fail: " + ex.Message);
            }
        }
        public override void Update(int id, Data data)
        {
            Product product = (Product)data;
            string sql;
            if (product.Avatar != null)
            {
                sql = "update Product set ProductName = @ProductName, Manufacturer = @Manufacturer, Description = @Description, " +
                "BoughtPrice = @BoughtPrice, Stock = @Stock, SoldPrice = @SoldPrice, Avatar = @Avatar where ID = @ID";
            }
            else
            {
                sql = "update Product set ProductName = @ProductName, Manufacturer = @Manufacturer, Description = @Description, " +
                "BoughtPrice = @BoughtPrice, Stock = @Stock, SoldPrice = @SoldPrice where ID = @ID";
            }
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            sqlCommand.Parameters.AddWithValue("@ID", id);
            sqlCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
            sqlCommand.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
            sqlCommand.Parameters.AddWithValue("@BoughtPrice", product.BoughtPrice);
            sqlCommand.Parameters.AddWithValue("@SoldPrice", product.SoldPrice);
            sqlCommand.Parameters.AddWithValue("@Stock", product.Stock);
            sqlCommand.Parameters.AddWithValue("@Description", product.Description);

            if (product.Avatar != null)
            {
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(product.Avatar));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    sqlCommand.Parameters.AddWithValue("@Avatar", stream.ToArray());
                }
            }

            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Updated {product.ID} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Updated {product.ID} Fail: " + ex.Message);
            }
        }
        public override void Remove(int productid)
        {
            string sql = "delete from Product where ID = @ID";
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            sqlCommand.Parameters.AddWithValue("@ID", productid);
            try
            {
                sqlCommand.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine($"Deleted {productid} OK");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deleted {productid} Fail: " + ex.Message);
            }
        }
        public int getTotalProduct()
        {
            var sql = "select count(*) as total from Product";
            var command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();

            int result = 0;
            if (reader.Read())
            {
                result = (int)reader["total"];
            }
            reader.Close();
            return result;
        }
        public List<Product> GetTop5OutStock()
        {
            var sql = "select top(5) * from Product where stock < 5 order by stock ";
            var command = new SqlCommand(sql, _connection);
            var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            var productList = DataMappingHelper.MappingDataTableToObjectList<Product>(dataTable);

            var filteredList = productList.Where(p => !string.IsNullOrEmpty(p.ProductName)).ToList();

            reader.Close();
            return filteredList;
        }
        public List<Product> getProductsAccordingToSpecificCategory(int srcCategoryID)
        {
            var sql = "select * from Product where CatID = @CategoryID";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@CategoryID";
            sqlParameter.Value = srcCategoryID;

            var command = new SqlCommand(sql, _connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            var productList = DataMappingHelper.MappingDataTableToObjectList<Product>(dataTable);

            var filteredList = productList.Where(p => !string.IsNullOrEmpty(p.ProductName)).ToList();

            reader.Close();
            return filteredList;
        }
        public int GetLastestInsertID()
        {
            string sql = "select ident_current('Product')";
            SqlCommand sqlCommand = new SqlCommand(sql, _connection);
            var resutl = sqlCommand.ExecuteScalar();
            System.Diagnostics.Debug.WriteLine(resutl);
            return System.Convert.ToInt32(sqlCommand.ExecuteScalar());
        }
        public List<BestSellingProduct> getBestSellingProductsInWeek(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar, sum(do.Quantity) as Quantity from Orders o join DetailOrder do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where OrderDate between DATEADD(DAY, -7, @SelectedDate) and DATEADD(DAY, 1, @SelectedDate) group by p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, _connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            // Use the helper to convert DataTable to List<BestSellingProduct>
            var productList = DataMappingHelper.MappingDataTableToObjectList<BestSellingProduct>(dataTable);

            // Filter out products with an empty product name
            var filteredList = productList.Where(p => !string.IsNullOrEmpty(p.ProductName)).ToList();

            reader.Close();
            return filteredList;
        }
        public List<BestSellingProduct> getBestSellingProductsInMonth(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar, sum(do.Quantity) as Quantity from Orders o join DetailOrder do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, OrderDate) = datepart(year, @SelectedDate) and datepart(month, OrderDate) = datepart(month, @SelectedDate) group by p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, _connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            // Use the helper to convert DataTable to List<BestSellingProduct>
            var productList = DataMappingHelper.MappingDataTableToObjectList<BestSellingProduct>(dataTable);

            // Filter out products with an empty product name
            var filteredList = productList.Where(p => !string.IsNullOrEmpty(p.ProductName)).ToList();

            reader.Close();
            return filteredList;
        }
        public List<BestSellingProduct> getBestSellingProductsInYear(DateTime src)
        {
            string sqlFormattedDate = src.ToString("yyyy-MM-dd");

            var sql = "select TOP(5) p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar, sum(do.Quantity) as Quantity from Orders o join DetailOrder do on o.ID = do.OrderID join Product p on p.ID = do.ProductID where datepart(year, OrderDate) = datepart(year, @SelectedDate) group by p.ID, p.ProductName, p.Manufacturer, p.Stock, p.Description, p.BoughtPrice, p.SoldPrice, p.CatID, p.UploadDate, p.Avatar order by sum(do.Quantity) desc;";

            var sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@SelectedDate";
            sqlParameter.Value = sqlFormattedDate;

            var command = new SqlCommand(sql, _connection);
            command.Parameters.Add(sqlParameter);

            var reader = command.ExecuteReader();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            var productList = DataMappingHelper.MappingDataTableToObjectList<BestSellingProduct>(dataTable);

            var filteredList = productList.Where(p => !string.IsNullOrEmpty(p.ProductName)).ToList();

            reader.Close();
            return filteredList;
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    Debug.WriteLine("GetObjectType Product called");
                    return GetObjectType();
                case "Clone":
                    Debug.WriteLine("Clone Product called");
                    return Clone();
                case "GetByID":
                    Debug.WriteLine("GetByID Product called");
                    return GetByID(inputParams.productID);
                case "Add":
                    Debug.WriteLine("Add Product called");
                    Add(inputParams.data);
                    return true;
                case "Update":
                    Debug.WriteLine("Update Product called");
                    Update(inputParams.id, inputParams.data);
                    return true;
                case "Remove":
                    Debug.WriteLine("Remove Product called");
                    Remove(inputParams.productid);
                    return true;
                case "GetObjects":
                    Debug.WriteLine("GetObjects Product called");
                    return GetObjects(inputParams.offset, inputParams.size);
                case "getTotalProduct":
                    Debug.WriteLine("getTotalProduct Product called");
                    return getTotalProduct();
                case "GetTop5OutStock":
                    Debug.WriteLine("GetTop5OutStock Product called");
                    return GetTop5OutStock();
                case "getProductsAccordingToSpecificCategory":
                    Debug.WriteLine("getProductsAccordingToSpecificCategory Product called");
                    return getProductsAccordingToSpecificCategory(inputParams.srcCategoryID);
                case "GetLastestInsertID":
                    Debug.WriteLine("GetLastestInsertID Product called");
                    return GetLastestInsertID();
                case "getBestSellingProductsInWeek":
                    Debug.WriteLine("getBestSellingProductsInWeek Product called");
                    return getBestSellingProductsInWeek(inputParams.src);
                case "getBestSellingProductsInMonth":
                    Debug.WriteLine("getBestSellingProductsInMonth Product called");
                    return getBestSellingProductsInMonth(inputParams.src);
                case "getBestSellingProductsInYear":
                    Debug.WriteLine("getBestSellingProductsInYear Product called");
                    return getBestSellingProductsInYear(inputParams.src);
            }
            return false;
        }
    }
}
