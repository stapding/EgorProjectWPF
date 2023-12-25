using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows;

namespace Rus
{
    public class Sock
    {

        private static List<Socket> clients = new List<Socket>();

        public static async void startServer()
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, 8080);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(ipPoint);
                while (true)
                {
                    socket.Listen(10);
                    // получаем входящее подключение
                    Socket client = await socket.AcceptAsync();
                    ProductsInfo info = new ProductsInfo(JsonRepository.findAllProducts(), JsonRepository.findAllCategories());

                    string json = JsonSerializer.Serialize(info);

                    byte[] data = Encoding.ASCII.GetBytes(json);

                    byte[] data1 = new byte[1];
                    data1[0] = 1;
                    client.Send(data.Concat(data1).ToArray());

                    clients.Add(client);
                }
            } catch (SocketException ex)
            {

            }
        }

        public static void sendData()
        {
            try
            {
                ProductsInfo info = new ProductsInfo(JsonRepository.findAllProducts(), JsonRepository.findAllCategories());

                string json = JsonSerializer.Serialize(info);

                byte[] data = Encoding.ASCII.GetBytes(json);

                byte[] data1 = new byte[1];
                data1[0] = 1;

                foreach (var client in clients)
                {
                    client.Send(data.Concat(data1).ToArray());
                }
            }
            catch (SocketException ex)
            {

            }
        }
    }
}
