using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace ProjectMyShop
{
    public class ServerConnector
    {
        public NetworkStream sender;
        public NetworkStream receiver;
        public int port = 8080;
        public static Socket listenerSocket;
        public static List<ThreadClient> clients;
        public ServerConnector()
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clients = new List<ThreadClient>();
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
            listenerSocket.Bind(ip);
            Thread listenThread = new Thread(ListenThread);
            listenThread.Start();
        }


        public static void ListenThread()
        {
            while (true)
            {
                listenerSocket.Listen(0);
                clients.Add(new ThreadClient(listenerSocket.Accept()));
            }
        }

        public static void Data_IN(object cSocket)
        {
            Socket clientSocket = (Socket)cSocket;
            byte[] Buffer;
            int readBytes;

            for (; ; )
            {
                Buffer = new byte[clientSocket.SendBufferSize];
                readBytes = clientSocket.Receive(Buffer);
                if (readBytes > 0)
                {
                    string jsonData = Encoding.ASCII.GetString(Buffer, 0, readBytes);

                    // Deserialize JSON to dynamic object
                    dynamic receivedObject = RecieveObject(Buffer, readBytes);
                    if (ContainsKey(receivedObject, "CreateRemoteObject"))
                    {
                        int ID = SOjectManager.CreateRemoteObject(receivedObject?.typeName);
                        SendObject(clientSocket, new {ID});
                    }
                    else if (ContainsKey(receivedObject, "methodName"))
                    {
                        dynamic sendObject = SOjectManager.ExecuteRemoteMethod(receivedObject?.ID, 
                            receivedObject?.methodName, receivedObject?.inputParams);
                        SendObject(clientSocket,sendObject);
                    }
                }
            }

        }

        public static void SendObject(Socket socket, dynamic data)
        {
            byte[] bytes = ConvertDynamicToBytes(data);
            socket.Send(bytes);
        }
        public static dynamic RecieveObject(byte[] buffer, int bytesRead)
        {
            string json = ConvertBytesToJson(buffer, bytesRead);
            dynamic data = ConvertJsonToDynamic(json);
            return data;
        }

        public static byte[] ConvertDynamicToBytes(dynamic data)
        {

            string responseJson = JsonConvert.SerializeObject(data);
            byte[] responseBytes = Encoding.ASCII.GetBytes(responseJson);
            return responseBytes;
        }
        public static string ConvertBytesToJson(byte[] buffer, int bytesRead)
        {
            // Convert received data to string
            string jsonData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            return jsonData;
        }
        public static dynamic ConvertJsonToDynamic(String jsonData)
        {
            // Deserialize JSON to dynamic object
            dynamic receivedObject = JsonConvert.DeserializeObject< ExpandoObject>(jsonData);
            return receivedObject;

        }
        public static bool ContainsKey(dynamic obj, string key)
        {
            try
            {
                // Thử truy cập thuộc tính, nếu không có, sẽ ném ngoại lệ
                var value = obj[key];
                return true;
            }
            catch (Exception)
            {
                // Nếu có ngoại lệ, key không tồn tại
                return false;
            }
        }


    }
}