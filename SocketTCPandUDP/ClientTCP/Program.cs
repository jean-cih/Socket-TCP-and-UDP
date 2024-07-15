using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTCP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1";
            //const int port = 8000;

            //while (true)
            //{
            //    //Создание сокета
            //    var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //    var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //    Console.WriteLine("Enter some message");
            //    var message = Console.ReadLine();

            //    var data = Encoding.UTF8.GetBytes(message);

            //    tcpSocket.Connect(tcpEndPoint);
            //    tcpSocket.Send(data);

            //    var buffer = new byte[256];
            //    var size = 0;
            //    var answer = new StringBuilder();

            //    do
            //    {
            //        size = tcpSocket.Receive(buffer);
            //        answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            //    }
            //    while (tcpSocket.Available > 0);

            //    Console.WriteLine(answer.ToString());

            //    //Закрываем сокет
            //    tcpSocket.Shutdown(SocketShutdown.Both);
            //    tcpSocket.Close();
            //}
            #endregion

            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //Мы создали udp сокет, который находится в режиме ожидания, ждет приема сообщения
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            while (true)
            {
                Console.WriteLine("Enter your message:");
                var message = Console.ReadLine();

                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
                udpSocket.SendTo(Encoding.UTF8.GetBytes(message), serverEndPoint);

                var buffer = new byte[256];
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(data);
                Console.ReadLine();
            }

        }
    }
}
