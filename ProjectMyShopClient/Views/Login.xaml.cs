using ProjectMyShopClient.Config;
using ProjectMyShopClient.SBUS;
using System.Windows;
using System.Windows.Controls;

namespace ProjectMyShopClient.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        SAccountBUS _accountBUS = new SAccountBUS();

        private string _username;
        private string _password;

        public Login()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _username = "username";
            _password = "password";

            bool canGetAccount = false;
            try
            {
                _username = AppConfig.GetValue(key: AppConfig.Username);
                _password = AppConfig.GetPassword();
                canGetAccount = true;
            }
            catch { System.Diagnostics.Debug.WriteLine("First time running - Cannot read username/password"); }

            if (canGetAccount)
            {
                TextBoxEmail.Text = _username;
                PasswordBox.Password = _password;
            }

        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            _username = TextBoxEmail.Text;
            _password = PasswordBox.Password;


            if (_username != null && _password != null)
            {

                // validate account
                if (_accountBUS.validate(_username, _password))
                {
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Wrong username or password", "Login", MessageBoxButton.OK, MessageBoxImage.Warning);
                    System.Diagnostics.Debug.WriteLine("Cannot connect to db");
                }
            }
            else
            {
                // gain focus 
            }

        }

    }
}
