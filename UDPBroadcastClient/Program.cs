using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPBroadcastClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            string message = Console.ReadLine();

            //Initializes a Socket we can use to send packets
            UdpClient socket = new UdpClient();

            //converts the message using the UTF8 encoding to a byte array
            //It's important to use the same enconding on both server and client
            byte[] data = Encoding.UTF8.GetBytes(message);
            //Sends the converted message to the broadcast address
            //255.255.255.255 is a generic broadcast address that always can be used to broadcast to the local network
            socket.Send(data, data.Length, "255.255.255.255", 65000);

            //Here we have a loop, because we don't know how many replies we can get, because it was a broadcast we send
            //Improvement could be to have it run only for in a pre-specified time period
            while (true)
            {
                //This is used to store the endpoint that replies to our broadcast
                IPEndPoint from = null;
                //receives data from a client, waits here until a client replies
                byte[] received = socket.Receive(ref from);
                //converts the reply back into a string using the same encoding
                string receivedString = Encoding.UTF8.GetString(received);
                Console.WriteLine(receivedString + " - " + from.Address);
            }
        }
    }
}
