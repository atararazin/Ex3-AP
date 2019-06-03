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
using System.Net;

namespace Ex3.Controllers
{
    public class HomeController : Controller
    {
        private static IModel currModel;
        private bool shouldWrite;
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, string port, string timesPerSec = "-1")
        {
            shouldWrite = false;
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                HomeModel.Instance.Connect(ip,port);
                currModel = HomeModel.Instance;
                Session["times"] = timesPerSec;
                Session["shouldSave"] = 0;
            }
            else
            {
                DisplayFromFileModel.Instance.OpenFile(ip);
                currModel = DisplayFromFileModel.Instance;
                Session["times"] = port;
                Session["shouldSave"] = 0;
            }

            //Session["times"] = timesPerSec;
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, string port, int timesPerSec, string numOfSec, string fileName)
        {
            shouldWrite = true;

            SaveModel.Instance.Connect(ip,port);
            SaveModel.Instance.openFile(fileName);
            //deal with viewing the map for a number of seconds
            //SaveModel.Instance.SaveToFile(fileName, timesPerSec, numOfSec);
            currModel = SaveModel.Instance;
            Session["times"] = timesPerSec;
            Session["numOfSecs"] = numOfSec;
            Session["shouldSave"] = 1;
            return View("display");
        }

        [HttpPost]
        public string GetLocation()
        {
            currModel.ReadData();
            var location = currModel.GetLocation();
            return ToXml(location);
        }

        [HttpPost]
        public void FinishTask()
        {
            SaveModel.Instance.CloseFile();
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