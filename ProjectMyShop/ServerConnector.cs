using MessagePack;
using ProjectMyShop.DTO;
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
                    Packet packet = new Packet(Buffer);
                    DataManager(clientSocket,packet);
                }
            }

        }

        public static void DataManager(Socket clientSocket,Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketTypeEnum.CREATE_PACKET:
                    string typeName =(string) packet.GetAttributeValue("typeName");
                    int ID = SOjectManager.CreateRemoteObject(typeName);
                    Packet p1 = new Packet(PacketTypeEnum.CREATE_PACKET);
                    p1.SetAttributeValue("ID", ID);
                    clientSocket.Send(p1.ToBytes());
                    break;
                case PacketTypeEnum.EXECUTE_PACKET:
                    int ID1 = (int)(long) packet.GetAttributeValue("ID");
                    string methodName1 = (string)packet.GetAttributeValue("methodName");
                    dynamic inputParams1 = (dynamic)packet.GetAttributeValue("inputParams");
                    dynamic sendObject = SOjectManager.ExecuteRemoteMethod(ID1,methodName1,inputParams1);
                    if(sendObject is Data)
                    {
                        sendObject.Avatar = null;
                    }else if(sendObject is List<Data> || sendObject is List<Product>) { 
                        foreach(var item in sendObject)
                        {
                            item.Avatar = null;
                        }
                    }
                    Packet p2 = new Packet(PacketTypeEnum.EXECUTE_PACKET);
                    p2.SetAttributeValue("outputParams",sendObject);
                    clientSocket.Send(p2.ToBytes());
                    break;
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