using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class Location
    {
        private double _lon;
        public double Lon {
            get { return _lon;}
            set { _lon = value; }
        }
        private double _lat;
        public double Lat
        {
            get { return _lat; }
            set { _lat = value; }
        }
        public Location(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;

        }


    }
}