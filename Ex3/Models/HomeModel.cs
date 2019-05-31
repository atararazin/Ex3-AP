using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class HomeModel:IDisplayModel
    {
        private Location location;

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

        public void Connect(string ip, string port)
        {
            Client.Connect(ip, port);
            double lat = Client.ReadFromServer("lat");
            double lon = Client.ReadFromServer("lon");
            this.location = new Location(lon, lat);
        }

        public Location GetLocation()
        {
            return this.location;
        }

        public void GetData(string fileName, string numOfSec)
        {
            
        }
    }
}