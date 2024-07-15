using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketTCPandUDP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region TCP
            //const string ip = "127.0.0.1";
            //const int port = 8000;

            ////Create socket
            //while (true)
            //{
            //    var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //    //Мы создали тсп сокет, который находится в режиме ожидания, ждет приема сообщения
            //    var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //    tcpSocket.Bind(tcpEndPoint);
            //    //Start listening
            //    tcpSocket.Listen(5);

            //    while (true)
            //    {
            //        //сокет для обработки клиента, после обработки он уничтожается
            //        var listener = tcpSocket.Accept();
            //        //создаем буфер
            //        var buffer = new byte[256];
            //        //переменная для реального количества байт
            //        var size = 0;
            //        var data = new StringBuilder();
            //        do
            //        {
            //            size = listener.Receive(buffer);
            //            data.Append(Encoding.UTF8.GetString(buffer, 0, size));
            //        }
            //        while (listener.Available > 0);

            //        Console.WriteLine(data);

            //        listener.Send(Encoding.UTF8.GetBytes("Success"));

            //        //Закрыть подключение
            //        listener.Shutdown(SocketShutdown.Both);
            //        listener.Close();
            //    }
            //}
            #endregion

            const string ip = "127.0.0.1";
            const int port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            //Мы создали udp сокет, который находится в режиме ожидания, ждет приема сообщения
            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            udpSocket.Bind(udpEndPoint);

            while (true)
            {
                var buffer = new byte[256];
                //переменная для реального количества байт
                var size = 0;
                var data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

                do
                {
                    size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                    data.Append(Encoding.UTF8.GetString(buffer));
                }
                while (udpSocket.Available > 0);

                udpSocket.SendTo(Encoding.UTF8.GetBytes("Success"), senderEndPoint);

                Console.WriteLine(data); 
            }
        }
    }
}
