﻿using Ex3.Models;
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
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult display(string ip, string port, string timesPerSec = "1")
        {
            IPAddress address;
            if (IPAddress.TryParse(ip, out address))
            {
                HomeModel.Instance.Connect(ip,port);
            }
            else
            {
                DisplayFromFileModel.Instance.Display(ip, port);
            }

            Session["times"] = timesPerSec;
            return View();
        }

        [HttpGet]
        public ActionResult save(string ip, string port, int timesPerSec, int numOfSec, string fileName)
        {
            SaveModel.Instance.Connect(ip,port);
            //deal with viewing the map for a number of seconds
            SaveModel.Instance.SaveToFile(fileName);

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