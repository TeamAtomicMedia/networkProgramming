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
            IPAddress myIP;
            int port;
            TcpClient client = new TcpClient(myIP.ToString(), port);
            NetworkStream networkStream = null;
            while (true)
            {
                Console.WriteLine("Enter data to write to stream");
                string data = Console.ReadLine();
                 /*Write to data to network stream*/

                Console.WriteLine();
            }
        }
    }
}