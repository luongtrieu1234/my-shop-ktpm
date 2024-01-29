using Microsoft.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                    dynamic recieveData = RecieveObject(Buffer, readBytes);
                    data = recieveData;
                }
            }

        }
        public static int CreateRemoteObject(string typeName)
        {
            SendObject(new { CreateRemoteObject = "CreateRemoteObject", typeName });

            if (data != null)
            {
                int id = (int)data.ID;
                data = null;
                return id;
            }
            return 0;

        }

        public static dynamic ExecuteRemoteMethod(int ID, string methodName, dynamic inputParams)
        {
            SendObject(new { ID, methodName, inputParams });
            if (data != null)
            {
                dynamic data2 = data;
                data = null;
                return data2;
            }
            return 0;
        }


        public static void SendObject(dynamic data)
        {
            byte[] bytes = ConvertDynamicToBytes(data);
            master.Send(bytes);
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
            dynamic receivedObject = JsonConvert.DeserializeObject(jsonData);
            return receivedObject;
        }
    }
}