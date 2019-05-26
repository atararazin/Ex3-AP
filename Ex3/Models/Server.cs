using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace Ex3.Models
{
    public static class Server
    {
        public static TcpClient client;

        public static void Connect(string ip, string p)
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(ip);
                int port = int.Parse(p);
                TcpListener listener = new TcpListener(ipAd, port);

                listener.Start();

                Debug.WriteLine("Waiting for a connection.....");

                client = listener.AcceptTcpClient();

                Debug.WriteLine("Connection accepted");
                listener.Stop();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }
        }

        public static void Read()
        {
            NetworkStream stream = client.GetStream();
            byte[] msg = new byte[1024];
            stream.Read(msg, 0, msg.Length);
            Debug.WriteLine(System.Text.Encoding.UTF8.GetString(msg));
        }
    }
}