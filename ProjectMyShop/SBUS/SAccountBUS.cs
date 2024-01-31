using ProjectMyShop.Config;
using ProjectMyShop.SDAO;

namespace ProjectMyShop.SBUS
{
    internal class SAccountBUS:SBUS
    {
        private SAccountDAO _accountDAO;

        public SAccountBUS()
        {
            _accountDAO = (SAccountDAO)SOjectManager.Prototypes["SAccountDAO"];
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
                    return validate((string)inputParams.username,(string) inputParams.password);
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
