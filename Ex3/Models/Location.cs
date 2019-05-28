using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

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

        public void ToXml(XmlWriter writer)
        {
            writer.WriteStartElement("Location");
            writer.WriteElementString("lon", this._lon.ToString());
            writer.WriteElementString("lat", this._lat.ToString());
            writer.WriteEndElement();
        }
    }
}