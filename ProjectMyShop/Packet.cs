using MessagePack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace ProjectMyShop
{
    public enum PacketTypeEnum
    {
        RECIEVE_PACKET = 0,
        REGISTRATION_PACKET = 1,
        CREATE_PACKET = 2,
        EXECUTE_PACKET = 3,
    }
    [Serializable]
    public class Packet
    {
        public static int NextPacketID = 0;
        public Dictionary<string, object> Attributes;
        public int PacketID { get; set; }
        public string SenderID { get; set; }
        public PacketTypeEnum PacketType { get; set; }

        public Packet()
        {
            PacketID = NextPacketID++;
            Attributes = new Dictionary<string, object>();
            PacketType = PacketTypeEnum.RECIEVE_PACKET;
        }
        public Packet(PacketTypeEnum packetType, string senderID)
        {
            Attributes = new Dictionary<string, object>();
            PacketID = NextPacketID++;
            PacketType = packetType;
            SenderID = senderID;
        }
        public Packet(PacketTypeEnum packetType)
        {
            Attributes = new Dictionary<string, object>();
            PacketID = NextPacketID++;
            PacketType = packetType;
        }

        public Packet(byte[] packetbytes)
        {
            PacketID = NextPacketID++;
            string json = Encoding.UTF8.GetString(packetbytes);
            string jsonNotNull = RemoveNullCharacters(json);
            Packet p = JsonConvert.DeserializeObject<Packet>(jsonNotNull);
            this.Attributes = p.Attributes;
            this.PacketID = p.PacketID;
            this.SenderID = p.SenderID;
            this.PacketType = p.PacketType;
        }

        public byte[] ToBytes()
        {
            string json = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(json);
        }
        public static string RemoveNullCharacters(string input)
        {
            int nullIndex = input.IndexOf('\0');
            if (nullIndex != -1)
            {
                return input.Substring(0, nullIndex);
            }
            return input;
        }

        public bool SetAttributeValue(string attributeName, object attributeValue)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                Attributes[attributeName] = attributeValue;
            }
            else
            {
                Attributes.Add(attributeName, attributeValue);
            }
            return true;
        }
        public object GetAttributeValue(string attributeName)
        {
            if (Attributes.ContainsKey(attributeName))
            {
                return Attributes[attributeName];
            }
            else
            {
                return (object)String.Empty;
            }
        }

        public object this[string attributeName]
        {
            get
            {
                return GetAttributeValue(attributeName);
            }
            set
            {
                Attributes[attributeName] = value;
            }
        }

    }
}
