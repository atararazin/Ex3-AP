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

        public static double ReadFromServer(string arg)
        {
            networkStream = client.GetStream();
            byte[] data = new byte[1024];

            byte[] reqData = Encoding.ASCII.GetBytes(SimulatorRequests.map[arg]);
            networkStream.Write(reqData, 0, reqData.Length);
           
            // Read the result and parse
            networkStream.Read(data, 0, data.Length);
            string raw = Encoding.ASCII.GetString(data);
            double result = ParseSimulatorResponse(raw);
            return result;
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