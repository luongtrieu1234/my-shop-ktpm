//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.Graph;
//using ProjectMyShop.DTO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjectMyShop.ViewModels
//{
//    // Subject (Observable)
//    public class MessageService
//    {
//        private const string HostName = "localhost"; // Thay đổi nếu cần
//        private const string ExchangeName = "your_exchange"; // Đặt tên cho exchange

//        private IConnection connection;
//        private IModel channel;

//        public MessageService()
//        {
//            var factory = new ConnectionFactory() { HostName = HostName };
//            connection = factory.CreateConnection();
//            channel = connection.CreateModel();

//            // Đảm bảo rằng exchange đã được khởi tạo
//            channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
//        }

//        public void Send(string message)
//        {
//            var body = Encoding.UTF8.GetBytes(message);
//            channel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: null, body: body);
//        }

//        public void Close()
//        {
//            connection.Close();
//        }
//    }
//}