using ProjectMyShopClient.Config;
using ProjectMyShop.SDAO;

namespace ProjectMyShopClient.CBUS
{
    internal class CAccountBUS : CBUS
    {
        private SAccountDAO _accountDAO;

        public CAccountBUS()
        {
            _accountDAO = new SAccountDAO();
        }
        public string GetObjectType()
        {
            return "CAccountBUS";
        }

        public CObject Clone()
        {
            return new CAccountBUS();
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
