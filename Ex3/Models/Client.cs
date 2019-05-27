using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public static class Client
    {
        public static TcpClient server;
        public static NetworkStream networkStream;

        public static void Connect(string ip, string p)
        {
            int port = Int32.Parse(p);

            try
            {
                server = new TcpClient(ip, port);
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to server");
            }
        }

        public static void Close()
        {
            Console.WriteLine("Disconnecting from server...");
            networkStream.Close();
            server.Close();
        }

        public static Location ReadFromServer()
        {
            byte[] data = new byte[1024];
            string stringData;

            networkStream = server.GetStream();
            Debug.WriteLine("reading from the server...");
            int recv = networkStream.Read(data, 0, data.Length);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Debug.WriteLine(stringData);
            string[] values = stringData.Split(',');
            double lon = Convert.ToDouble(values[0]);
            double lat = Convert.ToDouble(values[1]);
            return new Location(lon, lat);
        }

    }
}