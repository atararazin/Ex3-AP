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
            byte[] dataLonStr = Encoding.ASCII.GetBytes("get /position/longitude-deg\r\n");
            networkStream.Write(dataLonStr, 0, dataLonStr.Length);
            double lon = networkStream.Read(data, 0, data.Length);
            Debug.WriteLine("lon is", lon);
            //return value from the Flight Gear: "/ position / longitude - deg = '-157.9431848'(double)";

            byte[] dataLatStr = Encoding.ASCII.GetBytes("get /position/latitude-deg\r\n");
            networkStream.Write(dataLatStr, 0, dataLatStr.Length);
            double lat = networkStream.Read(data, 0, data.Length);

            return new Location(lon, lat);
        }

    }
}