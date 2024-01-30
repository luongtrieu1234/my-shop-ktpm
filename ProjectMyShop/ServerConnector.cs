using MessagePack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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
                    dynamic receivedObject = RecieveObject(Buffer);
                    if (ContainsKey(receivedObject, "CreateRemoteObject"))
                    {
                        int ID = SOjectManager.CreateRemoteObject(receivedObject["typeName"]);
                        SendObject(clientSocket, new { ID });
                    }
                    else if (ContainsKey(receivedObject, "methodName"))
                    {
                        dynamic sendObject = SOjectManager.ExecuteRemoteMethod(receivedObject["ID"],
                            receivedObject["methodName"], receivedObject["inputParams"]);
                        SendObject(clientSocket, sendObject);
                    }
                }
            }

        }
        public static bool ContainsKey(dynamic obj,string key)
        {
            try
            {
                if (obj[key])
                {
                    return true;
                }
            }catch(Exception e)
            {
                return false;
            }
            return false;
        }

        public static void SendObject(Socket socket, dynamic data)
        {
            byte[] buffer = ConvertObjectToBytes(data);
            socket.Send(buffer);
        }
        public static dynamic RecieveObject(byte[] buffer)
        {
            dynamic data = ConvertBytesToObject(buffer);
            return data;
        }

        public static byte[] ConvertObjectToBytes(dynamic obj)
        {
            return MessagePackSerializer.Serialize(obj);
        }

        public static dynamic ConvertBytesToObject(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<dynamic>(bytes);
        }



    }
}