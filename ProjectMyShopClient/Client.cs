using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ProjectMyShop;
using System.Net.Sockets;
using Microsoft.Graph;


namespace ProjectMyShopClient
{

    public class Client
    {
        private TcpClient client;
        private NetworkStream ns;
        public event Action<string> MessageReceived;
        public Client()
        {
            ConnectToServerAsync();
        }
        private async void ConnectToServerAsync()
        {
            try
            {
                client = new TcpClient();
                await client.ConnectAsync("127.0.0.1", 5000);
                ns = client.GetStream();
                ListenForMessagesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error connecting to server: " + ex.Message);
            }
        }
        private async void ListenForMessagesAsync()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                try
                {
                    bytesRead = await ns.ReadAsync(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    // TODO: React to the message
                    MessageReceived?.Invoke(message);
                    Debug.WriteLine("Received: " + message);
                }
                catch (System.IO.IOException)
                {
                    // The server has stopped
                    MessageBox.Show("The server has stopped.", "Message from server", MessageBoxButton.OK, MessageBoxImage.Information);
                    Debug.WriteLine("The server has stopped.");
                    break;
                }
            }
        }
    }
}
