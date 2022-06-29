using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace UDP
{
    class Program
    {
        public struct UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }

        public static bool messageReceived = false;

        public static void ReceiveCallback(IAsyncResult ar)
        {
            UdpClient u = ((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = ((UdpState)(ar.AsyncState)).e;

            byte[] receiveBytes = u.EndReceive(ar, ref e);
            string receiveString = Encoding.ASCII.GetString(receiveBytes);

            Console.WriteLine($"Received: {receiveString}");
            messageReceived = true;
        }

        public static void ReceiveMessages(IPEndPoint e, UdpClient u)
        {


            UdpState s = new UdpState();
            s.e = e;
            s.u = u;

            Console.WriteLine("listening for messages");
            u.BeginReceive(new AsyncCallback(ReceiveCallback), s);


        }

        public static void RequestCallback(IAsyncResult ar)
        {

            UdpClient u = ((UdpState)(ar.AsyncState)).u;
            IPEndPoint e = ((UdpState)(ar.AsyncState)).e;


            Console.Write(" ");


        }
        public static void SendMessages(IPEndPoint e, UdpClient u, IPEndPoint e2)
        {

            UdpState s = new UdpState();
            s.e = e;
            s.u = u;


            String message = Console.ReadLine();
            Byte[] sendbytes = Encoding.ASCII.GetBytes(message);
            s.u.Connect(e2);
            s.u.SendAsync(sendbytes, sendbytes.Length);

        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter send port");
            int sendport = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter recieve port");
            int recieveport = Int32.Parse(Console.ReadLine());

            IPEndPoint e = new IPEndPoint(IPAddress.Loopback, sendport);
            IPEndPoint e2 = new IPEndPoint(IPAddress.Loopback, recieveport);
            UdpClient u = new UdpClient(e);

            while (true)
            {
                ReceiveMessages(e, u);
                SendMessages(e, u, e2);
                messageReceived = false;
            }
        }
    }
}
