using MessagePack;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ProjectMyShopClient
{
    public class SOjectManager
    {
        public static string name;
        public static string id;
        public static Thread t;
        public static int port = 8080;
        public static dynamic data = null;
        public static Socket master;
        static SOjectManager()
        {
        A: string ip = "127.0.0.1";
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipe = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                master.Connect(ipe);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can not connet to server");
                Thread.Sleep(1000);
                goto A;
            }
            t = new Thread(Data_IN);
            t.Start();
        }
        public static void Data_IN()
        {
            byte[] Buffer;
            int readBytes;

            for (; ; )
            {
                Buffer = new byte[master.SendBufferSize];
                readBytes = master.Receive(Buffer);
                if (readBytes > 0)
                {
                    Packet p = new Packet(Buffer);
                    DataManager(p);
                }
            }

        }

        private static void DataManager(Packet packet)
        {
            switch (packet.PacketType)
            {
                case PacketTypeEnum.REGISTRATION_PACKET:
                    Debug.WriteLine("Recieve registration packet from socket client side server");
                    id = packet.SenderID;
                    break;
                case PacketTypeEnum.CREATE_PACKET:
                    Debug.WriteLine("Create BUS");
                    int ID = (int)(long)packet.GetAttributeValue("ID");
                    //data = new { };
                    data = ID;
                    break;
                case PacketTypeEnum.EXECUTE_PACKET:
                    Debug.WriteLine("EXECUTE_PACKET");
                    dynamic ouputParams = (dynamic)packet.GetAttributeValue("outputParams");
                    //data = new { };
                    data = ouputParams;
                    break;

            }
        }

        public static int CreateRemoteObject(string typeName)
        {
            Packet p = new Packet(PacketTypeEnum.CREATE_PACKET, id);
            p.SetAttributeValue("typeName", typeName);
            master.Send(p.ToBytes());
            for (; ; )
            {
                if (data != null && data is int)
                {
                    int id = (int)data;
                    data = null;
                    return id;
                }
            }
        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, dynamic inputParams)
        {
            Debug.WriteLine("MethodName:" + methodName);
            Packet p = new Packet(PacketTypeEnum.EXECUTE_PACKET, id);
            p.SetAttributeValue("ID", ID);
            p.SetAttributeValue("methodName", methodName);
            p.SetAttributeValue("inputParams", inputParams);
            master.Send(p.ToBytes());
            for (; ; )
            {
                if (data != null )
                {
                    //if(data is bool && methodName.Contains("get")) {
                    //    data = null;
                    //    continue;
                    //}
                    dynamic data2 = data;
                    data = null;
                    return data2;
                }
            }
        }


        public static void SendObject(dynamic data)
        {
            byte[] bytes = ConvertObjectToBytes(data);
            master.Send(bytes);
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