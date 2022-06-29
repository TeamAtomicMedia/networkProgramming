# Network programming basics in .Net

According to Wikipedia, a reliable source as any, describes network programming that enables "processes to communicate with each other across a computer network. These can be roughly divided into two flavours: connection-orientated  and connectionless. This is determined by the communication protocol.

## Connection-Orientated Communication


In general, any protocol which needs to determine whether a communication session has been established is a connection-orientated system. Each connection is usually defined with a dedicated connection line, this can be either a physical implementation or a virtual connection. TCP, the protocol we will explore here, uses the latter model. The appropriate address is defined in the packet level. Packet switching is then quicker easier to implement at the hardware level.

### Client-Server Model
Conventionally, in networks, the client is the node that initiates connections and the server is the node which waits for connections. 
### TCP and network programming in .Net
To construct a TCP connection between two points you need to define two sockets. These are essentially the start and end point of the network. They are defined by an IP address and a Port number, and can take multiple states.

|State   |Endpoint|Description                            |
|--------|--------------------|---------------------------------------|
|LISTEN  |Server  |Wait for a connection request from any remote TCP socket.|
|SYN-SENT|Client  |Waiting for a matching connection request after having sent a connection request.|
|SYN-RECIEVED|Server|Waiting for a confirming connection request acknowledgment after having both received and sent a connection request.|
|ESTABLISHED|Server and Client|An open connection, data received can be delivered to the user. The normal state for the data transfer phase of the connection.|
|FIN-WAIT-1|Server and Client|Waiting for a connection termination request from the remote TCP, or an acknowledgment of the connection termination request previously sent.|
|FIN-WAIT-1|Server and Client|Waiting for a connection termination request from the remote TCP.|
|CLOSE-WAIT|Server and Client|Waiting for a connection termination request from the local user.|       
|LAST-ACK|Server and Client|Waiting for an acknowledgment of the connection termination request previously sent to the remote TCP (which includes an acknowledgment of its connection termination request).|
|TIME-WAIT|Server and Client|Waiting for enough time to pass to be sure that all remaining packets on the connection have expired.|
|CLOSED| Server or Client|No connection state at all.|

### Writing a TCP Listener and Client

C\#.NET allows us to implement TCP really easily. In less than 30 lines we can write a simple TCP client, and in just shy of 40 lines we can writer the listener (server). Using the modules, System.Net and System.Net.Sockets gives us the capability to both read and write to the network stream.

Using the code abstracts and explanations below, try to fix the TCP Listener and TCP Client Template provided in the repo.


```C#
using System.Net; /*Provides the interface for TCP and
other protocols  used on networks today.*/

using System.Net.Sockets;/*Provides the interface to communicated with 
the Windows TCP/IP client applications and the underlying protocol stack*/

IPAddress localAddr = IPAddress.Loopback;/*127.0.0.1*/

TcpListener server = new TcpListener(IPAddress ip, int port);
/*Creates a new TCP Listener instance at the given socket*/

server.Start();/*Starts the TCP listener*/ 

TcpClient client = new TcpClient(String myIP.ToString(), int port);
/*Creates a new TCP Client instance at the given socket*/

Socket socket = client.Client; /*Socket object of the client*/

Socket.Poll(timeoffset, mode);/*determines if the socket is in a given mode,  
eg client.Client.Poll(1, SelectMode.SelectRead)*/

client.Client.Available;/*Returns the amount of data that 
has been recieved from the network and is available to read*/

client.GetStream();/*returns the network stream used to send and recieve data*/
NetworkStream netStream = client.GetStream();

networkStream.Write(byte[] buffer, int offset, int size);/*Writes to network stream from */
```

## Using Wireshark to analyse TCP packets

From a pair of TCP Sockets built using the above functions we can see what is actually happening at a network stream level using wireshark, configured to only watch the loopback IP and associated with port (1234):

### Wireshark
<img width="1510" alt="Screenshot%202022-04-27%20at%2014 25 01" src="https://user-images.githubusercontent.com/104011332/176417349-25a350ce-f8e0-484b-8e63-3dd33968c691.png">
### Packets
#### Packet 1
The first packet contains the flag "SYN", this is used to SYNchronise sequence numbers to initiate the TCP connection .
#### Packet 2
The second packet contains the flags "SYN" and "ACK". The sequence number of the actual first data byte and the ACKnowledged number in the corresponding ACK are then this sequence number plus 1.
#### Packet 3

This packet is essentially the receipt of the client receiving the server's SYN message.
#### Packet 4
This packet tells the client the maximum amount of bytes the host can receive before it's buffer is full.
#### Packet 5
This is the first packet that contains any of the actual payload data. The PSH flag asks to PuSH this buffered data to the receiving application.
#### Packet 6
This packet is ACK receipt for the previous packet.
#### Packet 7
The FIN flag shows that this is the last packet from sender.
#### Packet 8
This packet acknowledges the previous packet.
#### Packet 9
Requests a reset of the session.

## Connectionless
### UDP
In the github repo, you will find an example of a UDP listener and UDP client. Notice the difference. No network stream is established and no client is accepted by the server. Using wireshark, what do you notice differently about the packets sent?

