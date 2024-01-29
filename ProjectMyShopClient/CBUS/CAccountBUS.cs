namespace ProjectMyShopClient.CBUS
{
    internal class CAccountBUS : CBUS
    {

        public CAccountBUS()
        {
           this.ID = CObjectManager.CreateRemoteObject("SAccountBUS");
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
            return true;
        }
    }
}
