using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace TCP
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 1234;
            IPAddress localAddr = IPAddress.Loopback;
            TcpListener server = new TcpListener(localAddr, port);/*Defines TCP listener at loopbackIP and port1234*/
            TcpClient client; 
            server.Start();
            Console.Write("Server started");
            client = server.AcceptTcpClient();

            while (true){

                NetworkStream netStream = client.GetStream();
                byte[] bytes = new byte[256];
                netStream.Read(bytes, 0, bytes.Length);
                string clientdata = Encoding.ASCII.GetString(bytes);
                int index = 0;
                if (!(client.Client.Poll(1, SelectMode.SelectRead) && client.Client.Available == 0))
                {
                    Console.WriteLine("Client sent: ");
                    while (index < clientdata.Length || index == 0 || clientdata.Length == 0)
                    {
                        Console.Write(clientdata[index]);
                        index += 1;
                    }
                    Console.WriteLine();
                }           }
        }
    }
}
