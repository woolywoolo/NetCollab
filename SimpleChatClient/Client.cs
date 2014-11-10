using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace NetCollab
{
    class Client
    {
        static void Main(string[] args)
        {
            try
            {
                IPAddress adress = IPAddress.Parse("127.0.0.1");

                TcpClient client = new TcpClient();
                Console.WriteLine("Connecting to: " + adress.ToString());

                client.Connect("127.0.0.1", 8001);

                Console.WriteLine("[!] Connected to server.");
                Console.Write("\nEnter message: ");

                String str = Console.ReadLine();
                Stream stm = client.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);
                Console.WriteLine("Transmitting...");

                stm.Write(ba, 0, ba.Length);

                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);

                Console.Write("Server sent: ");
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(bb[i]));

                client.Close();
                Console.ReadLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.StackTrace);
            }
        }
    }
}
