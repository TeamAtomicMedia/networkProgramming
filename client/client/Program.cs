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
            IPAddress myIP = IPAddress.Loopback;
            int port = 1234;
            TcpClient client = new TcpClient(myIP.ToString(), port);
            Console.WriteLine(client.Client.LocalEndPoint);
            NetworkStream networkStream = client.GetStream();
            while (true)
            {
                Console.WriteLine("Enter data to write to stream");
                string data = Console.ReadLine();
                byte[] sendbytes = Encoding.ASCII.GetBytes(data);
                networkStream.Write(sendbytes, 0, sendbytes.Length);
                
                Console.Clear();
            }
        }
    }
}
