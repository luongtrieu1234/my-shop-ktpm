using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMyShop.Helpers
{
    public static class DataMappingHelper
    {
        public static List<T> MappingDataTableToObjectList<T>(DataTable dataTable) where T : new()
        {
            List<T> objectList = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();

                foreach (DataColumn col in dataTable.Columns)
                {
                    string columnName = col.ColumnName;
                    object value = row[columnName];

                    var property = typeof(T).GetProperty(columnName);

                    if (property != null && value != DBNull.Value)
                    {
                        // Special handling for BitmapImage property
                        //Debug.WriteLine("type obj " + property.PropertyType.ToString());
                        //Debug.WriteLine("type value " + value.GetType().ToString());
                        if (property.PropertyType == typeof(BitmapImage))
                        {
                            byte[] byteImage = (byte[])value;
                            using (MemoryStream ms = new MemoryStream(byteImage))
                            {
                                var image = new BitmapImage();
                                image.BeginInit();
                                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                                image.CacheOption = BitmapCacheOption.OnLoad;
                                image.UriSource = null;
                                image.StreamSource = ms;
                                image.EndInit();
                                image.Freeze();
                                property.SetValue(obj, image, null);
                            }
                        }
                        else if (property.PropertyType.ToString() != value.GetType().ToString())
                        {
                            property.SetValue(obj, (int)(decimal)value, null);
                        }
                        else
                        {
                            // Normal property assignment
                            property.SetValue(obj, value, null);
                        }
                    }
                }

                objectList.Add(obj);
            }

            return objectList;
        }
    }
}
