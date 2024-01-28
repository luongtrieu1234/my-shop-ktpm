using ProjectMyShop.Config;
using System;
using System.Data.SqlClient;

namespace ProjectMyShop.SDAO
{
    public class SDAO : SObject
    {
        public SqlConnection _connection;
        private string? username;
        private string password;

        public SDAO() => ResetConnection();

        public void ResetConnection()
        {
            try
            {
                username = AppConfig.GetValue(AppConfig.Username);
                password = AppConfig.GetValue(AppConfig.Password);

                string? connectionString = AppConfig.ConnectionString(username, password);
                _connection = new SqlConnection(connectionString);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


        }

        /// <summary>
        /// Kiểm tra xem có thể kết nối hay không
        /// </summary>
        /// <returns></returns>
        public bool CanConnect()
        {
            bool result = true;

            try
            {
                _connection.Open();
                _connection.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                result = false;
            }
            return result;
        }

        public void Connect()
        {
            _connection.Open();
        }

        public void Close()
        {
            _connection.Close();
        }
        

    }
}