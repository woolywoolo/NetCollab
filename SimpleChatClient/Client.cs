using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace NetCollab
{
    class Client
    {
        private static TcpClient client;
        static void Main(string[] args)
        {
            Thread thread = new Thread(Server.startServer);
            thread.Start();
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");

                client = new TcpClient();
                Console.WriteLine("CLIENT: Connecting to: " + address.ToString());

                client.Connect("127.0.0.1", 8001);

                Console.WriteLine("CLIENT: [!] Connected to server.");
                Console.Write("\nCLIENT: Enter message: ");

                while (client.Connected)
                {
                    send(Console.ReadLine().Trim());
                    string msg = recieve();

                    Console.Write("Server: " + msg);
                }
                client.Close();
                Console.ReadLine();
            }

            catch (Exception e)
            {
                Console.WriteLine("CLIENT: Error: " + e.StackTrace);
            }
        }
        static bool send(string msg)
        {
            try
            {

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(msg);
                Stream stm = client.GetStream();
                stm.Write(ba, 0, ba.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("CLIENT: Error: " + e.StackTrace);
                return false;
            }
            return true;
        }
        static string recieve()
        {
            try
            {
                Stream stm = client.GetStream();
                byte[] bb = new byte[100];
                int k = stm.Read(bb, 0, 100);
                string msg = "";
                for (int i = 0; i < k; i++)
                    msg += Convert.ToChar(bb[i]);
                return msg;
            }
            catch (Exception e)
            {

                Console.WriteLine("CLIENT: Error: " + e.StackTrace);
                return "";
            }
        }
    }
}
