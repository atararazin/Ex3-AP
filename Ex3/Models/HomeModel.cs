using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class HomeModel
    {
        private static HomeModel s_instace = null;

        public static HomeModel Instance
        {
            get
            {
                if (s_instace == null)
                {
                    s_instace = new HomeModel();
                }
                return s_instace;
            }
        }

        public string Ip { get; set; }
        public string Port { get; set; }
        public int TimesPerSecond { get; set; }
        public int NumOfSeconds { get; set; }
        public string FileName { get; set; }

        public void Connect()
        {
            Server.Connect(Ip, Port);
            Server.Read();
        }

    }
}