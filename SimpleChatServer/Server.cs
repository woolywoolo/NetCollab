using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SimpleChatServer
{
    class Server
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress adress = IPAddress.Parse("127.0.0.1"); // local host for now

                TcpListener listener = new TcpListener(adress, 8001);
                listener.Start();

                Console.WriteLine("Server running @ " + listener.LocalEndpoint);
                Console.WriteLine("Waiting for connection...");

                Socket socket = listener.AcceptSocket();
                Console.WriteLine("\n[!] Connection accepted from " + socket.RemoteEndPoint);

                byte[] b = new byte[100];
                int k = socket.Receive(b);
                Console.WriteLine("[!] Received message: ");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));
                Console.WriteLine("\n[!] End of message!");

                ASCIIEncoding asen = new ASCIIEncoding();
                socket.Send(asen.GetBytes("Message received!"));
                Console.WriteLine("\n[!] Sent: \"Message received\" to client.");

                socket.Close();
                listener.Stop();

                Console.ReadLine();
            }catch(Exception e){
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }
    }
}
