using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class HomeModel
    {
        private Location location;
        public string Ip { get; set; }
        public string Port { get; set; }

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

        public void Connect()
        {
            Client.Connect(Ip, Port);
            double lat = Client.ReadFromServer("lat");
            double lon = Client.ReadFromServer("lon");
            this.location = new Location(lon, lat);
            //Debug.WriteLine("lon =", this.location.Lon, "lat =", this.location.Lat);
        }

        public Location GetLocation()
        {
            return this.location;
        }

    }
}