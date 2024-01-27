﻿using ProjectMyShop.Config;
using ProjectMyShop.DTO;
using ProjectMyShop.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
                password = AppConfig.GetPassword();

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