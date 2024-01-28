using ProjectMyShop.Config;
using ProjectMyShop.DAO;
using ProjectMyShop.DTO;
using ProjectMyShop.SDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMyShop.SBUS
{
    internal class SAccountBUS:SBUS
    {
        private SAccountDAO _accountDAO;

        public SAccountBUS()
        {
            _accountDAO = new SAccountDAO();
        }
        public override dynamic ExecuteMethod(string methodName, dynamic inputParams)
        {
            switch (methodName)
            {
                case "GetObjectType":
                    return GetObjectType();
                case "Clone":
                    return Clone();
                case "validate":
                    return validate(inputParams.username, inputParams.password);
                default:
                    return false;
            }
        }
        public override string GetObjectType()
        {
            return "SAccountBUS";
        }

        public override SObject Clone()
        {
            return new SAccountBUS();
        }
        
        public bool validate(string username, string password)
        {
            // save username & password to config file
            AppConfig.SetValue(AppConfig.Username, username);
            AppConfig.SetPassword(password);

            _accountDAO.ResetConnection();

            return _accountDAO.CanConnect();
        }
    }
}
