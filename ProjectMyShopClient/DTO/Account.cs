using System.ComponentModel;

namespace ProjectMyShopClient.DTO
{
    public class Account : INotifyPropertyChanged
    {
        public string Username { get; set; }
        public string Rolename { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
