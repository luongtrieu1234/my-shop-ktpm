using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;


namespace ProjectMyShop
{
    public class Server
    {
        private static Server _instance;
        private TcpListener server;
        private List<TcpClient> clients = new List<TcpClient>();
        private NetworkStream ns;

        private Server()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            server = new TcpListener(ip, 5000);

            server.Start();
            Debug.WriteLine("Server started on " + server.LocalEndpoint);
            AcceptClientAsync();
        }
        private async void AcceptClientAsync()
        {
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                clients.Add(client);
                Console.WriteLine("Accepted client " + client.Client.RemoteEndPoint);
            }
        }
        public static Server Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Server();
                }
                return _instance;
            }
        }
        public void SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            foreach (TcpClient client in clients)
            {
                if (client.Connected)
                {
                    NetworkStream ns = client.GetStream();
                    ns.Write(buffer, 0, buffer.Length);
                }
            }
        }
        public void StopServer()
        {
            foreach (TcpClient client in clients)
            {
                client.Close();
            }
            server.Stop();
        }
    }

}
