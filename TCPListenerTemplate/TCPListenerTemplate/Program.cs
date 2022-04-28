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
            int port;
            IPAddress localAddr = null;
            TcpListener server = new TcpListener(localAddr, port);/*Defines TCP listener at loopbackIP and port1234*/
            TcpClient client;
            server.Start();
            Console.WriteLine("Server started");
            client = null;
            while (true)
            {
                NetworkStream netStream = client.GetStream();
                byte[] bytes = new byte[256];
                /*load netstream bytes into the bytes buffer*/
                string clientdata; /*convert from bytes to string*/

                if (true) /*write condition to check if the client is still viable*/ 
                {
                    Console.WriteLine("Client sent: ");

                    Console.Write(clientdata);

                    Console.WriteLine();
                }
            }
        }
    }
}