using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace genericUDPClient
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {


                UdpClient udpClient = new UdpClient(11000);
                udpClient.Connect(IPAddress.Loopback, 11001);
                Console.WriteLine("Enter message");
                String message = Console.ReadLine();
                Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

                udpClient.Send(sendBytes, sendBytes.Length);
                udpClient.Close();
            }
        }
    }
}
