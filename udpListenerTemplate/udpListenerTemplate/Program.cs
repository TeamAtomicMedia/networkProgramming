using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace udpListenerTemplate
{
    class Program
    {
        public struct UdpState
        {
            public UdpClient udpclient;
            public IPEndPoint endpoint;
        }

        public static bool messageReceived = false;

        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient udpclient = ((UdpState)(ar.AsyncState)).udpclient;
            IPEndPoint endpoint = ((UdpState)(ar.AsyncState)).endpoint

            byte[] receiveBytes = udpclient.EndReceive(ar, ref endpoint);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine("Received: " + receiveString);
            messageReceived = true;
        }

        public static void ReceiveMessages(IPEndPoint e, UdpClient u)
        {


            UdpState s = new UdpState();
            s.e = e;
            s.u = u;

            Console.WriteLine("listening for messages");
            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);

            while (!messageReceived)
            {
                Thread.Sleep(100);

            }
        }
        static void Main(string[] args)
        {

            IPEndPoint e = new IPEndPoint(IPAddress.Loopback, 11001);
            UdpClient u = new UdpClient(e);
            while (true)
            {
                ReceiveMessages(e, u);
                messageReceived = false;
            }
        }
    }
}
