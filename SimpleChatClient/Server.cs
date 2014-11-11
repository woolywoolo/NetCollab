using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetCollab
{
    class Server
    {
        public static void startServer()
        {
            try
            {
                IPAddress adress = IPAddress.Parse("127.0.0.1"); // local host for now

                TcpListener listener = new TcpListener(adress, 8001);
                listener.Start();

                Console.WriteLine("SERVER: Server running @ " + listener.LocalEndpoint);
                Console.WriteLine("SERVER: Waiting for connection...");

                Socket socket = listener.AcceptSocket();
                Console.WriteLine("SERVER: \n[!] Connection accepted from " + socket.RemoteEndPoint);

                bool check = true;
                while (check)
                {
                    byte[] b = new byte[100];
                    int k = socket.Receive(b);
                    Console.Write("Received message: ");
                    for (int i = 0; i < k; i++)
                        Console.Write(Convert.ToChar(b[i]));
                    Console.WriteLine();
                    ASCIIEncoding asen = new ASCIIEncoding();
                    socket.Send(asen.GetBytes("Message received!"));
                    if(Convert.ToChar(b[1])=='^')
                    {
                        check = false;
                    }
                }

                socket.Close();
                listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("SERVER: Error: " + e.StackTrace);
            }
        }

    }
}
