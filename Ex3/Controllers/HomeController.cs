using Ex3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Ex3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, string port)
        {
            HomeModel.Instance.Ip = ip;
            HomeModel.Instance.Port = port;
            HomeModel.Instance.Connect();
            return View();
        }

        [HttpPost]
        public string GetLocation()
        {
            var location = HomeModel.Instance.GetLocation();

            return ToXml(location);
        }


        // ToXml fuction from the Tirgul
        private string ToXml(Location location)
        {
            //Initiate XML stuff
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            XmlWriter writer = XmlWriter.Create(sb, settings);

            writer.WriteStartDocument();
            writer.WriteStartElement("Location");

            location.ToXml(writer);

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            return sb.ToString();
        }
    }
}