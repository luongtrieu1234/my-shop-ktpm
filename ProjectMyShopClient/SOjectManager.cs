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
                    Debug.WriteLine(readBytes);
                    dynamic recieveData = RecieveObject(Buffer);

                    data = recieveData;
                }
            }

        }
        public static int CreateRemoteObject(string typeName)
        {
            SendObject(new { CreateRemoteObject = "CreateRemoteObject", typeName });
            if (data != null)
            {
                int id = (int)data["ID"];
                data = null;
                return id;
            }
            return -1;
        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, dynamic inputParams)
        {
            SendObject(new { ID, methodName, inputParams });
            C: Debug.WriteLine(ID);
            if (data != null)
            {
                dynamic data2 = data;
                data = null;
                return data2;
            }
            return new { };
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