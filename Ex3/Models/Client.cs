using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Ex3.Models
{
    public static class Client
    {
        private static  readonly Regex regex;
        public static TcpClient client;
        public static NetworkStream networkStream;

        // Initial the regex pattern
        static Client()
        {
            regex = new Regex(@"=\s+'(.+)'");
        }


        public static void Connect(string ip, string p)
        {
            int port = Int32.Parse(p);

            try
            {
                client = new TcpClient(ip, port);
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
            client.Close();
        }

        public static Location ReadFromServer()
        {

            byte[] data = new byte[1024];

            networkStream = client.GetStream();
            Debug.WriteLine("reading from the server...");
            byte[] dataLonStr = Encoding.ASCII.GetBytes("get /position/longitude-deg\r\n");
            networkStream.Write(dataLonStr, 0, dataLonStr.Length);

            // Read the result and parse
            networkStream.Read(data, 0, data.Length);
            string raw = Encoding.ASCII.GetString(data);
            double lon = ParseSimulatorResponse(raw);



            byte[] dataLatStr = Encoding.ASCII.GetBytes("get /position/latitude-deg\r\n");
            networkStream.Write(dataLatStr, 0, dataLatStr.Length);

            networkStream.Read(data, 0, data.Length);
            raw = Encoding.ASCII.GetString(data);
            double lat = ParseSimulatorResponse(raw);


            return new Location(lon, lat);
        }

        private static double ParseSimulatorResponse(string raw)
        {
            var match = regex.Match(raw);
            double res = 0;
            if (match.Success)
            {
                try
                {
                    res = Convert.ToDouble(match.Groups[1].Value);
                }
                catch
                {
                    res = 0;
                }
            }
            return res;
        }

    }
}