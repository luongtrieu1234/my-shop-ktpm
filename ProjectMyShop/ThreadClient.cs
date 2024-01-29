using System;
using System.Net.Sockets;
using System.Threading;

namespace ProjectMyShop
{
    public class ThreadClient
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;
        public ThreadClient()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(ServerConnector.Data_IN);
            clientThread.Start(clientSocket);
        }
        public ThreadClient(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(ServerConnector.Data_IN);
            clientThread.Start(clientSocket);
        }
    }
}